namespace Validation.Lite
{
    public class NotNullValidator<T> : IValidator<T>
    {
        public string ValidationName { get; set; }

        public ValidationResult Validate(T value)
        {
            if (value == null)
            {
                return new ValidationResult(false, $"{ValidationName} should not be null.");
            }

            return ValidationResult.Valid;
        }
    }
}
