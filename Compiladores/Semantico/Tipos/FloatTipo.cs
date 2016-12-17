using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class FloatTipo:TiposBases
    {
        public override string ToString()
        {
            return "Float";
        }
        public override Value GetDefaultValue()
        {
            return new FloatValue { Value = 0 };
        }
    }
}
