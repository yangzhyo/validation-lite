using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public abstract class ValidationRule
    {
        public string ValidateObjectName { get; set; }
        public Type ValidateObjectType { get; set; }
        public IList<IValidator> Validators { get; set; }

        protected ValidationRule(string validateObjectName, Type validateObjectType)
        {
            ValidateObjectName = validateObjectName;
            ValidateObjectType = validateObjectType;
            Validators = new List<IValidator>();
        }

        public void AddValidator(IValidator validator)
        {
            Validators.Add(validator);
        }

        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult finalResult = new ValidationResult();
            foreach (IValidator validator in Validators)
            {
                ValidationResult result = validator.Validate(context);
                finalResult.MergeValidationResult(result);
            }

            return finalResult;
        }

        public abstract object GetValidateObjectValue(object obj);
    }
}
