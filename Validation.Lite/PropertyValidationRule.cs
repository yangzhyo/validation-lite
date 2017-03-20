using System;

namespace Validation.Lite
{
    public class PropertyValidationRule : ValidationRule
    {
        private Func<object, object> GetValueFunc { get; set; }

        public PropertyValidationRule(string ruleName, Type valueType, Func<object, object> getValueFunc)
            : base(ruleName, valueType)
        {
            GetValueFunc = getValueFunc;
        }

        public override object GetValidateValue(object obj)
        {
            return GetValueFunc(obj);
        }
    }
}
