using System.ComponentModel.DataAnnotations;

namespace ISSHAR.Application.Attributes
{
    public class LaterThanNowAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            if (currentValue <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
