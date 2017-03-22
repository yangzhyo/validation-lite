using System;
using System.Collections;
using System.Collections.Generic;
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
            _validationRules.Add(new EntityValidationRule(typeof(T).Name, typeof(T)));
            return this;
        }

        public ValidateFor<T> Field<TProperty>(Expression<Func<T, TProperty>> fieldExpression)
        {
            Func<T, TProperty> getFieldFunc = fieldExpression.Compile();
            Func<object, object> weakTypeGetFieldFunc = x => getFieldFunc((T)x);

            if(fieldExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)fieldExpression.Body;
                string validateObjectName = $"{typeof(T).Name}.{memberExpression.Member.Name}";
                Type validateObjectType = typeof(TProperty);

                _validationRules.Add(new PropertyValidationRule(validateObjectName, validateObjectType, weakTypeGetFieldFunc));
                return this;
            }

            throw new Exception("Field<TProperty> method only support member access.");
        }

        public ValidateFor<T> ShouldNotNull()
        {
            CurrentValidationRule.AddValidator(new NotNullValidator());
            return this;
        }

        public ValidateFor<T> ShouldNotEmpty()
        {
            if (CurrentValidationRule.ValidateObjectType != typeof(string))
            {
                throw new Exception("ShouldNotEmpty method only support string type.");
            }

            CurrentValidationRule.AddValidator(new NotEmptyValidator());
            return this;
        }

        public ValidateFor<T> Length(int exactLength)
        {
            return Length(exactLength, exactLength);
        }

        public ValidateFor<T> Length(int minLength, int maxLength)
        {
            if (CurrentValidationRule.ValidateObjectType != typeof(string))
            {
                throw new Exception("Length method only support string type.");
            }

            CurrentValidationRule.AddValidator(new LengthValidator(minLength, maxLength));
            return this;
        }

        public ValidateFor<T> ShouldGreaterThan(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldGreaterThan method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new GreaterThanValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldGreaterThanOrEqualTo(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldGreaterThanOrEqualTo method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new GreaterThanOrEqualToValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldLessThan(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldLessThan method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new LessThanValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldLessThanOrEqualTo(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldLessThanOrEqualTo method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new LessThanOrEqualToValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldEqualTo(IComparable factor)
        {
            if (!typeof(IComparable).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldEqualTo method only support IComparable type.");
            }

            CurrentValidationRule.AddValidator(new EqualToValidator(factor));
            return this;
        }

        public ValidateFor<T> ShouldHaveData()
        {
            if (!typeof(ICollection).IsAssignableFrom(CurrentValidationRule.ValidateObjectType))
            {
                throw new Exception("ShouldHaveData method only support ICollection type.");
            }

            CurrentValidationRule.AddValidator(new HaveDataValidator());
            return this;
        }

        public ValidateFor<T> ValidateWith<TProperty>(ValidateFor<TProperty> validateFor)
        {
            CurrentValidationRule.AddValidator(new NestedValidator<TProperty>(validateFor));

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
            ValidationResult finalResult = new ValidationResult();

            if(_validationRules.Count > 0)
            {
                foreach(var rule in _validationRules)
                {
                    ValidationContext context = new ValidationContext(rule.ValidateObjectName, rule.GetValidateObjectValue(target));
                    ValidationResult result = rule.Validate(context);
                    finalResult.MergeValidationResult(result);
                }
            }

            return finalResult;
        }
    }
}