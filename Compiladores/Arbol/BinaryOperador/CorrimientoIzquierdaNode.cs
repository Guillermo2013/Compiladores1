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
                throw new Sintactico.SintanticoException(" la expresion debe ser de tipo Int ");
            if (expresion2 is IntTipo || expresion2 is StringTipo)
                return expresion2;
            throw new Sintactico.SintanticoException(" no se puede hacer corrimiento de tipos " + expresion2);
        }
    }
}
