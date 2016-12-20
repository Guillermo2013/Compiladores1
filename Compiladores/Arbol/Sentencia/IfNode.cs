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
                  if(bloqueTrue !=null)
                    bloqueTrue.ValidSemantic();
                ContenidoStack.InstanceStack.Stack.Pop();
            }
            if (BloqueCondicionalFalse != null)
            {
                ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
                foreach (var bloqueFalse in BloqueCondicionalFalse)
                    if (bloqueFalse != null)
                    bloqueFalse.ValidSemantic();
                ContenidoStack.InstanceStack.Stack.Pop();
            }
        }
        public override void Interpret()
        {
            var condition = (BoolValue)condicional.Interpret();
            if (condition.Value)
            {
                foreach (var statementNode in BloqueCondicionalTrue)
                {
                    statementNode.Interpret();
                }
            }
            else
            {
                if (BloqueCondicionalFalse != null)
                {
                    foreach (var statementNode in BloqueCondicionalFalse)
                    {
                        statementNode.Interpret();
                    }
                }
            }
        }
        public override string GenerarCodigo()
        {
            string codigo = "if(";

            if (condicional != null)
                codigo += condicional.GenerarCodigo() + "){\n ";
            foreach (var lista in BloqueCondicionalTrue)
                codigo += lista.GenerarCodigo()+"\n";
            codigo += "}";
            if (BloqueCondicionalFalse != null)
            {
                codigo += "else {";
                foreach (var lista in BloqueCondicionalFalse)
                    codigo += lista.GenerarCodigo() + "\n";
                codigo += "}";
            }

            return codigo;
        }
    }
}
