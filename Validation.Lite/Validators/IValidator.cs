namespace Validation.Lite
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T value);
    }
}
