using Compiladores.Semantico.Tipos;
using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Implementacion;

namespace Compiladores.Arbol.Sentencia
{
    public class DoWhileNode :StatementNode
    {
        public List<StatementNode> BloqueCondicionalDoWhile;
        public ExpressionNode condicional;
        public override void ValidSemantic()
        {
            ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
            foreach (StatementNode sentencia in BloqueCondicionalDoWhile)
                sentencia.ValidSemantic();
            ContenidoStack.InstanceStack.Stack.Pop();
            var condicionalEvaluar = condicional.ValidateSemantic();
            if (!(condicionalEvaluar is BooleanTipo))
                throw new SemanticoException("la condiciona debe de ser tipo booleano fila "+ condicional._TOKEN.Fila +" columna "+condicional._TOKEN.Columna );
        }
        public override void Interpret()
        {
            do{
                foreach(var sentencia in BloqueCondicionalDoWhile)
                   sentencia.Interpret(); 
            }while((condicional.Interpret() as BoolValue).Value);

        }
    }
}
