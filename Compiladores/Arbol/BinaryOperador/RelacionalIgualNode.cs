using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class RelacionalIgualNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var Derecho = OperadorDerecho.ValidateSemantic();
            var Izquierdo = OperadorIzquierdo.ValidateSemantic();
            if (Derecho is StructTipo || Izquierdo is StructTipo || Derecho is EnumTipo || Izquierdo is EnumTipo || Derecho is VoidTipo || Izquierdo is VoidTipo)
                throw new SemanticoException(" No se pude compara " + Derecho + " con " + Izquierdo + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            return new BooleanTipo();
        }
        public override Implementacion.Value Interpret()
        {
            dynamic left = OperadorIzquierdo.Interpret();
            dynamic right = OperadorDerecho.Interpret();
            return new BoolValue { Value = (left.Value == right.Value) };
;
        }
    }
}
