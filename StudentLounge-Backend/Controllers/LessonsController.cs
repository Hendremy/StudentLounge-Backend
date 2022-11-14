using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentLounge_Backend.Models;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
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
        [HttpPut("{name}")]
        public async Task<IActionResult> PutLesson(string name)
        {
            if (name == null)
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
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesson", new { id = lesson.Id }, lesson);
        }

        [HttpGet("LessonOfUser/{mail}")]
        public async Task<ActionResult<List<Lesson>>> GetLessonsOfUser(String mail)
        {
            var users = _context.Users.Where(user => mail == user.Email).Include(u => u.Lessons);
            var myUser = users.First();
            
            return Ok(myUser.Lessons);
        }

        [HttpPut("LessonRegister/{mail}/{lessonId}")]
        public async Task<ActionResult<Lesson>> LessonRegister(string mail, int lessonId)
        {

            var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
            var user = _context.Users.Where(user => mail.Equals(user.Email)).Include(u => u.Lessons).First();

            lesson.Users.Add(user);
            user.Lessons.Add(lesson);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
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
        }
    }
}
