using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class RelacionalMenorNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var Derecho = OperadorDerecho.ValidateSemantic();
            var Izquierdo = OperadorIzquierdo.ValidateSemantic();
            if (Derecho is StructTipo || Izquierdo is StructTipo)
                throw new SemanticoException(" No se pude compara " + Derecho + " con " + Izquierdo);
            return new BooleanTipo();
        }
    }
}
