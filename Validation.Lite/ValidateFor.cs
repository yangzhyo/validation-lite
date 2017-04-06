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

        //public EntityValidationRule<T> EnumerableEntity<TEntity, TElement>()
        //    where TEntity : IEnumerable<TElement>
        //{
        //    var rule = new EnumerableEntityValidationRule<T>(typeof(T).Name, this);
        //    _validationRules.Add(rule);
        //    return rule;
        //}

        public PropertyValidationRule<T, TProperty> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            if (fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                string entityName = typeof(T).Name;
                string propertyName = memberExpression.Member.Name;

                Func<T, TProperty> getFieldFunc = fieldExpression.Compile();

                var rule = new PropertyValidationRule<T, TProperty>(entityName, this, propertyName, getFieldFunc);
                _validationRules.Add(rule);
                return rule;
            }
            else
            {
                throw new Exception("Field<TProperty> method only support member access.");
            }
        }

        public EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement> EnumerableField<TEnumerableProperty, TElement>(Expression<Func<T, TEnumerableProperty>> fieldExpression)
            where TEnumerableProperty : IEnumerable<TElement>
        {
            if (fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                string entityName = typeof(T).Name;
                string propertyName = memberExpression.Member.Name;

                Func<T, TEnumerableProperty> getFieldFunc = fieldExpression.Compile();

                var rule = new EnumerablePropertyValidationRule<T, TEnumerableProperty, TElement>(entityName, this, propertyName, getFieldFunc);
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
                    ValidationResult result = rule.Validate(target);
                    finalResult.MergeValidationResult(result);
                }
            }

            return finalResult;
        }
    }
}