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
            var rule = new EntityValidationRule<T>(typeof(T).Name, this);
            _validationRules.Add(rule);
            return rule;
        }

        public PropertyValidationRule<T, TProperty> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            if (fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                string ruleName = $"{typeof(T).Name}.{memberExpression.Member.Name}";

                Func<T, TProperty> getFieldFunc = fieldExpression.Compile();

                var rule = new PropertyValidationRule<T, TProperty>(ruleName, this, getFieldFunc);
                _validationRules.Add(rule);
                return rule;
            }
            else
            {
                throw new Exception("Field<TProperty> method only support member access.");
            }
        }

        public ValidationResult Validate(T target)
        {
            ValidationResult finalResult = new ValidationResult();

            if(_validationRules.Count > 0)
            {
                foreach(var rule in _validationRules)
                {
                    ValidationResult result = rule.Validate1(target);
                    finalResult.MergeValidationResult(result);
                }
            }

            return finalResult;
        }
    }
}