using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class CharValue : Value
    {
        public char Value;
        public override Value Clone()
        {
            return new CharValue { Value = Value };
        }
    }
}
