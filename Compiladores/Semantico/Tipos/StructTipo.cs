using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class StructTipo:TiposBases
    {
        public string identificadorStruct = "";
        public Dictionary<string, TiposBases> elementos = new Dictionary<string, TiposBases>();
        public override string ToString()
        {
            return "struct";
        }
        public override Value GetDefaultValue()
        {
            return new IntValue();
        }

    }
}
