using System.ComponentModel.DataAnnotations;

namespace Event_manager_API.Validations
{
    public class ValidRole : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }


            if (value.ToString().Equals("admin") || value.ToString().Equals("user"))
            {
                return ValidationResult.Success;
                
            }
            else
            {
                return new ValidationResult("Must be a valid role (user/admin)");
            }
            
        }

    }
}
