using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Tutorats;
using System.Xml.Linq;

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
        public async Task<ActionResult<Lesson>> AskTutorat(string lessonId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
                var user = _context.AppUsers.Where(user => user.Id == userId).First();
                var tutorat = Activator.CreateInstance<Tutorat>();

                tutorat.Date = DateTime.Now;
                tutorat.Tutored = user;
                tutorat.Lesson = lesson;

                _context.Tutorats.Add(tutorat);

                lesson.Tutorats.Add(tutorat);
                user.TutoratAsked.Add(tutorat);

                await _context.SaveChangesAsync();

                return Ok(tutorat);
            }
            return Unauthorized();
        }
    }
}
