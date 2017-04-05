namespace Validation.Lite
{
    public class NotNullValidator<T> : IValidator<T>
    {
        public string ValidationName { get; set; }

        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            if (value == null)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{ValidationName} should not be null.");
            }

            return result;
        }
    }
}
