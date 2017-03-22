namespace Validation.Lite
{
    public interface IValidator
    {
        ValidationResult Validate(ValidationContext context);
    }
}
