using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AsignacionNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion2 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return expresion2;
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1.GetType() == expresion2.GetType())
                return expresion2;

            throw new SemanticoException("no se puede asignar"+expresion1+" con "+ expresion2);
        }
    }
}
