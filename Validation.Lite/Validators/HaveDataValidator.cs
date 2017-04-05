using System.Collections;

namespace Validation.Lite
{
    public class HaveDataValidator<T> : IValidator<T>
    {
        public string ValidationName { get; set; }

        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            var collection = value as ICollection;
            if (collection == null || collection.Count == 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{ValidationName} should have data.");
            }

            return result;
        }
    }
}
