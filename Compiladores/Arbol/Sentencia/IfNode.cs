using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class IfNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<StatementNode> BloqueCondicionalTrue;
        public List<StatementNode> BloqueCondicionalFalse;
        public override void ValidSemantic()
        {
          
       
            var condicionalTest = condicional.ValidateSemantic();
            if (!(condicionalTest is BooleanTipo))
                throw new Sintactico.SintanticoException("la condicion debe de ser booleana");
            if (BloqueCondicionalTrue != null)
            {
                ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
                foreach (var bloqueTrue in BloqueCondicionalTrue)
                    bloqueTrue.ValidSemantic();
                ContenidoStack.InstanceStack.Stack.Pop();
            }
            if (BloqueCondicionalFalse != null)
            {
                ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
                foreach (var bloqueFalse in BloqueCondicionalFalse)
                    bloqueFalse.ValidSemantic();
                ContenidoStack.InstanceStack.Stack.Pop();
            }
        }
    }
}
