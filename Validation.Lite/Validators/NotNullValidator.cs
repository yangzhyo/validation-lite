namespace Validation.Lite
{
    public class NotNullValidator<T> : IValidator<T>
    {
        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            if (value == null)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{typeof(T)} should not be null.");
            }

            return result;
        }
    }
}
