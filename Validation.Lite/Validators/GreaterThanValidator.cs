using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Lite
{
    public class GreaterThanValidator : IValidator
    {
        private IComparable _factor;

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public GreaterThanValidator(IComparable factor)
        {
            _factor = factor;
        }

        public void Validate(ValidationContext context)
        {
            IComparable value = context.Value as IComparable;

            if (value == null || value.CompareTo(_factor) <= 0)
            {
                IsValid = false;
                Message = $"{context.Name} should be greater than {_factor}.";
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
