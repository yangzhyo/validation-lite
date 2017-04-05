namespace Validation.Lite
{
    public class LengthValidator<T> : IValidator<T>
    {
        private int _minLength;
        private int _maxLength;

        public string ValidationName { get; set; }

        public LengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            string str = value?.ToString();
            
            int length = 0;
            if (str != null)
            {
                length = str.Length;
            }

            if (length < _minLength || length > _maxLength)
            {
                result.IsValid = false;
                if (_minLength == _maxLength)
                {
                    result.ErrorMessages.Add($"Length of {ValidationName} should be {_maxLength}.");
                }
                else
                {
                    result.ErrorMessages.Add($"Length of {ValidationName} should between {_minLength} and {_maxLength}.");
                }
            }

            return result;
        }
    }
}
