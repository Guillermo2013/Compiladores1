using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class GeneralDeclarationNode:StatementNode
    {
        public string tipo;
        public List<ExpressionNode> pointer;
        public string identificador;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
