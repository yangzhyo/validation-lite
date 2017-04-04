using System;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public abstract class ValidationRule<T>
    {
        private ValidateFor<T> _validateFor = null;

        protected ValidationRule(ValidateFor<T> validationFor)
        {
            _validateFor = validationFor;
        }

        public abstract ValidationResult Validate(T entiy);

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
