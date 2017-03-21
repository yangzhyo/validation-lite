namespace Validation.Lite
{
    public class LengthValidator : IValidator
    {
        private int _minLength;
        private int _maxLength;

        public bool IsValid { get; set; }
        public string Message { get; set; }

        public LengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public void Validate(ValidationContext context)
        {
            string value = context.Value as string;
            value = value ?? "";
            int length = value.Length;

            if (length < _minLength || length > _maxLength)
            {
                IsValid = false;
                if (_minLength == _maxLength)
                {
                    Message = $"Length of {context.Name} should be {_maxLength}.";
                }
                else
                {
                    Message = $"Length of {context.Name} should between {_minLength} and {_maxLength}.";
                }
                
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
