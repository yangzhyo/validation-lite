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
            string entityName = string.Empty;
            string propertyName = string.Empty;

            if (fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                entityName = typeof(T).Name;
                propertyName = memberExpression.Member.Name;
            }
            else
            {
                entityName = fieldExpression.ToString();
            }

            Func<T, TProperty> getFieldFunc = fieldExpression.Compile();

            var rule = new PropertyValidationRule<T, TProperty>(entityName, this, propertyName, getFieldFunc);
            _validationRules.Add(rule);
            return rule;
        }

        public EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> EnumerableField<TEnumerableProperty, TElement>(Expression<Func<T, TEnumerableProperty>> fieldExpression)
            where TEnumerableProperty : IEnumerable<TElement>
        {
            string entityName = string.Empty;
            string propertyName = string.Empty;

            if (fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                entityName = typeof(T).Name;
                propertyName = memberExpression.Member.Name;
            }
            else
            {
                entityName = fieldExpression.ToString();
            }

            Func<T, TEnumerableProperty> getFieldFunc = fieldExpression.Compile();

            var rule = new EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement>(entityName, this, propertyName, getFieldFunc);
            _validationRules.Add(rule);
            return rule;
        }

        public ValidationResult Validate(T target)
        {
            ValidationResult finalResult = ValidationResult.Valid;

            if(_validationRules.Count > 0)
            {
                foreach(var rule in _validationRules)
                {
                    ValidationResult result = rule.Validate(target);
                    finalResult.MergeResult(result);
                }
            }

            return finalResult;
        }
    }
}