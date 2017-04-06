using System;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public abstract class ValidationRule<T>
    {
        private ValidateFor<T> _validateFor;
        protected string EntityName;

        protected ValidationRule(string entityName, ValidateFor<T> validationFor)
        {
            EntityName = entityName;
            _validateFor = validationFor;
        }

        internal abstract ValidationResult Validate(T entiy);

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
