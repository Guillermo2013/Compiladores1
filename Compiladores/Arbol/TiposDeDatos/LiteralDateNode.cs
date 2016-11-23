using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.TiposDeDatos
{
    public class LiteralDateNode:ExpressionNode
    {
        public DateTime valor { get; set; }
    }
}
