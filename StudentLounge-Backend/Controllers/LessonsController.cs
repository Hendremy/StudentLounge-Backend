using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    //TODO: Correctement autoriser les utilisateurs, vérifier l'ID dans le token pour voir si correspond à ID fournit en param
    //var id = this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            GetUser();
            return await _context.Lessons.ToListAsync();
        }
        /*
        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }
        */
        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{name}")]
        public async Task<IActionResult> CreateLesson(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var lesson = Activator.CreateInstance<Lesson>();
            lesson.Name = name;
            _context.Add(lesson);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(name);
            }
            return NoContent();
        }*/

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.Id }, lesson);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Lesson>>> GetUserLessons(string userId)
        {
            var users = _context.Users.Where(user => userId == user.Id).Include(u => u.Lessons);
            var myUser = users.First();
            
            return Ok(myUser.Lessons);
        }

        [HttpPut("{lessonId}/user/{userId}")]
        public async Task<ActionResult<Lesson>> JoinLesson(int lessonId, string userId)
        {
            var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
            var user = _context.Users.Where(user => user.Id == userId).Include(u => u.Lessons).First();

            lesson.Users.Add(user);
            user.Lessons.Add(lesson);

            await _context.SaveChangesAsync();

            return Ok(lesson);
        }

        [HttpDelete("{lessonId}/user/{userId}")]
        public async Task<ActionResult<Lesson>> LeaveLesson(int lessonId, string userId)
        {
            var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
            var user = _context.Users.Where(user => user.Id == userId).Include(u => u.Lessons).First();

            lesson.Users.Remove(user);
            user.Lessons.Remove(lesson);

            await _context.SaveChangesAsync();

            return Ok(lesson);
        }

        // DELETE: api/Lessons/5
        /*[HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson(int lessonId)
        {
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }*/

        private void GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claims = identity.Claims;
            }
        }
    }
}
