using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> : ValidationRule<T>
        where TEnumerableProperty : IEnumerable<TElement>
    {
        private Func<T, TEnumerableProperty> _getPropertyValueFunc;
        private ICollection<IValidator<TEnumerableProperty>> _validators;
        private string _propertyName;

        public EnumerablePropertyValidationRule(string entityName, ValidateFor<T> validationFor, string propertyName, Func<T, TEnumerableProperty> getPropertyValueFunc) 
            : base(entityName, validationFor)
        {
            _propertyName = propertyName;
            _getPropertyValueFunc = getPropertyValueFunc;
            _validators = new List<IValidator<TEnumerableProperty>>();
        }

        private void AddValidator(IValidator<TEnumerableProperty> validator)
        {
            validator.ValidationName = $"{EntityName}.{_propertyName}";
            _validators.Add(validator);
        }

        internal override ValidationResult Validate(T entiy)
        {
            if (entiy == null)
            {
                return new ValidationResult(false, $"{EntityName} should not be null.");
            }

            TEnumerableProperty property = _getPropertyValueFunc.Invoke(entiy);

            ValidationResult finalResult = ValidationResult.Valid;
            foreach (IValidator<TEnumerableProperty> validator in _validators)
            {
                ValidationResult result = validator.Validate(property);
                finalResult.MergeResult(result);
            }

            return finalResult;
        }

        public EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> ShouldHaveData()
        {
            AddValidator(new HaveDataValidator<TEnumerableProperty, TElement>());
            return this;
        }

        public EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> ValidateWith(ValidateFor<TElement> validateFor)
        {
            AddValidator(new NestedEnumerableValidator<TEnumerableProperty, TElement>(validateFor));
            return this;
        }

        public EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> ShouldPassCustomCheck(Func<TEnumerableProperty, ValidationResult> customCheckFunc)
        {
            AddValidator(new CustomValidator<TEnumerableProperty>(customCheckFunc));
            return this;
        }
    }
}
