using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.TiposDeDatos
{
    public class LiteralBooleanaNode:ExpressionNode
    {
        public bool valor { get; set; }
        public override TiposBases ValidateSemantic()
        {
            return new BooleanTipo();
        }
    }
}
