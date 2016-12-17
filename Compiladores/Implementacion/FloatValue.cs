using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class FloatValue : Value
    {
        public float Value;
        public override Value Clone()
        {
            return new FloatValue { Value = Value };
        }
    }
}
