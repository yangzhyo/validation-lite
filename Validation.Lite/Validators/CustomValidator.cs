using System;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator<T>
    {
        public Func<T, ValidationResult> CustomValidateFunc { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            CustomValidateFunc = customValidateFunc;
        }

        public ValidationResult Validate(T value)
        {
            return CustomValidateFunc(value);
        }
    }
}
