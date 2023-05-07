using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Validations
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }


            if (value.ToString().Length < 8)
            {
                return new ValidationResult("The Password must be at least 8 Characters");
            }
            return ValidationResult.Success;
        }

    }
}
