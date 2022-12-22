using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.DTOs;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class ChatsController : SecuredController
    {
        private readonly AppDbContext _context;
        public ChatsController([FromServices] AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
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
