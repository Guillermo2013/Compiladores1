using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class OPorBitNode : BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            var expresion2 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is BooleanTipo && expresion2 is BooleanTipo)
                return expresion1;
            if ((expresion1 is BooleanTipo || expresion2 is IntTipo) && (expresion2 is BooleanTipo || expresion1 is IntTipo))
                return new IntTipo();
            throw new SemanticoException("no se puede auto operacion o logico no se puede  " + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            dynamic left = OperadorIzquierdo.Interpret();
            dynamic right = OperadorDerecho.Interpret();
            if (left is BoolValue && right is BoolValue)
                return new BoolValue { Value = (left.Value | right.Value) };
            if ((left is BooleanTipo || right is IntTipo) && (right is BooleanTipo || left is IntTipo))
                return new IntValue { Value = (left.Value | right.Value) };
            return null;

        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (OperadorIzquierdo != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            codigo += "|";
            if (OperadorDerecho != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            return codigo;
        }
    }
}
