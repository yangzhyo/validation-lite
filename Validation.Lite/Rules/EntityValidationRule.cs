using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class EntityValidationRule<T> : ValidationRule<T>
    {
        private ICollection<IValidator<T>> Validators { get; }

        public EntityValidationRule(ValidateFor<T> validationFor)
            : base(validationFor)
        {
            Validators = new List<IValidator<T>>();
        }

        private void AddValidator(IValidator<T> validator)
        {
            Validators.Add(validator);
        }

        public override ValidationResult Validate(T entiy)
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

        //public EntityValidationRule<T> ValidateWith<TEntity>(ValidateFor<TEntity> validateFor)
        //{
        //    AddValidator(new NestedValidator<TEntity>(validateFor));
        //    return this;
        //}

        public EntityValidationRule<T> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
