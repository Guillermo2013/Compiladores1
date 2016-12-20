using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Semantico.Tipos;
using Compiladores.Semantico;
using Compiladores.Implementacion;

namespace Compiladores.Arbol.TiposDeDatos
{
    public class LiteralStringNode:ExpressionNode
    {
        public string valor;
        public override TiposBases ValidateSemantic()
        {
            return new StringTipo();
        }
        public override Implementacion.Value Interpret()
        {
            return new StringValue { Value = valor };
        }
        public override string GenerarCodigo()
        {
            string codigo = '\"' + valor + '\"';

            return codigo;
        }
    }
}
