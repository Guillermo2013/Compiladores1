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
    public class AutoOperacionDivisionNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            
            var Derecho = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return Derecho;
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            var Izquierdo = OperadorIzquierdo.ValidateSemantic();

            if (!(OperadorIzquierdo is Identificador.IdentificadorNode))
                 throw new SemanticoException("No se debe de asignar a un identificador fila" + _TOKEN.Fila+" columna " +_TOKEN.Columna);

            if(Izquierdo is BooleanTipo || Izquierdo is EnumTipo || Izquierdo is StructTipo||
                Derecho is BooleanTipo || Derecho is EnumTipo || Derecho is StructTipo)
                throw new SemanticoException("El tipo no puede ser divido fila" + OperadorDerecho._TOKEN.Fila );
       
            throw new SemanticoException("No se puede dividir"+Izquierdo+" con " + Derecho+" fila" + _TOKEN.Fila+" columna " +_TOKEN.Columna);
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
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as IntValue).Value / (rightV as FloatValue).Value });
                    if (valorID is FloatValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value / (rightV as IntValue).Value });
                    if (valorID is FloatValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value / (rightV as FloatValue).Value });
                    if (valorID is IntValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value / (rightV as IntValue).Value });
                    if (valorID is IntValue && rightV is CharValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value / (rightV as CharValue).Value });

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
