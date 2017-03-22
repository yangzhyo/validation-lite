using System;

namespace Validation.Lite
{
    public class PropertyValidationRule : ValidationRule
    {
        private Func<object, object> GetValueFunc { get; }

        public PropertyValidationRule(string validateObjectName, Type validateObjectType, Func<object, object> getValueFunc)
            : base(validateObjectName, validateObjectType)
        {
            GetValueFunc = getValueFunc;
        }

        public override object GetValidateObjectValue(object obj)
        {
            return GetValueFunc(obj);
        }
    }
}
