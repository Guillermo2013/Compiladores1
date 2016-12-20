using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Compiladores.Arbol.Sentencia
{
    public class BreakNode:StatementNode
    {
        public override void ValidSemantic()
        {
            
        }
        public override void Interpret()
        {
           
        }
        public override string GenerarCodigo()
        {

           var codigo = "break;\n";

            return codigo;
        }
    }
}
