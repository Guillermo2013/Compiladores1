using Compiladores.Arbol.Identificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Accesores
{
    public class PuntoNode : AccesoresNode
    {
        public IdentificadorNode identificador;
        public override void ValidSemantic()
        {
          
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (identificador != null)
                codigo = "." + identificador.GenerarCodigo();
            return codigo;
        }
    }
}
