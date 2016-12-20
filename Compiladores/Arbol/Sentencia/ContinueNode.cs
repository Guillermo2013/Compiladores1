using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ContinueNode:StatementNode
    {
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
        public override string GenerarCodigo()
        {
            string codigo = "continue;";
            ;
            codigo += "\n";
            return codigo;
        }
    }
}
