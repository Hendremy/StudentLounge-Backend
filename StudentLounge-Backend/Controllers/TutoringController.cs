using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
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

        [HttpPut("{lessonId}")]
        public async Task<ActionResult<Tutorat>> AskTutorat(string lessonId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                try
                {
                    var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).First();
                    var user = _context.AppUsers.Where(user => user.Id == userId).First();
                    var tutorat = new Tutorat() {Lesson = lesson, Date = DateTime.Now, Tutored = user, TutoredId = userId};

                    //_context.Tutorats.Add(tutorat);
                    //lesson.Tutorats.Add(tutorat);
                    user.TutoratAsked.Add(tutorat);

                    await _context.SaveChangesAsync();
                    return Ok(tutorat);
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound(ex);
                }
            }
            return Unauthorized();
        }

        [HttpPost("{tutoratId}")]
        public async Task<ActionResult<Tutorat>> AcceptTutorat(int tutoratId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                try
                {
                    var user = _context.AppUsers.Where(user => user.Id == userId).Include(u => u.TutoratAccepted).First();
                    var tutorat = _context.Tutorats.Where(tutorat => tutorat.Id == tutoratId).First();

                    tutorat.Tutor = user;

                    await _context.SaveChangesAsync();
                    return Ok(tutorat);
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound(ex);
                }
            }
            return Unauthorized();
        }

        [HttpPost("all/{lessonId}")]
        public async Task<ActionResult<Tutorat>> GetTutorats(string lessonId)
        {
            try
            {
                var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).First();
                var tutorats = _context.Tutorats.Where(tutorat => tutorat.Lesson == lesson).Include(tutorat => tutorat.Tutor).Include(tutorat => tutorat.Tutored).ToList();

                return Ok(tutorats);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
