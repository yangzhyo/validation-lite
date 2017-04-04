using System.Collections;

namespace Validation.Lite
{
    public class HaveDataValidator<T> : IValidator<T>
        //where T : ICollection
    {
        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            var collection = value as ICollection;
            if (collection == null || collection.Count == 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{typeof(T)} should have data.");
            }

            return result;
        }
    }
}
