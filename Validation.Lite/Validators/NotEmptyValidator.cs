using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Lite
{
    public class NotEmptyValidator : IValidator
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public void Validate(ValidationContext context)
        {
            string value = context.Value as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                IsValid = false;
                Message = $"{context.Name} should not be empty.";
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
