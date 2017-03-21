using System.Collections.Generic;

namespace Validation.Lite
{
    public class NotNullValidator : IValidator
    {
        public bool IsValid { get; set; }

        public List<string> Messages { get; set; }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            if(context.Value == null)
            {
                IsValid = false;
                Messages.Add($"{context.Name} should not be null.");
            }
        }
    }
}
