using Microsoft.AspNetCore.Http;
using Notes.Data.Models;
using Notes.Web.Validators;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Web.Models
{
    public class UploadFileViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "File Name")]
        [FileNameValidation(ErrorMessage = "The {0} field is using illegal characters.")]
        public string? FileName { get; set; }

        [Display(Name = "Content Type")]
        public string? ContentType { get; set; }

        [Display(Name = "File Size")]
        [DisplayFormat(DataFormatString = "{0:#,#}")]
        public long Length { get; set; }

        [Display(Name = "Hash Value")]
        public string? HashValue { get; set; }

        [Display(Name = "File")]
        public virtual IFormFile? File { get; set; }

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
            HashValue = uploadFile.HashValue;
            Version = uploadFile.Version;
        }

        public ValueTask<UploadFile> ToUploadFileAsync() => UpdateUploadFileAsync(new UploadFile());

        public async ValueTask<UploadFile> UpdateUploadFileAsync(UploadFile uploadFile)
        {
            uploadFile.FileName = GetFileName() ?? uploadFile.FileName;
            uploadFile.Version = Version;

            if (File != null)
            {
                uploadFile.UploadFileData ??= new UploadFileData();
                uploadFile.ContentType = File.ContentType;
                uploadFile.Length = File.Length;
                uploadFile.HashValue = await GetFileHashValueAsync(File);
                uploadFile.UploadFileData.Data = await GetFileDataAsync(File);
            }

            return uploadFile;
        }

        private string? GetFileName()
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
                    name = Path.GetFileNameWithoutExtension(name) + Path.GetExtension(File?.FileName);
                }

                return name;
            }
        }

        private static async Task<string> GetFileHashValueAsync(IFormFile file)
        {
            using var algorithm = SHA256.Create();
            using var stream = file.OpenReadStream();
            var hash = await Task.Run(() => algorithm.ComputeHash(stream));
            return hash.Select(b => b.ToString("x2"))
                       .Aggregate(new StringBuilder(hash.Length * 2),
                                  (sb, s) => sb.Append(s),
                                  sb => sb.ToString());
        }

        private static async Task<byte[]> GetFileDataAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}
