using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class ArrayValue:Value
    {
        public Value[] Value ;
        public override Value Clone()
        {
            return new ArrayValue { Value = this.Value };
        }
    }
}
