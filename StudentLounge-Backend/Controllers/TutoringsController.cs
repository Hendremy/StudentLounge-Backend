using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.DTOs;
using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class TutoringsController : SecuredController
    {
        private readonly AppDbContext _context;

        public TutoringsController(AppDbContext context)
        {
            _context= context;
        }

        [HttpPost("lesson/{lessonId}")]
        public async Task<ActionResult<TutoringDTO>> AskTutorat(string lessonId)
        {
            try
            {
                var userId = GetUserId();
                var lesson = _context.Lessons.Find(lessonId);
                var user = _context.AppUsers.Find(userId);
                if (user is null || lesson is null) throw new InvalidOperationException();
                var existingTutorat = lesson.Tutorats.FirstOrDefault(t => t.Tutored.Id == userId);
                if(existingTutorat is null)
                {
                    var tutorat = new Tutoring() { Lesson = lesson, Date = DateTime.Now, Tutored = user, TutoredId = userId };
                    user.TutoringRequests.Add(tutorat);
                    await _context.SaveChangesAsync();
                    return Ok(new TutoringDTO(tutorat));
                }
                return BadRequest("Tutoring already asked");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPut("{tutoratId}")]
        public async Task<ActionResult<TutoringDTO>> AcceptTutorat(int tutoratId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                try
                {
                    var user = _context.AppUsers.Where(user => user.Id == userId).Include(u => u.AcceptedTutorings).First();
                    var tutorat = _context.Tutorings
                        .Where(tutoring => tutoring.Id == tutoratId && tutoring.TutoredId != userId)
                        .First();
                    tutorat.Tutor = user;

                    await _context.SaveChangesAsync();
                    return Ok(new TutoringDTO(tutorat));
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound(ex);
                }
            }
            return Unauthorized();
        }

        [HttpGet("lesson/{lessonId}")]
        public ActionResult<IEnumerable<TutoringDTO>> GetLessonTutoringsRequests(string lessonId)
        {
            try
            {
                string userId = GetUserId();
                var lesson = _context.Lessons
                    .Where(lesson => lesson.Id == lessonId)
                    .First();
                var tutorings = _context.Tutorings
                    .Include(tutoring => tutoring.Tutor)
                    .Include(tutoring => tutoring.Tutored)
                    .Where(tutoring => tutoring.Lesson == lesson
                                       && tutoring.Tutored.Id != userId
                                       && tutoring.Tutor == null)
                    .Select(tutoring => new TutoringDTO(tutoring));

                return Ok(tutorings);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ValidatedTutoringDTO>> GetUserValidatedTutorings()
        {
            var user = _context.AppUsers.Find(GetUserId());
            return user.AllTutorings.Where(t => t.Tutor is not null)
                .Select(tutoring => new ValidatedTutoringDTO(tutoring))
                .ToList();
        }
    }
}
