﻿using Microsoft.AspNetCore.Http;
using Notes.Data.Models;
using Notes.Web.Validators;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        private static async Task<byte[]> GetFileDataAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}
