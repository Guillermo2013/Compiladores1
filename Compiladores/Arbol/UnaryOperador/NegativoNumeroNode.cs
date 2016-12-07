using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class NegativoNumeroNode:UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (expresion is IntTipo || expresion is FloatTipo)
                return expresion;
            throw new SemanticoException("se esperaba un literal numerica ");
        }
    }
}
