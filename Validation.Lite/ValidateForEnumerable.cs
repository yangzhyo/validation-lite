using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class ValidateForEnumerable<T, TElement>
        where T : IEnumerable<TElement>
    {
        private readonly ICollection<IValidator<T>> _validators = new List<IValidator<T>>();

        private void AddValidator(IValidator<T> validator)
        {
            validator.ValidationName = typeof(T).Name;
            _validators.Add(validator);
        }

        public ValidationResult Validate(T entiy)
        {
            ValidationResult finalResult = ValidationResult.Valid;
            foreach (IValidator<T> validator in _validators)
            {
                ValidationResult result = validator.Validate(entiy);
                finalResult.MergeResult(result);
            }

            return finalResult;
        }

        public ValidateForEnumerable<T, TElement> ShouldHaveData()
        {
            AddValidator(new HaveDataValidator<T, TElement>());
            return this;
        }

        public ValidateForEnumerable<T, TElement> ValidateElementWith(ValidateFor<TElement> validateFor)
        {
            AddValidator(new NestedEnumerableValidator<T, TElement>(validateFor));
            return this;
        }

        public ValidateForEnumerable<T, TElement> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }
    }
}
