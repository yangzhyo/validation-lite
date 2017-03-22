using System.Collections;

namespace Validation.Lite
{
    public class HaveDataValidator : IValidator
    {
        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            var collection = context.ValidateObjectValue as ICollection;
            if (collection == null || collection.Count == 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should have data.");
            }

            return result;
        }
    }
}
