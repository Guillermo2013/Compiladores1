using Compiladores.Arbol.Accesores;
using Compiladores.Arbol.BinaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Identificador
{
    public class IdentificadorNode : ExpressionNode
    {
        public string value { get; set; }
        public List<ExpressionNode> pointer;
        public List<AccesoresNode> Asesores;
        public override TiposBases ValidateSemantic()
        {
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(value))
                    return stack.GetVariable(value);
            };
            throw new SemanticoException("la variable " + value + " no existe");

            
        }
        
    }
    
}
