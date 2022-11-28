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

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize("Student")]
    [ApiController]
    public class FileController : SecuredController
    {
        private readonly ITransferFiles _transferFiles;
        private readonly AppDbContext _appDbContext;

        public FileController([FromServices] ITransferFiles transferFiles, AppDbContext appDbContext)
        {
            _transferFiles = transferFiles;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] FileUpload fileUpload)
        {
            try
            {
                var response = await _transferFiles.Upload(",",fileUpload.File);
                //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                return Ok("Upload Successfuly.");
            }
            catch (Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            var stream = _transferFiles.Download(filename);
            return File(stream, "application/pdf", filename);
        }

        [HttpGet("lesson/{lessonId}")]
        public async Task<IEnumerable<LessonFile>> GetLessonFiles()
        {
            return null;
        }
    }
}
