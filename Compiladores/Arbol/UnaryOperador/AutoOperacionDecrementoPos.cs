using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Arbol.Identificador;
namespace Compiladores.Arbol.UnaryOperador
{
    public class AutoOperacionDecrementoPos : UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (!(Operando is IdentificadorNode))
                throw new SemanticoException("no se puede autoIncrementarLiterales literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if (expresion is FloatTipo || expresion is IntTipo || expresion is CharTipo || expresion is EnumTipo)
               return expresion;
            throw new Sintactico.SintanticoException("este tipo no puede decrementarse fila" + Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
            
        }
        public override Implementacion.Value Interpret()
        {
            var value = Operando.Interpret();

        }
    }
}
