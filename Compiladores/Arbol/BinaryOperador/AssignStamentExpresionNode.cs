using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AssignStamentExpresionNode:StatementNode
    {
        public StatementNode OperadorIquierdo;
        public ExpressionNode expresion;
    }
}
