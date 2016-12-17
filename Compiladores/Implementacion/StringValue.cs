using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class StringValue : Value
    {
        public string Value { get; set; }
        public override Value Clone()
        {
            return new StringValue { Value = this.Value };
        }
    }
}
