using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Data.Models
{
    [Table("upload_file_data")]
    public class UploadFileData
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("upload_file_id")]
        public int UploadFileId { get; set; }

        [Column("data")]
        public byte[] Data { get; set; }

        public virtual UploadFile UploadFile { get; set; }
    }
}
