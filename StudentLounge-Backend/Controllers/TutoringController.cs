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
    public class TutoringController : SecuredController
    {
        private readonly AppDbContext _context;

        public TutoringController(AppDbContext context)
        {
            _context= context;
        }

        [HttpPut("lesson/{lessonId}")]
        public async Task<ActionResult<TutoringDTO>> AskTutorat(string lessonId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                try
                {
                    var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).First();
                    var user = _context.AppUsers.Where(user => user.Id == userId).First();
                    var tutorat = new Tutoring() {Lesson = lesson, Date = DateTime.Now, Tutored = user, TutoredId = userId};

                    //_context.Tutorats.Add(tutorat);
                    //lesson.Tutorats.Add(tutorat);
                    user.TutoringRequests.Add(tutorat);

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
        public async Task<ActionResult<IEnumerable<TutoringDTO>>> GetLessonTutoringsRequests(string lessonId)
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

        //TODO: Mettre cette fonction dans un controller Chat, Tutoring doit être indépendant du Chat
        [HttpGet("chat/")]
        public async Task<ActionResult<IEnumerable<DiscussionDTO>>> GetDiscussions()
        {
            try
            {
                string userId = GetUserId();
                var tutorings = _context.Tutorings
                    .Include(tutoring => tutoring.Tutor)
                    .Include(tutoring => tutoring.Tutored)
                    .Where(tutoring => tutoring.Tutor != null && (tutoring.Tutored.Id == userId
                                       || tutoring.Tutor.Id == userId))
                    .Select(tutoring => new DiscussionDTO(tutoring, userId));

                return Ok(tutorings);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
