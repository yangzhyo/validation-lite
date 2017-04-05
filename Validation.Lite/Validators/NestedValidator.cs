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
            return _validateFor.Validate(value);
        }
    }
}
