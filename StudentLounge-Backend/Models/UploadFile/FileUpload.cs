using Microsoft.Extensions.FileProviders;
using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models.UploadFile
{
    public class FileUpload
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
