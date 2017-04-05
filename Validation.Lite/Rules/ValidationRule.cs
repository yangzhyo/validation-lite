using System;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public abstract class ValidationRule<T>
    {
        private ValidateFor<T> _validateFor = null;
        protected string _ruleName = null;

        protected ValidationRule(string ruleName, ValidateFor<T> validationFor)
        {
            _ruleName = ruleName;
            _validateFor = validationFor;
        }

        public abstract ValidationResult Validate1(T entiy);

        public ValidateFor<T> Build()
        {
            return _validateFor;
        }

        public PropertyValidationRule<T, TProperty> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            return _validateFor.Field(fieldExpression);
        }

        public EntityValidationRule<T> Entity()
        {
            return _validateFor.Entity();
        }
    }
}
