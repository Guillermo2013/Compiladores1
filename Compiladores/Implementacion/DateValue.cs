using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class DateValue : Value
    {
        public DateTime Value;
        public override Value Clone()
        {
            return new DateValue { Value = Value };
        }
    }
}
