using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class SeparadorNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var izquierdo = OperadorDerecho.ValidateSemantic();
            var derecho = OperadorIzquierdo.ValidateSemantic();
            return null;
        }
    }
}
