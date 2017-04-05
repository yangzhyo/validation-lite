using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class EntityValidationRule<T> : ValidationRule<T>
    {
        private ICollection<IValidator<T>> Validators { get; }

        public EntityValidationRule(string ruleName, ValidateFor<T> validationFor)
            : base(ruleName, validationFor)
        {
            Validators = new List<IValidator<T>>();
        }

        private void AddValidator(IValidator<T> validator)
        {
            validator.ValidationName = _ruleName;
            Validators.Add(validator);
        }

        public override ValidationResult Validate1(T entiy)
        {
            ValidationResult finalResult = new ValidationResult();
            foreach (IValidator<T> validator in Validators)
            {
                ValidationResult result = validator.Validate(entiy);
                finalResult.MergeValidationResult(result);
            }

            return finalResult;
        }

        public EntityValidationRule<T> ShouldHaveData()
        {
            AddValidator(new HaveDataValidator<T>());
            return this;
        }

        public EntityValidationRule<T> ValidateWith(ValidateFor<T> validateFor)
        {
            AddValidator(new NestedValidator<T>(validateFor));
            return this;
        }

        public EntityValidationRule<T> ValidateWith<TEntity>(ValidateFor<TEntity> validateFor)
        {
            AddValidator(new NestedListValidator<T, TEntity>(validateFor));
            return this;
        }

        public EntityValidationRule<T> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
