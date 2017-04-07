using System.Collections.Generic;

namespace Validation.Lite
{
    public class HaveDataValidator<T, TElement> : IValidator<T>
        where T : IEnumerable<TElement>
    {
        public string ValidationName { get; set; }

        public ValidationResult Validate(T value)
        {
            if (value == null)
            {
                return new ValidationResult(false, $"{ValidationName} should have data.");
            }

            var enumerable = (IEnumerable<TElement>)value;
            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    return ValidationResult.Valid;
                }
            }

            return new ValidationResult(false, $"{ValidationName} should have data.");
        }
    }
}
