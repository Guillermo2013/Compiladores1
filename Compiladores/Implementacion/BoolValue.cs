using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class BoolValue : Value
    {
        public bool Value { get; set; }
        public override Value Clone()
        {
            return new BoolValue { Value = this.Value };
        }
    }
}
