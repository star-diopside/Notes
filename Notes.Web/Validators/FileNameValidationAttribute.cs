using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Notes.Web.Validators
{
    public class FileNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;

            if (v != null)
            {
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    if (v.Contains(c))
                    {
                        return new ValidationResult(null);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
