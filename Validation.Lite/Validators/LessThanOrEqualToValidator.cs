using System;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public class LessThanOrEqualToValidator<T> : IValidator<T>
    {
        private T _factor;
        private static Func<T, T, bool> CompareFunc
        {
            get
            {
                ParameterExpression value = Expression.Parameter(typeof(T), "value");
                ParameterExpression factor = Expression.Parameter(typeof(T), "factor");
                BinaryExpression compare = Expression.LessThanOrEqual(value, factor);
                Expression<Func<T, T, bool>> lambda =
                    Expression.Lambda<Func<T, T, bool>>(
                        compare,
                        new ParameterExpression[] { value, factor });

                return lambda.Compile();
            }
        }

        public string ValidationName { get; set; }

        public LessThanOrEqualToValidator(T factor)
        {
            _factor = factor;
        }

        public ValidationResult Validate(T value)
        {
            ValidationResult result = new ValidationResult();

            if (!CompareFunc(value, _factor))
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{ValidationName} should be less than or equal to {_factor}.");
            }

            return result;
        }
    }
}
