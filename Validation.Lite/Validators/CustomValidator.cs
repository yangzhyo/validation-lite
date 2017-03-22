using System;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator
    {
        public Func<T, ValidationResult> CustomValidateFunc { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            CustomValidateFunc = customValidateFunc;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            return CustomValidateFunc((T) context.ValidateObjectValue);
        }
    }
}
