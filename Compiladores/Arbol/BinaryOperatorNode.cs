using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol
{
    public abstract class BinaryOperatorNode:ExpressionNode
    {
        public string operador { get; set; }
        public ExpressionNode OperadorDerecho ;
        public ExpressionNode OperadorIzquierdo;
        
    }
}
