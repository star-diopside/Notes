using System.ComponentModel.DataAnnotations;

namespace Notes.Web.Validators;

public class FileNameValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string v)
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
