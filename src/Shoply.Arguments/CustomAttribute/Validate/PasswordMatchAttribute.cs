using System.ComponentModel.DataAnnotations;

namespace Shoply.Arguments.CustomAttribute.Validate
{
    public class PasswordMatchAttribute : ValidationAttribute
    {
        public string PasswordProperty { get; set; }

        public PasswordMatchAttribute(string passwordProperty)
        {
            PasswordProperty = passwordProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(PasswordProperty);
            if (passwordProperty == null)
            {
                return new ValidationResult($"Propriedade {PasswordProperty} não encontrada.");
            }

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null);
            if (passwordValue != null && passwordValue.ToString() != value?.ToString())
            {
                return new ValidationResult("As senhas não coincidem.");
            }

            return ValidationResult.Success!;
        }
    }
}
