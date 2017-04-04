using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Validation.Lite
{
    public class NestedValidator<T> : IValidator<T>
    {
        private ValidateFor<T> _validateFor;

        public NestedValidator(ValidateFor<T> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(T value)
        {
            if (value is IEnumerable)
            {
                var enumerable = value as IEnumerable<T>;
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
                return _validateFor.Validate(value);
            }
        }
    }
}
