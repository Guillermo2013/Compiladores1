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
            ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
            var condicionalEvaluado = condicional.ValidateSemantic();
           if(!(condicionalEvaluado is EnumTipo) && !(condicionalEvaluado is StructTipo) && !(condicionalEvaluado is VoidTipo))
            foreach (var cases in BloqueCondicionalCase){
               
               if(!(cases is DefaultCaseNode)){
                var caseEvaluado = cases.ValidSemanticReturn();
                if (condicionalEvaluado.GetType() != caseEvaluado.GetType())
                  throw new Sintactico.SintanticoException("el tipo de lo cases debe ser igual a la condicional fila " +cases._TOKEN.Fila +" columna "+cases._TOKEN.Columna);
              }
              else
                  cases.ValidSemantic();
              
            }
           else
            throw new Sintactico.SintanticoException("no se permite la este tipo en condicional "+ condicionalEvaluado +" fila "+ condicional._TOKEN.Fila+"columna"+condicional._TOKEN.Columna );
           ContenidoStack.InstanceStack.Stack.Pop();
        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
