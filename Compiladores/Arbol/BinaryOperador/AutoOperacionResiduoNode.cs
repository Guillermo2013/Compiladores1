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
    public class AutoOperacionResiduoNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion2 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return expresion2;
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
  
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new IntTipo();
            if (expresion1 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede obtener residuo de esta division " + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
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
                    if (valorID is IntValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as IntValue).Value % (rightV as FloatValue).Value });
                    if (valorID is FloatValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value % (rightV as IntValue).Value });
                    if (valorID is FloatValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value % (rightV as FloatValue).Value });
                    if (valorID is IntValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value % (rightV as IntValue).Value });
                    if (valorID is IntValue && rightV is CharValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value % (rightV as CharValue).Value });

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
