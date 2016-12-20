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
    public class NegacionNode:UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if(expresion is BooleanTipo)
                return new BooleanTipo();
            
            throw new SemanticoException("La expresion debe ser booleana" + Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            var valor = Operando.Interpret();
            if (valor is BoolValue)
                return new BoolValue() { Value = !(valor as BoolValue).Value };
            return null;
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (Operando != null)
                codigo =  "!"+Operando.GenerarCodigo() ;
            return codigo;
        }
    }
}
