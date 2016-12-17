using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class IntValue : Value
    {
        public int Value { get; set; }
        public override Value Clone()
        {
            return new IntValue { Value = this.Value };
        }
    }
}
