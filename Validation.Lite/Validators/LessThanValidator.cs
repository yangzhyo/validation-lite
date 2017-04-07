using System;
using System.Linq.Expressions;

namespace Validation.Lite
{
    public class LessThanValidator<T> : IValidator<T>
    {
        private readonly T _factor;
        private static Func<T, T, bool> _compareFunc;
        private static Func<T, T, bool> CompareFunc
        {
            get
            {
                if (_compareFunc == null)
                {
                    ParameterExpression value = Expression.Parameter(typeof(T), "value");
                    ParameterExpression factor = Expression.Parameter(typeof(T), "factor");
                    BinaryExpression compare = Expression.LessThan(value, factor);
                    Expression<Func<T, T, bool>> lambda = Expression.Lambda<Func<T, T, bool>>(compare, value, factor);
                    _compareFunc = lambda.Compile();
                }

                return _compareFunc;
            }
        }

        public string ValidationName { get; set; }

        public LessThanValidator(T factor)
        {
            _factor = factor;
        }

        public ValidationResult Validate(T value)
        {
            if (!CompareFunc(value, _factor))
            {
                return new ValidationResult(false, $"{ValidationName} should be less than {_factor}.");
            }

            return ValidationResult.Valid;
        }
    }
}
