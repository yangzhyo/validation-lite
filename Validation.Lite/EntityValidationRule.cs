using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Lite
{
    public class EntityValidationRule : ValidationRule
    {
        public EntityValidationRule(string ruleName, Type valueType)
            : base(ruleName, valueType)
        {
        }

        public override object GetValidateValue(object obj)
        {
            return obj;
        }
    
    }
}
