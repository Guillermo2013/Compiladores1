using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class AutoOperacionDecrementoPre:UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (expresion is StructTipo || expresion is ConstTipo || expresion is BooleanTipo || expresion is EnumTipo)
                throw new Sintactico.SintanticoException("este tipo no puede decrementarse" + Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
            return expresion;
        }
    }
}
