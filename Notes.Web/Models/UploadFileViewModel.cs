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

        public uint Version { get; set; }

        public UploadFileViewModel()
        {
        }

        public UploadFileViewModel(UploadFile uploadFile)
        {
            Id = uploadFile.Id;
            FileName = uploadFile.FileName;
            ContentType = uploadFile.ContentType;
            Length = uploadFile.Length;
        }

        public UploadFile ToUploadFile() => UpdateUploadFile(new UploadFile());

        public UploadFile UpdateUploadFile(UploadFile uploadFile)
        {
            uploadFile.FileName = GetFileName();

            if (File != null)
            {
                if (uploadFile.UploadFileData == null)
                {
                    uploadFile.UploadFileData = new UploadFileData();
                }

                uploadFile.ContentType = File.ContentType;
                uploadFile.Length = File.Length;
                uploadFile.UploadFileData.Data = GetFileData();
            }

            return uploadFile;
        }

        private string GetFileName()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return File == null ? null : Path.GetFileName(File.FileName);
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
