using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class YporBitNode : UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
             Operando.ValidateSemantic();
             return new IntTipo();
        }
    }
}
