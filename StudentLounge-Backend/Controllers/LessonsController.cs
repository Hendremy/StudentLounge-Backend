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
            /*string userId = GetUserId();
            if (UserIdIsValid(userId))
            {
                var users = _context.AppUsers.Where(user => userId == user.Id).Include(u => u.Lessons);
                var myUser = users.First();

                return Ok(myUser.Lessons);
            }
            return Unauthorized();*/
            var user = _context.AppUsers.Find(GetUserId());
            return Ok(user.Lessons.Select(l => new LessonDTO(l)));
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
