using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class SwitchNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<CaseNode> BloqueCondicionalCase;
        public override void ValidSemantic()
        {
            foreach (var cases in BloqueCondicionalCase)
                cases.ValidSemantic();
        }
    }
}
