using System;
using System.Linq;

namespace Validation.Lite
{
    public class CustomValidator<T> : IValidator
    {
        public Func<T, ValidationResult> CustomValidateFunc { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public CustomValidator(Func<T, ValidationResult> customValidateFunc)
        {
            CustomValidateFunc = customValidateFunc;
        }

        public void Validate(ValidationContext context)
        {
            ValidationResult result = CustomValidateFunc((T)context.Value);
            if (!result.IsValid)
            {
                IsValid = false;
                Message = string.Empty;
                if (result.ErrorMessages.Count > 0)
                {
                    //result.ErrorMessages.ForEach(msg => Message += msg + Environment.NewLine);
                    //Message.TrimEnd(Environment.NewLine.ToCharArray());
                    for (int i = 0; i < result.ErrorMessages.Count; i++)
                    {
                        if (i == result.ErrorMessages.Count - 1)
                        {
                            Message += result.ErrorMessages[i];
                        }
                        else
                        {
                            Message += result.ErrorMessages[i] + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    Message = $"Custom validate {context.Name} failed";
                }
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
