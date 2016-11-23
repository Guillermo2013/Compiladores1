using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class DoWhileNode :StatementNode
    {
        public List<StatementNode> BloqueCondicionalDoWhile;
        public ExpressionNode condicional;
    }
}
