using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
namespace Compiladores.Arbol.UnaryOperador
{
    public class AutoOperacionDecrementoPos : UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (expresion is StructTipo || expresion is ConstTipo || expresion is BooleanTipo || expresion is EnumTipo)
                throw new Sintactico.SintanticoException("este tipo no puede decrementarse");
            return expresion;
        }
    }
}
