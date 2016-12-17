using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class StringTipo:TiposBases
    {
        public override string ToString()
        {
            return "String";
        }
        public override Value GetDefaultValue()
        {
            return new StringValue { Value = "" };
        }
    }
}
