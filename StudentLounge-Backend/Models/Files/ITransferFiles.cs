using StudentLounge_Backend.Models.UploadFile;
using System.Net;

namespace StudentLounge_Backend.Models.Files
{
    public interface ITransferFiles
    {
        FtpWebResponse Upload(string path, IFormFile file);
        Stream Download(string filename);
    }
}
