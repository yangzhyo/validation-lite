using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public class ValidateFor<T>
    {
        private List<ValidationRule> _validationRules = new List<ValidationRule>();

        private ValidationRule CurrentValidationRule
        {
            get
            {
                if (_validationRules.Count == 0)
                {
                    throw new Exception("No validation rule was set.");
                }

                return _validationRules[_validationRules.Count - 1];
            }
        }

        public ValidateFor<T> Entity()
        {
            _validationRules.Add(new EntityValidationRule(typeof(T).ToString(), typeof(T)));
            return this;
        }

        public ValidateFor<T> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            Func<T, TProperty> getFieldFunc = fieldExpression.Compile();
            Func<object, object> weakTypeGetFieldFunc = x => getFieldFunc((T)x);

            if(fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                string ruleName = memberExpression.Member.Name;
                Type valueType = typeof(TProperty);

                _validationRules.Add(new PropertyValidationRule(ruleName, valueType, weakTypeGetFieldFunc));
                return this;
            }
            else
            {
                throw new Exception("Field<TProperty> method only support member access.");
            }
        }

        public ValidateFor<T> ShouldNotNull()
        {
            CurrentValidationRule.AddValidator(new NotNullValidator());
            return this;
        }

        public ValidateFor<T> ShouldNotEmpty()
        {
            if (CurrentValidationRule.ValueType != typeof(string))
            {
                throw new Exception("ShouldNotEmpty method only support string type.");
            }

            CurrentValidationRule.AddValidator(new NotEmptyValidator());
            return this;
        }

        public ValidateFor<T> ShouldGreaterThan(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValueType))
            {
                throw new Exception("ShouldGreaterThan method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new GreaterThanValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldPassCustomCheck(Func<T, ValidationResult> customCheckFunc)
        {
            if (!(CurrentValidationRule is EntityValidationRule))
            {
                throw new Exception("ShouldPassCustomCheck method only support entity validation rule.");
            }

            CurrentValidationRule.AddValidator(new CustomValidator<T>(customCheckFunc));
            return this;
        }

        public ValidationResult Validate(T target)
        {
            ValidationResult result = new ValidationResult();

            if(_validationRules.Count > 0)
            {
                foreach(var rule in _validationRules)
                {
                    ValidationContext context = new ValidationContext(rule.RuleName, rule.GetValidateValue(target));

                    foreach(var validator in rule.Validators)
                    {
                        validator.Validate(context);
                        if(!validator.IsValid)
                        {
                            result.IsValid = false;
                            result.ErrorMessages.Add(validator.Message);
                        }
                    }
                }
            }

            return result;
        }
    }
}