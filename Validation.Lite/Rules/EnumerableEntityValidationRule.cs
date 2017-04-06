using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class EnumerableEntityValidationRule<T, TElement> : ValidationRule<T>
        where T : IEnumerable<TElement>
    {
        private ICollection<IValidator<T>> Validators { get; }

        public EnumerableEntityValidationRule(string entityName, ValidateFor<T> validationFor) 
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

        public EnumerableEntityValidationRule<T, TElement> ShouldHaveData()
        {
            AddValidator(new HaveDataValidator<T, TElement>());
            return this;
        }

        public EnumerableEntityValidationRule<T, TElement> ValidateWith(ValidateFor<TElement> validateFor)
        {
            AddValidator(new NestedEnumerableValidator<T, TElement>(validateFor));
            return this;
        }

        public EnumerableEntityValidationRule<T, TElement> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
