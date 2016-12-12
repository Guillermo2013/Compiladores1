using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class NegacionPorBitNode : UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var operador = Operando.ValidateSemantic();
            if(operador is IntTipo || operador is BooleanTipo|| operador is CharTipo)
            return new IntTipo();
            throw new Sintactico.SintanticoException(" no se puede negar el tipo " + operador+ " "+ Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
        }
    }

}
