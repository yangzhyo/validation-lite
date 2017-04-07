namespace Validation.Lite
{
    public interface IValidator<in T>
    {
        string ValidationName { get; set; }
        ValidationResult Validate(T value);
    }
}
