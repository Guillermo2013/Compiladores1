using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class OperdadorMultiplicacionUnaryNode:UnaryOperadorNode
    {
        public string value { get; set; }
        public override TiposBases ValidateSemantic()
        {
            var operador = Operando.ValidateSemantic();
            return operador;
        }
        public override Implementacion.Value Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
