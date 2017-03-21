using System.Collections.Generic;
using System.Linq;

namespace Validation.Lite
{
    public class NestedValidator<T> : IValidator
    {
        private ValidateFor<T> _validateFor;

        public bool IsValid { get; set; }

        public List<string> Messages { get; set; }

        public NestedValidator(ValidateFor<T> validateFor)
        {
            _validateFor = validateFor;
        }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            if(context.Value == null)
            {
                IsValid = false;
                Messages.Add($"{context.Name} should not be null.");
                return;
            }

            if (typeof(IEnumerable<T>).IsAssignableFrom(context.Value.GetType()))
            {
                var entityCollections = ((IEnumerable<T>)context.Value).GetEnumerator();
                int i = 0;
                while (entityCollections.MoveNext())
                {
                    i++;
                    var entity = entityCollections.Current;
                    ValidationResult result = _validateFor.Validate(entity);
                    
                    if (!result.IsValid)
                    {
                        IsValid = false;
                        Messages.AddRange(result.ErrorMessages.Select(m => $"Collection@{i}:{m}"));
                    }

                    if(!IsValid)
                    {
                        // Prevent too much failures. Just return the first failure validation.
                        break;
                    }
                }
            }
            else
            {
                ValidationResult result = _validateFor.Validate((T)context.Value);
                if (!result.IsValid)
                {
                    IsValid = false;
                    Messages.AddRange(result.ErrorMessages);
                }
            }
        }
    }
}
