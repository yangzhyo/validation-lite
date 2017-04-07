using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class NestedEnumerableValidator<T, TElement> : IValidator<T>
        where T : IEnumerable<TElement>
    {
        private readonly ValidateFor<TElement> _validateFor;

        public string ValidationName { get; set; }

        public NestedEnumerableValidator(ValidateFor<TElement> validateFor)
        {
            _validateFor = validateFor;
        }

        public ValidationResult Validate(T value)
        {
            if (_validateFor == null || value == null)
            {
                return ValidationResult.Valid;
            }

            var enumerable = (IEnumerable<TElement>) value;
            using (var enumerator = enumerable.GetEnumerator())
            {
                int i = 0;
                while (enumerator.MoveNext())
                {
                    i++;
                    var entity = enumerator.Current;
                    ValidationResult result = _validateFor.Validate(entity);

                    if (result.IsValid)
                    {
                        continue;
                    }

                    for (int j = 0; j < result.ErrorMessages.Count; j++)
                    {
                        result.ErrorMessages[j] = $"{ValidationName} Collection@{i}:{result.ErrorMessages[j]}";
                    }
                    // Prevent too much failures. Just return the first failure validation.
                    return result;
                }
            }

            return ValidationResult.Valid;
        }
    }
}
