using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Lite
{
    public class ValidationContext
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ValidationContext()
        {

        }

        public ValidationContext(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
