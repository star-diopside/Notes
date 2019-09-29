using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Models
{
    [Table("upload_files")]
    public class UploadFile
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("file_name")]
        [Required]
        public string FileName { get; set; }

        [Column("content_type")]
        [Required]
        public string ContentType { get; set; }

        [Column("length")]
        [Required]
        public long Length { get; set; }

        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint Version { get; set; }

        public virtual UploadFileData UploadFileData { get; set; }
    }
}
