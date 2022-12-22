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
using StudentLounge_Backend.Models.DTOs;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles="Student")]
    [ApiController]
    public class LessonsController : SecuredController
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _context.Lessons.ToListAsync();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> GetUserLessons()
        {
            var userId = GetUserId();
            var user = _context.AppUsers.Find(userId);
            var lessonDTOs = new List<LessonDTO>();
            foreach(var lesson in user.Lessons)
            {
                var tutoring = _context.Tutorings.FirstOrDefault(t => t.Tutored.Id == userId && t.Lesson.Id == lesson.Id);
                lessonDTOs.Add(new LessonDTO(lesson, tutoring));
            }
            return Ok(lessonDTOs);
        }

        [HttpPut("{lessonId}")]
        public async Task<ActionResult<Lesson>> JoinLesson(string lessonId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
                var user = _context.AppUsers.Where(user => user.Id == userId).Include(u => u.Lessons).First();

                lesson.Users.Add(user);
                user.Lessons.Add(lesson);

                var res = await _context.SaveChangesAsync();

                return Ok(lesson);
            }
            return Unauthorized();
        }

        [HttpDelete("{lessonId}")]
        public async Task<ActionResult<Lesson>> LeaveLesson(string lessonId)
        {
            string userId = GetUserId();
            if (UserIdIsValid(userId)) {
                var lesson = _context.Lessons.Where(lesson => lesson.Id == lessonId).Include(l => l.Users).First();
                var user = _context.AppUsers.Where(user => user.Id == userId).Include(u => u.Lessons).First();

                lesson.Users.Remove(user);
                user.Lessons.Remove(lesson);

                var res = await _context.SaveChangesAsync();

                return Ok(lesson);
            }
            return Unauthorized();
        }

    }
}
