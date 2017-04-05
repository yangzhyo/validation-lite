namespace Validation.Lite
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T value);
    }
}
