using Compiladores.Arbol.Identificador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AsignacionNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion2 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return expresion2;
            if (!(OperadorIzquierdo is IdentificadorNode))
                throw new SemanticoException("no se puede asignar literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1.GetType() == expresion2.GetType())
                return expresion2;

            throw new SemanticoException("no se puede asignar"+expresion1+" con "+ expresion2+ " fila "+_TOKEN.Fila+" columna "+ _TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()

        {
           var rightV = OperadorDerecho.Interpret();
            if (OperadorIzquierdo is IdentificadorNode)
            {
               var nombre = (OperadorIzquierdo as IdentificadorNode).value;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                    if (stack.VariableExist(nombre))
                    {
                        var valorID = stack.GetVariableValue(nombre);
                        if (valorID.GetType() == rightV.GetType())
                            stack.SetVariableValue(nombre, rightV);
                    }
            }


            return rightV;
        }
        
    }
}
