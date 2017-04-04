using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public class ValidateFor<T>
    {
        private ICollection<ValidationRule<T>> _validationRules = new List<ValidationRule<T>>();

        public EntityValidationRule<T> Entity()
        {
            var rule = new EntityValidationRule<T>(this);
            _validationRules.Add(rule);
            return rule;
        }

        public PropertyValidationRule<T, TProperty> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            Func<T, TProperty> getFieldFunc = fieldExpression.Compile();
            var rule = new PropertyValidationRule<T, TProperty>(this, getFieldFunc);
            _validationRules.Add(rule);
            return rule;
        }

        public ValidationResult Validate(T target)
        {
            ValidationResult finalResult = new ValidationResult();

            if(_validationRules.Count > 0)
            {
                foreach(var rule in _validationRules)
                {
                    ValidationResult result = rule.Validate(target);
                    finalResult.MergeValidationResult(result);
                }
            }

            return finalResult;
        }
    }
}