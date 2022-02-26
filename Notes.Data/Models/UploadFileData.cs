using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Models;

public class UploadFileData
{
    public int Id { get; set; }

    public int UploadFileId { get; set; }

    public byte[]? Data { get; set; }

    [Timestamp]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("xmin", TypeName = "xid")]
    public uint Version { get; set; }

    public virtual UploadFile? UploadFile { get; set; }
}
