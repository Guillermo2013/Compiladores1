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
    public class AutoOperacionXorNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return expresion1;
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            var expresion2 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is BooleanTipo && expresion2 is BooleanTipo)
                return expresion1;
            if (expresion2 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede auto operacion o logico no se puede  " + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
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
                    if (valorID is BoolValue && rightV is BoolValue)
                        stack.SetVariableValue(nombre, new BoolValue { Value = (valorID as BoolValue).Value ^ (rightV as BoolValue).Value });
                    if (valorID is IntValue && rightV is IntValue)
                        stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value ^ (rightV as IntValue).Value });


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
            codigo += "^=";
            if (OperadorDerecho != null)
                codigo += OperadorDerecho.GenerarCodigo();
            return codigo;
        }
    }
}
