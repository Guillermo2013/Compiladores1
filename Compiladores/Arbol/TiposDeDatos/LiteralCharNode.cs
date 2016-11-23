using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.TiposDeDatos
{
   public  class LiteralCharNode: ExpressionNode
    {
       public char valor { get; set; }
    }
}
