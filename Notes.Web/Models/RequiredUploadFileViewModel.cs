using System.ComponentModel.DataAnnotations;

namespace Notes.Web.Models;

public class RequiredUploadFileViewModel : UploadFileViewModel
{
    [Required(ErrorMessage = "The {0} field is required.")]
    public override IFormFile? File { get => base.File; set => base.File = value; }
}
