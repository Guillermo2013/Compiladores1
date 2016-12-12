using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AutoOperacionCorrimientoDerechaNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var Izquierdo = OperadorIzquierdo.ValidateSemantic();
            var Derecho = OperadorDerecho.ValidateSemantic();
            if(!(Derecho is IntTipo))
                throw new SemanticoException(" El incremeto debe ser de tipo int fila "+ OperadorDerecho._TOKEN.Fila+" columna +"+ OperadorDerecho._TOKEN.Columna );
            if (Izquierdo is IntTipo || Izquierdo is StringTipo || Izquierdo is CharTipo)
            return Izquierdo;
             throw new SemanticoException("El tipo de dato no puede hacerse corrimiento Derecha  fila "+_TOKEN.Fila+" columna "+ _TOKEN.Columna );
        }
    }
}
