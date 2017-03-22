namespace Validation.Lite
{
    public class NotEmptyValidator : IValidator
    {
        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            string value = context.ValidateObjectValue as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should not be empty.");
            }

            return result;
        }
    }
}
