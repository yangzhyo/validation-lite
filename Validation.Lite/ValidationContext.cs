namespace Validation.Lite
{
    public class ValidationContext
    {
        public string ValidateObjectName { get; set; }
        public object ValidateObjectValue { get; set; }

        public ValidationContext(string validateObjectName, object validateObjectValue)
        {
            ValidateObjectName = validateObjectName;
            ValidateObjectValue = validateObjectValue;
        }
    }
}
