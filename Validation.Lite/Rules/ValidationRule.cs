using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Lite
{
    public abstract class ValidationRule
    {
        public string RuleName { get; set; }
        public Type ValueType { get; set; }
        public IList<IValidator> Validators { get; set; }

        public ValidationRule(string ruleName, Type valueType)
        {
            RuleName = ruleName;
            ValueType = valueType;
            Validators = new List<IValidator>();
        }

        public void AddValidator(IValidator validator)
        {
            Validators.Add(validator);
        }

        public abstract object GetValidateValue(object obj);
    }
}
