using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class DateTipo:TiposBases
    {
        public override string ToString()
        {
            return "Date";
        }
        public override Value GetDefaultValue()
        {
            return new DateValue { Value = new DateTime() };
        }
    }
}
