using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.TiposDeDatos
{
    public class LiteralIntNode:ExpressionNode
    {
        public int valor;
        public override TiposBases ValidateSemantic()
        {
            return new IntTipo();
        }
        public override Implementacion.Value Interpret()
        {
            return new IntValue { Value = valor };
        }
        public override string GenerarCodigo()
        {
            string codigo = valor.ToString();

            return codigo;
        }
    }
}
