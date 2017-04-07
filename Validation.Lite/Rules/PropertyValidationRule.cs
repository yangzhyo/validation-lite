using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class PropertyValidationRule<T, TProperty> : ValidationRule<T>
    {
        private Func<T, TProperty> _getPropertyValueFunc;
        private ICollection<IValidator<TProperty>> _validators;
        private string _propertyName;

        public PropertyValidationRule(string entityName, ValidateFor<T> validateFor, string propertyName, Func<T, TProperty> getPropertyValueFunc)
            : base(entityName, validateFor)
        {
            _propertyName = propertyName;
            _getPropertyValueFunc = getPropertyValueFunc;
            _validators = new List<IValidator<TProperty>>();
        }

        private void AddValidator(IValidator<TProperty> validator)
        {
            validator.ValidationName = $"{EntityName}.{_propertyName}";
            _validators.Add(validator);
        }

        internal override ValidationResult Validate(T entiy)
        {
            if(entiy == null)
            {
                return new ValidationResult(false, $"{EntityName} should not be null.");
            }

            TProperty property = _getPropertyValueFunc.Invoke(entiy);

            ValidationResult finalResult = ValidationResult.Valid;
            foreach (IValidator<TProperty> validator in _validators)
            {
                ValidationResult result = validator.Validate(property);
                finalResult.MergeResult(result);
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
