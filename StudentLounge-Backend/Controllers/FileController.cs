using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models.UploadFile;
using System.IO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;

namespace StudentLounge_Backend.Controllers
{
    [Route("[controller]")]
    [Authorize("Student")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("FTPUpload")]
        public async Task<IActionResult> FTPUpload([FromForm] FileUpload fileUpload)
        {
            try
            {
                string uploadUrl = String.Format("ftp://{0}/{1}/{2}", "e191088@ftps.cg.helmo.be", "/Students/E191088/StudentLounge", fileUpload.File.FileName);
                var request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.EnableSsl = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("e191088", "Q5C7eeEv");
                byte[] buffer = new byte[1024];
                var stream = fileUpload.File.OpenReadStream();
                byte[] fileContents;
                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    fileContents = ms.ToArray();
                }
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                return Ok("Upload Successfuly.");
            }
            catch (Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        [HttpGet("FTPDownload/{filename}")]
        public async Task<IActionResult> FTPDownload(string filename)
        {
            string downloadUrl = String.Format("ftp://{0}/{1}/{2}", "e191088@ftps.cg.helmo.be", "/Students/E191088/StudentLounge", filename);
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(downloadUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.EnableSsl = true;
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("e191088", "Q5C7eeEv");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
      

            var result = $"Download Complete, status {response.StatusDescription}";
          

            return File(responseStream, "application/pdf", filename);

        }
    }
}
