using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class OperacionDivisionNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var tipoIzquierdo = OperadorIzquierdo.ValidateSemantic();
            var TipoDerecho = OperadorDerecho.ValidateSemantic();
            if (TipoDerecho.GetType() == tipoIzquierdo.GetType())
            {
                return TipoDerecho;
            }
              throw new SemanticoException("No se puede dividir entre tipo: "+tipoIzquierdo+"y"+TipoDerecho);
        }
    }
}
