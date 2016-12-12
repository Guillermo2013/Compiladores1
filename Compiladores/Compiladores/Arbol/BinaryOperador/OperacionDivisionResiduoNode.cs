using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class OperacionDivisionResiduoNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            var expresion2 = OperadorDerecho.ValidateSemantic();
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
            {
                if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                    return new FloatTipo();
                if (expresion1 is IntTipo || expresion2 is IntTipo)
                    return new IntTipo();
            }
            throw new SemanticoException("no se puede obtener residuo de esta division " + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
    }
}
