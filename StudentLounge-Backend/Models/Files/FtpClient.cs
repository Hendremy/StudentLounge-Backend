﻿using StudentLounge_Backend.Models.UploadFile;
using System.Net;

namespace StudentLounge_Backend.Models.Files
{
    public class FtpClient : ITransferFiles
    {
        private readonly string _baseUrl;
        private readonly bool _sslEnabled;
        private readonly NetworkCredential _credential;

        public FtpClient(string serverAddress, string mainDirectory, string login, string password, bool sslEnabled)
        {
            _credential = new NetworkCredential(login, password);
            _baseUrl = $"ftp://{login}@{serverAddress}/{mainDirectory}";
            _sslEnabled = sslEnabled;
        }

        public Stream Download(string filePath)
        {
            string downloadUrl = $"{_baseUrl}/{filePath}";
            FtpWebRequest request = CreateRequest(downloadUrl, WebRequestMethods.Ftp.DownloadFile);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        //?? Pas moyen d'ouvrir le Stream du fichier et de write directement sur le requestStream ??
        public async Task<FtpWebResponse> Upload(string path, IFormFile file)
        {
            try
            {
                string uploadUrl = $"{_baseUrl}/{path}/{file.FileName}";
                var request = CreateRequest(uploadUrl, WebRequestMethods.Ftp.UploadFile);
                return await CopyFileToServer(file, request);
            }
            catch (IOException ex)
            {
                return null;
            }
        }

        //private byte[] GetFileContents(IFormFile file)
        //{
        //    byte[] fileContents;
        //    using (var stream = file.OpenReadStream()) { 
        //        using (var ms = new MemoryStream())
        //        {
        //            fileContents = WriteToMemoryStream(stream, ms);
        //        }
        //    }
        //    return fileContents;
        //}

        //private byte[] WriteToMemoryStream(Stream from, MemoryStream to)
        //{
        //    int read;
        //    byte[] buffer = new byte[1024];
        //    while ((read = from.Read(buffer, 0, buffer.Length)) > 0)
        //    {
        //        to.Write(buffer, 0, read);
        //    }
        //    return to.ToArray();
        //}

        private async Task<FtpWebResponse> CopyFileToServer(IFormFile file, FtpWebRequest request)
        {
            using(var fileStream = file.OpenReadStream())
            {
                using(var serverStream = request.GetRequestStream())
                {
                    await fileStream.CopyToAsync(serverStream);
                }
            }
            return (FtpWebResponse)request.GetResponse();
        }

        private FtpWebRequest CreateRequest(string url, string method)
        {
            var request = (FtpWebRequest)WebRequest.Create(url);
            request.EnableSsl = _sslEnabled;
            request.Method = method;
            request.Credentials = _credential;
            return request;
        }
    }
}
