using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ForNode: StatementNode
    {
        public ExpressionNode ExpresionDeclaracion;
        public ExpressionNode ExpresionCondicional;
        public ExpressionNode ExpresionIncremento;
        public List<StatementNode> BloqueCondicionalFor;
        public override void ValidSemantic()
        {

            var declaracion = ExpresionDeclaracion;
            var condicional = ExpresionCondicional.ValidateSemantic();
            var incremento = ExpresionIncremento;
            if (incremento != null)
               if(!(incremento.ValidateSemantic() is IntTipo))
                   throw new Semantico.SemanticoException("la se debe asignar Int");

            if (declaracion != null)
            {
                declaracion.ValidateSemantic();
                if (!(condicional is BooleanTipo))
                    throw new Semantico.SemanticoException("la expresion debe se booleana");
            }
            ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
            foreach (var lista in BloqueCondicionalFor)
                lista.ValidSemantic();
            ContenidoStack.InstanceStack.Stack.Pop();

        }
    }
}
