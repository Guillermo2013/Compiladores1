using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class CaseNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<StatementNode> BloqueCondicionalCase;
        public BreakNode breakNode;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
