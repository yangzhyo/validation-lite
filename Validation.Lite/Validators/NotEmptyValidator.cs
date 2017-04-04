namespace Validation.Lite
{
    public class NotEmptyValidator<T> : IValidator<T>
    {
        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            string str = value?.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{typeof(T)} should not be empty.");
            }

            return result;
        }
    }
}
