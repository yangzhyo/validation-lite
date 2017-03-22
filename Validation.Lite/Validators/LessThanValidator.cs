﻿using System;

namespace Validation.Lite
{
    public class LessThanValidator : IValidator
    {
        private IComparable _factor;

        public LessThanValidator(IComparable factor)
        {
            _factor = factor;
        }

        public ValidationResult Validate(ValidationContext context)
        {
            ValidationResult result = new ValidationResult();

            IComparable value = context.ValidateObjectValue as IComparable;

            if (value == null || value.CompareTo(_factor) >= 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{context.ValidateObjectName} should be less than {_factor}.");
            }

            return result;
        }
    }
}
