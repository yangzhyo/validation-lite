using System.Collections;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class HaveDataValidator : IValidator
    {
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            var collection = context.Value as ICollection;
            if (collection == null || collection.Count == 0)
            {
                IsValid = false;
                Messages.Add($"{context.Name} should have data.");
            }
        }
    }
}
