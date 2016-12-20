using Compiladores.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class StructValue: Value
    {
       public Dictionary<string, Value> Value = new Dictionary<string, Value>();
        public override Value Clone()
        {
            return new StructValue { Value = this.Value };
        }
    }
}
