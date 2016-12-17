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
    public class CorrimientoIzquierdaNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            var expresion2 = OperadorIzquierdo.ValidateSemantic();

            if (!(expresion1 is IntTipo))
                throw new Sintactico.SintanticoException(" la expresion debe ser de tipo Int " + "fila " + OperadorDerecho._TOKEN.Fila + " columna " + OperadorDerecho._TOKEN.Columna);
            if (expresion2 is IntTipo || expresion2 is CharTipo)
                return new IntTipo();
            throw new Sintactico.SintanticoException(" no se puede hacer corrimiento de tipos " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            dynamic left = OperadorIzquierdo.Interpret();
            dynamic right = OperadorDerecho.Interpret();

            if (!(right is IntValue))
            {
                if (left is IntValue)
                    return new IntValue { Value = (right as IntValue).Value >> (left as IntValue).Value };
                if (left is CharValue)
                    return new IntValue { Value = (right as CharValue).Value >> (left as IntValue).Value };
            }
            return null;

        }
    }
}
