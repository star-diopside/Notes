using Microsoft.AspNetCore.Http;
using Notes.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Notes.Web.Models
{
    public class UploadFileViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("ファイル名")]
        public string FileName { get; set; }

        [DisplayName("コンテンツタイプ")]
        public string ContentType { get; set; }

        [DisplayName("ファイルサイズ")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public long Length { get; set; }

        public IFormFile File { get; set; }

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

        public UploadFile ToUploadFile() => new UploadFile()
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
