using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class IfNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<StatementNode> BloqueCondicionalTrue;
        public List<StatementNode> BloqueCondicionalFalse;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
