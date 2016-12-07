using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class StructTipo:TiposBases
    {
        public Dictionary<string, TiposBases> elementos = new Dictionary<string, TiposBases>();
        public override string ToString()
        {
            return "struct";
        }
        
    }
}
