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
            ValidationResult finalResult = new ValidationResult();
            foreach (IValidator<T> validator in Validators)
            {
                ValidationResult result = validator.Validate(entiy);
                finalResult.MergeValidationResult(result);
            }

            return finalResult;
        }

        public EntityValidationRule<T> ValidateWith(ValidateFor<T> validateFor)
        {
            AddValidator(new NestedValidator<T>(validateFor));
            return this;
        }

        public EntityValidationRule<T> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
