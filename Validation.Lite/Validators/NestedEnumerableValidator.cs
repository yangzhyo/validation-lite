using System.Collections.Generic;
using System.Linq;

namespace Validation.Lite
{
    public class NestedEnumerableValidator<T, TElement> : IValidator<T>
        where T : IEnumerable<TElement>
    {
        private ValidateFor<TElement> _validateFor;

        public string ValidationName { get; set; }

        public NestedEnumerableValidator(ValidateFor<TElement> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(T value)
        {
            var enumerable = (IEnumerable<TElement>) value;
            using (var enumerator = enumerable.GetEnumerator())
            {
                int i = 0;
                while (enumerator.MoveNext())
                {
                    i++;
                    var entity = enumerator.Current;
                    ValidationResult result = _validateFor.Validate(entity);

                    if (!result.IsValid)
                    {
                        result.ErrorMessages =
                            result.ErrorMessages.Select(m => $"{ValidationName} Collection@{i}:{m}").ToList();
                        // Prevent too much failures. Just return the first failure validation.
                        return result;
                    }
                }
            }

            return new ValidationResult();
        }
    }
}
