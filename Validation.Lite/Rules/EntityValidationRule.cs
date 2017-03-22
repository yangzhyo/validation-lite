using System;

namespace Validation.Lite
{
    public class EntityValidationRule : ValidationRule
    {
        public EntityValidationRule(string validateObjectName, Type validateObjectType)
            : base(validateObjectName, validateObjectType)
        {
        }

        public override object GetValidateObjectValue(object obj)
        {
            return obj;
        }
    
    }
}
