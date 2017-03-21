namespace Validation.Lite
{
    public interface IValidator
    {
        bool IsValid { get; set; }
        string Message { get; set; }
        void Validate(ValidationContext context);
    }
}
