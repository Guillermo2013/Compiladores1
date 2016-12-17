using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.TiposDeDatos
{
   public  class LiteralCharNode: ExpressionNode
    {
       public char valor;
       public override TiposBases ValidateSemantic()
       {
           return new CharTipo();
       }
       public override Implementacion.Value Interpret()
       {
           return new CharValue { Value = valor };
       }
    }
}
