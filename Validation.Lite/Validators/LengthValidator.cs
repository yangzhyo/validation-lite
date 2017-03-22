namespace Validation.Lite
{
    public class LengthValidator : IValidator
    {
        private int _minLength;
        private int _maxLength;

        public LengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            string value = context.ValidateObjectValue as string;
            value = value ?? "";
            int length = value.Length;

            if (length < _minLength || length > _maxLength)
            {
                result.IsValid = false;
                if (_minLength == _maxLength)
                {
                    result.ErrorMessages.Add($"Length of {context.ValidateObjectName} should be {_maxLength}.");
                }
                else
                {
                    result.ErrorMessages.Add($"Length of {context.ValidateObjectName} should between {_minLength} and {_maxLength}.");
                }
            }

            return result;
        }
    }
}
