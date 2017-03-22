using System.Collections.Generic;

namespace Validation.Lite
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorMessages { get; set; }

        public ValidationResult()
        {
            IsValid = true;
            ErrorMessages = new List<string>();
        }

        public void MergeValidationResult(ValidationResult result)
        {
            if (!result.IsValid)
            {
                IsValid = false;
                ErrorMessages.AddRange(result.ErrorMessages);
            }
        }
    }
}