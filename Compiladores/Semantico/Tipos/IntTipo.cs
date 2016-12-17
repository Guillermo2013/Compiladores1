using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class IntTipo:TiposBases
    {
        public override string ToString()
        {
            return "Int";
        }
        public override Value GetDefaultValue()
        {
            return new IntValue { Value = 0 };
        }
    }
}
