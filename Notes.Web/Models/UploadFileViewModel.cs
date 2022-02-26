using Notes.Data.Models;
using Notes.Web.Validators;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Notes.Web.Models;

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

        if (File is not null)
        {
            uploadFile.UploadFileData ??= new UploadFileData();
            uploadFile.ContentType = File.ContentType;
            uploadFile.Length = File.Length;
            (uploadFile.UploadFileData.Data, uploadFile.HashValue) = await GetFileDataAsync(File);
        }

        return uploadFile;
    }

    private string? GetFileName()
    {
        if (string.IsNullOrEmpty(FileName))
        {
            return File is null ? null : Path.GetFileName(File.FileName);
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

    private static async Task<(byte[] data, string hashValue)> GetFileDataAsync(IFormFile file)
    {
        byte[] data;
        string hashValue;

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            data = stream.ToArray();
        }

        using (var algorithm = SHA256.Create())
        {
            var hash = await Task.Run(() => algorithm.ComputeHash(data));
            hashValue = hash.Select(b => b.ToString("x2"))
                            .Aggregate(new StringBuilder(hash.Length * 2),
                                       (sb, s) => sb.Append(s),
                                       sb => sb.ToString());
        }

        return (data, hashValue);
    }
}
