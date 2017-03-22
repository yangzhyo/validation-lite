using System;

namespace Validation.Lite
{
    public class EqualToValidator : IValidator
    {
        private IComparable _factor;

        public EqualToValidator(IComparable factor)
        {
            _factor = factor;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            IComparable value = context.ValidateObjectValue as IComparable;

            if (value == null || value.CompareTo(_factor) != 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should be equal to {_factor}.");
            }

            return result;
        }
    }
}
