using System.ComponentModel.DataAnnotations;

namespace ManagementBudget.Validations
{
    public class FirstUpperAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLeter = value.ToString()[0].ToString();

            if (firstLeter != firstLeter.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayuscula");
            }
            return ValidationResult.Success;
        }
    }
}
