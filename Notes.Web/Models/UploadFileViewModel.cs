using Microsoft.AspNetCore.Http;
using Notes.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Notes.Web.Models
{
    public class UploadFileViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Content Type")]
        public string ContentType { get; set; }

        [Display(Name = "File Size")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public long Length { get; set; }

        [Display(Name = "File")]
        public IFormFile File { get; set; }

        public UploadFile ToUploadFile() => new UploadFile
        {
            FileName = GetFileName(),
            ContentType = File.ContentType,
            Length = File.Length,
            Data = GetFileData()
        };

        private string GetFileName()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return Path.GetFileName(File.FileName);
            }
            else
            {
                var name = Path.GetFileName(FileName);

                if (string.IsNullOrEmpty(Path.GetExtension(name)))
                {
                    name = Path.GetFileNameWithoutExtension(name) + Path.GetExtension(File.FileName);
                }

                return name;
            }
        }

        private byte[] GetFileData()
        {
            using (var stream = new MemoryStream())
            {
                File.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}
