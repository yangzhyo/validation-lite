using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class PropertyValidationRule<T, TProperty> : ValidationRule<T>
    {
        private Func<T, TProperty> _getValueFunc = null;
        private ICollection<IValidator<TProperty>> _validators = null;

        public PropertyValidationRule(ValidateFor<T> validateFor, Func<T, TProperty> getValueFunc)
            : base(validateFor)
        {
            _getValueFunc = getValueFunc;
            _validators = new List<IValidator<TProperty>>();
        }

        private void AddValidator(IValidator<TProperty> validator)
        {
            _validators.Add(validator);
        }

        public override ValidationResult Validate(T entiy)
        {
            TProperty property = _getValueFunc.Invoke(entiy);

            ValidationResult finalResult = new ValidationResult();
            foreach (IValidator<TProperty> validator in _validators)
            {
                ValidationResult result = validator.Validate(property);
                finalResult.MergeValidationResult(result);
            }

            return finalResult;
        }

        public PropertyValidationRule<T, TProperty> ShouldNotNull()
        {
            AddValidator(new NotNullValidator<TProperty>());
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldNotEmpty()
        {
            AddValidator(new NotEmptyValidator<TProperty>());
            return this;
        }

        public PropertyValidationRule<T, TProperty> Length(int exactLength)
        {
            return Length(exactLength, exactLength);
        }

        public PropertyValidationRule<T, TProperty> Length(int minLength, int maxLength)
        {
            AddValidator(new LengthValidator<TProperty>(minLength, maxLength));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldGreaterThan(TProperty factor)
        {
            AddValidator(new GreaterThanValidator<TProperty>(factor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldGreaterThanOrEqualTo(TProperty factor)
        {
            AddValidator(new GreaterThanOrEqualToValidator<TProperty>(factor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldLessThan(TProperty factor)
        {
            AddValidator(new LessThanValidator<TProperty>(factor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldLessThanOrEqualTo(TProperty factor)
        {
            AddValidator(new LessThanOrEqualToValidator<TProperty>(factor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldEqualTo(TProperty factor)
        {
            AddValidator(new EqualToValidator<TProperty>(factor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldHaveData()
        {
            AddValidator(new HaveDataValidator<TProperty>());
            return this;
        }

        public PropertyValidationRule<T, TProperty> ValidateWith(ValidateFor<TProperty> validateFor)
        {
            AddValidator(new NestedValidator<TProperty>(validateFor));
            return this;
        }

        public PropertyValidationRule<T, TProperty> ShouldPassCustomCheck(Func<TProperty, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<TProperty>(customCheckFunc));
            return this;
        }
    }
}
