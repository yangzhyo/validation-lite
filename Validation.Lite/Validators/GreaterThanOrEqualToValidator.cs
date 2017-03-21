using System;

namespace Validation.Lite
{
    public class GreaterThanOrEqualToValidator : IValidator
    {
        private IComparable _factor;

        public bool IsValid { get; set; }
        public string Message { get; set; }

        public GreaterThanOrEqualToValidator(IComparable factor)
        {
            _factor = factor;
        }

        public void Validate(ValidationContext context)
        {
            IComparable value = context.Value as IComparable;

            if (value == null || value.CompareTo(_factor) < 0)
            {
                IsValid = false;
                Message = $"{context.Name} should be greater than or equal to {_factor}.";
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
