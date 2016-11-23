using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Identificador
{
   
    public class IdentificadorStamNode : StatementNode
    {
        public string value { get; set; }
        public List<ExpressionNode> pointer;
        public ExpressionNode inicializacion;
        public string tipo;
    }
}
