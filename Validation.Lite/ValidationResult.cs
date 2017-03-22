﻿using System;
using System.Collections.Generic;

namespace Validation.Lite
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorMessages { get; set; }

        public ValidationResult()
        {
            IsValid = true;
            ErrorMessages = new List<string>();
        }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            ErrorMessages = new List<string>() { message };
        }

        public void MergeValidationResult(ValidationResult result)
        {
            if (!result.IsValid)
            {
                IsValid = false;
                ErrorMessages.AddRange(result.ErrorMessages);
            }
        }

        public override string ToString()
        {
            if (IsValid)
            {
                return "Validate successfully.";
            }
            else
            {
                string errorMessage = string.Empty;
                for (int i = 0; i < ErrorMessages.Count; i++)
                {
                    if (i == ErrorMessages.Count - 1)
                    {
                        errorMessage += ErrorMessages[i];
                    }
                    else
                    {
                        errorMessage += ErrorMessages[i] + Environment.NewLine;
                    }
                }
                return errorMessage;
            }
        }
    }
}