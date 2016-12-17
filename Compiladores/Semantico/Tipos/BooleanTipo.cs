using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class BooleanTipo:TiposBases
    {
        public override string ToString()
        {
            return "Boolean";
        }
        public override Value GetDefaultValue()
        {
            return new BoolValue { Value = false };
        }
    }
}
