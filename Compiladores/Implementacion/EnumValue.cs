using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Implementacion
{
    public class EnumValue:Value
    {
        public List<StringValue> Value;
        public override Value Clone()
        {
            return new EnumValue { Value = this.Value };
        }
    }
}
