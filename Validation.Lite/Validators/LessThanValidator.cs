﻿using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class LessThanValidator : IValidator
    {
        private IComparable _factor;

        public bool IsValid { get; set; }

        public List<string> Messages { get; set; }

        public LessThanValidator(IComparable factor)
        {
            _factor = factor;
        }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            IComparable value = context.Value as IComparable;

            if (value == null || value.CompareTo(_factor) >= 0)
            {
                IsValid = false;
                Messages.Add($"{context.Name} should be less than {_factor}.");
            }
        }
    }
}