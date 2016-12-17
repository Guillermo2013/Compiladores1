using Compiladores.Arbol.Identificador;
using Compiladores.Implementacion;
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
              if (OperadorIzquierdo == null)
                  return Derecho;
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if(!(Derecho is IntTipo))
                throw new SemanticoException(" El incremeto debe ser de tipo int fila "+ OperadorDerecho._TOKEN.Fila+" columna +"+ OperadorDerecho._TOKEN.Columna );
            if (Izquierdo is IntTipo || Izquierdo is CharTipo)
            return new IntTipo();
             throw new SemanticoException("El tipo de dato no puede hacerse corrimiento Derecha  fila "+_TOKEN.Fila+" columna "+ _TOKEN.Columna );
        }
        public override Implementacion.Value Interpret()
        {
            Value valorID = null;
            string nombre = " ";
            if (OperadorIzquierdo is IdentificadorNode)
            {
                nombre = (OperadorIzquierdo as IdentificadorNode).value;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                    if (stack.VariableExist(nombre))
                    {
                        valorID = stack.GetVariableValue(nombre);
                    }
            }
            var rightV = OperadorDerecho.Interpret();

            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(nombre))
                {
                    if (valorID is IntValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value >> (rightV as IntValue).Value });

                }
            }
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
                if (stack.VariableExist(nombre))
                {
                    valorID = stack.GetVariableValue(nombre);
                }
            return valorID;
        }
    }
}
