using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Validation.Lite
{
    public class NestedValidator<T> : IValidator
    {
        private ValidateFor<T> _validateFor;

        public NestedValidator(ValidateFor<T> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            if (context.ValidateObjectValue == null)
            {
                ValidationResult result = new ValidationResult {IsValid = false};
                result.ErrorMessages.Add($"{context.ValidateObjectName} should not be null.");
                return result;
            }

            if (context.ValidateObjectValue is IEnumerable)
            {
                var enumerable = context.ValidateObjectValue as IEnumerable<T>;
                if (enumerable != null)
                {
                    var entityCollections = enumerable.GetEnumerator();
                    int i = 0;
                    while (entityCollections.MoveNext())
                    {
                        i++;
                        var entity = entityCollections.Current;
                        ValidationResult result = _validateFor.Validate(entity);
                    
                        if (!result.IsValid)
                        {
                            result.ErrorMessages = result.ErrorMessages.Select(m => $"Collection@{i}:{m}").ToList();
                            // Prevent too much failures. Just return the first failure validation.
                            entityCollections.Dispose();
                            return result;
                        }
                    }
                    entityCollections.Dispose();
                }

                return new ValidationResult();
            }
            else
            {
                return _validateFor.Validate((T)context.ValidateObjectValue);
            }
        }
    }
}
