using Microsoft.AspNetCore.Http;
using Notes.Data.Models;
using System.IO;

namespace Notes.Web.Models
{
    public class UploadFileViewModel
    {
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public UploadFile ToUploadFile() => new UploadFile()
        {
            FileName = string.IsNullOrEmpty(FileName) ? Path.GetFileName(File.FileName) : Path.GetFileName(FileName),
            ContentType = File.ContentType
        };
    }
}
