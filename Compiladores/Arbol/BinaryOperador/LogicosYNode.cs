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
    public class LogicosYNode: BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            var expresion2 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is BooleanTipo && expresion2 is BooleanTipo)
                return new BooleanTipo();
            throw new Sintactico.SintanticoException(" las expresiones tiene que ser booleanas " + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            dynamic left = OperadorIzquierdo.Interpret();
            dynamic right = OperadorDerecho.Interpret();
            return new BoolValue { Value = (left.Value && right.Value) };

        }
    }
}
