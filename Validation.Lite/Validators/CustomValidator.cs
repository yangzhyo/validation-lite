﻿using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator
    {
        public Func<T, ValidationResult> CustomValidateFunc { get; set; }

        public bool IsValid { get; set; }

        public List<string> Messages { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            CustomValidateFunc = customValidateFunc;
        }

        public void Validate(ValidationContext context)
        {
            IsValid = true;
            Messages = new List<string>();

            ValidationResult result = CustomValidateFunc((T)context.Value);

            if (!result.IsValid)
            {
                IsValid = false;
                Messages.AddRange(result.ErrorMessages);
            }
        }
    }
}
