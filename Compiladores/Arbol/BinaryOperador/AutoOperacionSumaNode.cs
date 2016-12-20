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
    public class AutoOperacionSumaNode : BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {

            var expresion2 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return OperadorDerecho.ValidateSemantic();
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is StructTipo || expresion2 is StructTipo || expresion1 is VoidTipo || expresion2 is VoidTipo
                 || expresion1 is EnumTipo || expresion2 is EnumTipo || expresion1 is DateTipo || expresion2 is DateTipo)
                throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);

            if (expresion1 is StringTipo || expresion2 is StringTipo)
                     return new StringTipo();
            if (expresion1 == expresion2)
                return expresion1;
            if ((expresion1 is IntTipo && expresion2 is FloatTipo) || (expresion2 is IntTipo && expresion1 is FloatTipo))
                return new FloatTipo();
            if ((expresion1 is CharTipo && expresion2 is IntTipo) || (expresion2 is CharTipo && expresion1 is IntTipo))
                return new IntTipo();
           

            throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
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
                    if (valorID is StringValue && rightV is StringValue)
                        stack.SetVariableValue(nombre, new StringValue { Value = (valorID as StringValue).Value + (rightV as StringValue).Value });
                    if(valorID is StringValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new StringValue { Value = (valorID as StringValue ).Value + (rightV  as IntValue).Value });
                    if (valorID is StringValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new StringValue { Value = (valorID as StringValue).Value + (rightV as FloatValue).Value });
                    if (valorID is IntValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as IntValue).Value + (rightV as FloatValue).Value });
                    if (valorID is FloatValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value + (rightV as IntValue ).Value });
                    if (valorID is FloatValue && rightV is FloatValue)
                        stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value + (rightV as FloatValue).Value });
                    if (valorID is IntValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value + (rightV as IntValue).Value });
                    if (valorID is StringValue && rightV is CharValue)
                        stack.SetVariableValue(nombre, new StringValue { Value = (valorID as StringValue).Value + (rightV as CharValue).Value });
                    if (valorID is CharValue && rightV is StringValue)
                        stack.SetVariableValue(nombre, new StringValue { Value = (valorID as CharValue).Value + (rightV as StringValue).Value });
                    if (valorID is IntValue && rightV is CharValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value + (rightV as CharValue).Value });
                   
                }
            }
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
                if (stack.VariableExist(nombre))
                {
                    valorID = stack.GetVariableValue(nombre);
                }
            return valorID;
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (OperadorIzquierdo != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            codigo += "+=";
            if (OperadorDerecho != null)
                codigo += OperadorDerecho.GenerarCodigo();
            return codigo;
        }
    }


}