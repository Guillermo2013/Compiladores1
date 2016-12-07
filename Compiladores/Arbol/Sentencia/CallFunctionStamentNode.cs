using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class CallFunctionStamentNode:StatementNode
    {
        public string identificador;
        public List<ExpressionNode> ListaDeParametros;

        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
