namespace Validation.Lite
{
    public class LengthValidator<T> : IValidator<T>
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public string ValidationName { get; set; }

        public LengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public ValidationResult Validate(T value)
        {
            string str = value?.ToString();
            
            int length = 0;
            if (str != null)
            {
                length = str.Length;
            }

            if (length >= _minLength && length <= _maxLength)
            {
                return ValidationResult.Valid;
            }

            if (_minLength == _maxLength)
            {
                return new ValidationResult(false, $"Length of {ValidationName} should be {_maxLength}.");
            }

            return new ValidationResult(false, $"Length of {ValidationName} should between {_minLength} and {_maxLength}.");
        }
    }
}
