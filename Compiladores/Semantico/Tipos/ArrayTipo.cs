using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class ArrayTipo:TiposBases
    {
        public TiposBases tipo = null;
        public bool unidimensional = false;
        public bool bidimensional = false ;

        public override string ToString()
        {
            return "Array";
        }
        public override Value GetDefaultValue()
        {
            return new ArrayValue() { Value = new Value[0] };
        }

    }
}
