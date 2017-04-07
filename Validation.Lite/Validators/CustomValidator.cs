using System;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator<T>
    {
        private readonly Func<T, ValidationResult> _customValidateFunc;
        public string ValidationName { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            _customValidateFunc = customValidateFunc;
        }

        public ValidationResult Validate(T value)
        {
            if (_customValidateFunc == null)
            {
                return ValidationResult.Valid;
            }

            return _customValidateFunc(value);
        }
    }
}
