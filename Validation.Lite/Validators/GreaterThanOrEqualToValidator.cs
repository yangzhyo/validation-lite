using System;

namespace Validation.Lite
{
    public class GreaterThanOrEqualToValidator : IValidator
    {
        private IComparable _factor;

        public GreaterThanOrEqualToValidator(IComparable factor)
        {
            _factor = factor;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            IComparable value = context.ValidateObjectValue as IComparable;

            if (value == null || value.CompareTo(_factor) < 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should be greater than or equal to {_factor}.");
            }

            return result;
        }
    }
}
