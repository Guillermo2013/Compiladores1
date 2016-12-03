using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ForNode: StatementNode
    {
        public ExpressionNode ExpresionDeclaracion;
        public ExpressionNode ExpresionCondicional;
        public ExpressionNode ExpresionIncremento;
        public List<StatementNode> BloqueCondicionalFor;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
