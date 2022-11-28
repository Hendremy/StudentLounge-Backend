using Microsoft.Extensions.FileProviders;
using StudentLounge_Backend.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.UploadFile
{
    public class FileUpload
    {
        [Required]
        public IFormFile File { get; set; }
        public LessonFileType Type { get; set; }
        public string LessonId { get; set; }

        public string FileName => File.FileName;
    }
}
