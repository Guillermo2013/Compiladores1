using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class SwitchNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<CaseNode> BloqueCondicionalCase;
        public override void ValidSemantic()
        {
            var condicionalEvaluado = condicional.ValidateSemantic();
           if(!(condicionalEvaluado is EnumTipo) && !(condicionalEvaluado is StructTipo) && !(condicionalEvaluado is VoidTipo))
            foreach (var cases in BloqueCondicionalCase){
                ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
               if(!(cases is DefaultCaseNode)){
                var caseEvaluado = cases.ValidSemanticReturn();
                if (condicionalEvaluado.GetType() != caseEvaluado.GetType())
                  throw new Sintactico.SintanticoException("el tipo de lo cases debe ser igual a la condicional");
              }
              else
                  cases.ValidSemantic();
               ContenidoStack.InstanceStack.Stack.Pop();
            }
           else
            throw new Sintactico.SintanticoException("no se permite la este tipo en condicional "+ condicionalEvaluado);
        }
    }
}
