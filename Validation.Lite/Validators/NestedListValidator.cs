using System.Collections.Generic;
using System.Linq;

namespace Validation.Lite
{
    public class NestedListValidator<T, TElement> : IValidator<T>
    {
        private ValidateFor<TElement> _validateFor;

        public string ValidationName { get; set; }

        public NestedListValidator(ValidateFor<TElement> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(T value)
        {
            var enumerable = value as IEnumerable<TElement>;
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
                        result.ErrorMessages = result.ErrorMessages.Select(m => $"{ValidationName} Collection@{i}:{m}").ToList();
                        // Prevent too much failures. Just return the first failure validation.
                        entityCollections.Dispose();
                        return result;
                    }
                }
                entityCollections.Dispose();
            }

            return new ValidationResult();
        }
    }
}
