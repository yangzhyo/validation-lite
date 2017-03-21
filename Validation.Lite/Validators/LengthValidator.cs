using System.Collections.Generic;

namespace Validation.Lite
{
    public class LengthValidator : IValidator
    {
        private int _minLength;
        private int _maxLength;

        public bool IsValid { get; set; }
        public List<string> Messages { get; set; }

        public LengthValidator(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            string value = context.Value as string;
            value = value ?? "";
            int length = value.Length;

            if (length < _minLength || length > _maxLength)
            {
                IsValid = false;
                if (_minLength == _maxLength)
                {
                    Messages.Add($"Length of {context.Name} should be {_maxLength}.");
                }
                else
                {
                    Messages.Add($"Length of {context.Name} should between {_minLength} and {_maxLength}.");
                }
            }
        }
    }
}
