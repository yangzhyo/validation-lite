namespace Validation.Lite
{
    public class NotNullValidator : IValidator
    {
        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            if (context.ValidateObjectValue == null)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should not be null.");
            }

            return result;
        }
    }
}
