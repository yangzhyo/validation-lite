namespace Validation.Lite
{
    public class NotEmptyValidator<T> : IValidator<T>
    {
        public string ValidationName { get; set; }

        public ValidationResult Validate(T value)
        {
            string str = value?.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, $"{ValidationName} should not be empty.");
            }

            return ValidationResult.Valid;
        }
    }
}
