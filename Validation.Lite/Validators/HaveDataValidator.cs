using System.Collections;

namespace Validation.Lite
{
    public class HaveDataValidator : IValidator
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public void Validate(ValidationContext context)
        {
            var collection = context.Value as ICollection;
            if (collection == null || collection.Count == 0)
            {
                IsValid = false;
                Message = $"{context.Name} should have data.";
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
