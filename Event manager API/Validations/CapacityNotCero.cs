using System.ComponentModel.DataAnnotations;
namespace Event_manager_API.Validations
{
    public class CapacityNotCero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            
            if (int.Parse(value.ToString()) <= 0)
            {
                return new ValidationResult("The Capacity must be more than Cero");
            }
            return ValidationResult.Success;
        }

    }
}
