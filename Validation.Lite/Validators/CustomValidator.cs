using System;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator<T>
    {
        private Func<T, ValidationResult> _customValidateFunc { get; }
        public string ValidationName { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            _customValidateFunc = customValidateFunc;
        }

        public ValidationResult Validate(T value)
        {
            return _customValidateFunc(value);
        }
    }
}
