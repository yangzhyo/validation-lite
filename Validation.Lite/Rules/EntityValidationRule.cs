using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class EntityValidationRule<T> : ValidationRule<T>
    {
        private ICollection<IValidator<T>> Validators { get; }

        public EntityValidationRule(string entityName, ValidateFor<T> validationFor)
            : base(entityName, validationFor)
        {
            Validators = new List<IValidator<T>>();
        }

        private void AddValidator(IValidator<T> validator)
        {
            validator.ValidationName = EntityName;
            Validators.Add(validator);
        }

        internal override ValidationResult Validate(T entiy)
        {
            ValidationResult finalResult = ValidationResult.Valid;
            foreach (IValidator<T> validator in Validators)
            {
                ValidationResult result = validator.Validate(entiy);
                finalResult.MergeResult(result);
            }

            return finalResult;
        }

        public EntityValidationRule<T> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
