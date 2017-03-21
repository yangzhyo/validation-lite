using System.Collections.Generic;

namespace Validation.Lite
{
    public class NotEmptyValidator : IValidator
    {
        public bool IsValid { get; set; }

        public List<string> Messages { get; set; }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            string value = context.Value as string;
            if (string.IsNullOrWhiteSpace(value))
            {
                IsValid = false;
                Messages.Add($"{context.Name} should not be empty.");
            }
        }
    }
}
