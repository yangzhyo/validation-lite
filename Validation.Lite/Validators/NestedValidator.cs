using System;

namespace Validation.Lite
{
    public class NestedValidator<T> : IValidator<T>
    {
        private ValidateFor<T> _validateFor;

        public string ValidationName { get; set; }

        public NestedValidator(ValidateFor<T> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(T value)
        {
            if (_validateFor == null)
            {
                return ValidationResult.Valid;
            }

            return _validateFor.Validate(value);
        }
    }
}
