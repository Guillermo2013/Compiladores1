using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public abstract class UnaryOperadorNode : ExpressionNode
    {
       public string Value;
       public ExpressionNode Operando;
    }
}
