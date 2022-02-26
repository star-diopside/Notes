using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Models;

public class UploadFile
{
    public int Id { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string ContentType { get; set; } = string.Empty;

    [Required]
    public long Length { get; set; }

    [Required]
    [MaxLength(64)]
    public string HashValue { get; set; } = string.Empty;

    [Timestamp]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("xmin", TypeName = "xid")]
    public uint Version { get; set; }

    public virtual UploadFileData? UploadFileData { get; set; }
}
