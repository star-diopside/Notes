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
        public string ContentType { get; set; }
    }
}
