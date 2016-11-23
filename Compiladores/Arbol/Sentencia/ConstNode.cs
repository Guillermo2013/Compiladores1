using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ConstNode: StatementNode
    {
        public string identificador { get; set; }
        public List<ExpressionNode> pointer;
        public ExpressionNode expresion;
        public string Tipo;
    }
}
