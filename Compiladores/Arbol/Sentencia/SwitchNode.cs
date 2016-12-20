using Compiladores.Implementacion;
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
            dynamic condicionalValue = condicional.Interpret();
         
            var defaultStatements = new List<StatementNode>();
            foreach (var casos in BloqueCondicionalCase)
            {
                
                if (casos is DefaultCaseNode)
                {
                    defaultStatements = (casos as DefaultCaseNode).BloqueCondicionalCase;
                }
                else
                {
                    dynamic casoValue = casos.condicional.Interpret();
                    if (casoValue.Value == condicionalValue.Value)
                    {
                        foreach (var stamet in casos.BloqueCondicionalCase)
                        {
                            stamet.Interpret();
                            if (stamet is BreakNode)
                                return;
                        }
                        
                    }
                }
                
            
            }
                if(defaultStatements != null)
                foreach (var stamet in defaultStatements)
                    stamet.Interpret();


        }
        public override string GenerarCodigo()
        {
            string codigo = "switch ( ";

            if (condicional != null)
                codigo += condicional.GenerarCodigo() + ")\n{ \n";
            foreach (var lista in BloqueCondicionalCase)
                codigo += lista.GenerarCodigo() + "\n";
            codigo += "}";
            return codigo;
        }
    }
}
