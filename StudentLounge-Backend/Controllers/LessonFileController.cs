using Microsoft.AspNetCore.Http;
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
using StudentLounge_Backend.Models.DTOs;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Student")]
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
                    return Ok();
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

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(string fileId)
        {
            var file = _appDbContext.LessonFiles
                .Include(file => file.Lesson)
                .FirstOrDefault(file => file.Id == fileId);
            if (file != null)
            {
                var path = $"Lessons/{file.Lesson.Id}/{file.FileName}";
                var stream = _transferFiles.GetDownloadStream(path);
                return stream != null ? File(stream, "application/pdf", file.FileName) : StatusCode(500, "Download failed");
            }
            return BadRequest("Invalid fileId");
        }

        
        [HttpGet("lesson/{lessonId}")]
        public async Task<ActionResult<IEnumerable<LessonFileDTO>>> GetLessonFiles(string lessonId)
        {
            var lesson = _appDbContext.Lessons
                .Include(lesson => lesson.Files)
                .ThenInclude(file => file.Author)
                .FirstOrDefault(lesson => lesson.Id == lessonId);
            if(lesson != null)
            {
                var files = lesson.Files.ToList();
                var filesDTO = ConvertFilesToDTO(files);
                return Ok(filesDTO);
            }
            return BadRequest("Invalid lessonId");
        }

        private IEnumerable<LessonFileDTO> ConvertFilesToDTO(IEnumerable<LessonFile> files)
        {
            IList<LessonFileDTO> filesDTO = new List<LessonFileDTO>();
            foreach(var file in files)
            {
                filesDTO.Add(new LessonFileDTO(file));
            }
            return filesDTO;
        }
    }
}
