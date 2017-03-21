using System.Collections.Generic;

namespace Validation.Lite
{
    public interface IValidator
    {
        bool IsValid { get; set; }
        List<string> Messages { get; set; }
        void Validate(ValidationContext context);
    }
}
