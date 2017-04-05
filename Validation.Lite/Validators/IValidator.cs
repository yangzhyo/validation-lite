namespace Validation.Lite
{
    public interface IValidator<T>
    {
        string ValidationName { get; set; }
        ValidationResult Validate(T value);
    }
}
