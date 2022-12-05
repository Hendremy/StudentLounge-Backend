﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models.UploadFile;
using System.IO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonFileController : SecuredController
    {
        private const string LessonsDirectory = "Lessons";
        private readonly ITransferFiles _transferFiles;
        private readonly AppDbContext _appDbContext;

        public LessonFileController([FromServices] ITransferFiles transferFiles, AppDbContext appDbContext)
        {
            _transferFiles = transferFiles;
            _appDbContext = appDbContext;
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileUpload fileUpload)
        {
            //Il y a moyen de récupérer le fichier par Request.Form.Files
            var userId = GetUserId();
            var lesson = _appDbContext.Lessons.Include(lesson => lesson.Files).FirstOrDefault(lesson => lesson.Id == fileUpload.LessonId);
            if (lesson != null)
            {
                string directoryPath = $"{LessonsDirectory}/{lesson.Id}"; 
                var response = await _transferFiles.Upload(directoryPath, fileUpload.File);
                if (response != null && RegisterFileUpload(userId, directoryPath, lesson, fileUpload))
                {
                    return Ok("Upload successful");
                }
                return StatusCode(500, "Upload failed");
            }
            return BadRequest("Invalid lessonId");
        }

        private bool RegisterFileUpload(string userId, string directoryPath, Lesson lesson, FileUpload fileUpload)
        {
            try
            {
                var user = _appDbContext.AppUsers.Where(user => user.Id == userId).First();
                var postedFile = new LessonFile(user, fileUpload.FileName, directoryPath, fileUpload.Type, lesson);
                _appDbContext.LessonFiles.Add(postedFile);
                user.PostedFiles.Add(postedFile);
                lesson.Files.Add(postedFile);
                _appDbContext.SaveChanges();
                return true;
            }
            catch(InvalidOperationException ex)
            {
                return false;
            }
        }

        [Authorize(Roles = "Student")]
        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(string fileId)
        {
            var file = _appDbContext.LessonFiles.FirstOrDefault(file => file.Id == fileId);
            if (file != null)
            {
                var stream = _transferFiles.GetDownloadStream(file.FileName);
                return stream != null ? File(stream, "application/pdf", file.FileName) : StatusCode(500, "Download failed");
            }
            return BadRequest("Invalid fileId");
        }

        [Authorize(Roles = "Student")]
        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<IEnumerable<LessonFile>>> GetLessonFiles(string lessonId)
        {
            var lesson = _appDbContext.Lessons
                .Include(lesson => lesson.Files)
                .FirstOrDefault(lesson => lesson.Id == lessonId);
            if(lesson != null)
            {
                var files = lesson.Files.ToList();
                return Ok(files);
            }
            return BadRequest("Invalid lessonId");
        }

        [HttpGet("files")]
        public async Task<ActionResult> GetNbFiles()
        {
            var files = _appDbContext.LessonFiles.ToArray();
            if (files != null)
            {
                var nb = files.Length;
                return Ok(nb);
            }
            return BadRequest("Error request");
        }
    }
}
