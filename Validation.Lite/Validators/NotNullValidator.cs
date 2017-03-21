namespace Validation.Lite
{
    public class NotNullValidator : IValidator
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }

        public void Validate(ValidationContext context)
        {
            if(context.Value == null)
            {
                IsValid = false;
                Message = $"{context.Name} should not be null.";
                return;
            }

            IsValid = true;
            Message = "Ok";
        }
    }
}
