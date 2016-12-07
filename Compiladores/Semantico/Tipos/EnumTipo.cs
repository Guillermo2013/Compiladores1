using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    
        public class EnumTipo : TiposBases
        {
            public Dictionary<string, int> elementos = new Dictionary<string, int>();
            public override string ToString()
            {
                return "Enum";
            }
           
        
    }
}
