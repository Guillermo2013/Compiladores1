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
    public class WhileNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<StatementNode> BloqueCondicionalWhile;
        public override void ValidSemantic()
        {
            var condicionalTipe = condicional.ValidateSemantic();
            if (condicionalTipe is BooleanTipo)
            {
                ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
                foreach (var sentencias in BloqueCondicionalWhile)
                    sentencias.ValidSemantic();
                ContenidoStack.InstanceStack.Stack.Pop();
            }
            else
                throw new SemanticoException("la condiciona debe de ser tipo booleano fila " +condicional._TOKEN.Fila + " columna " +condicional._TOKEN.Columna);
        }
        public override void Interpret()
        {
            
            while (((BoolValue)condicional.Interpret()).Value)
            {
                foreach (var statementNode in BloqueCondicionalWhile)
                {
                    statementNode.Interpret();
                    if (statementNode is BreakNode)
                        break;
                    if (statementNode is ReturnNode)
                        return;
                }
            }
        }
        public override string GenerarCodigo()
        {
            string codigo = "while ( ";

            if (condicional != null)
                codigo += condicional.GenerarCodigo() + ")\n{ \n";
            foreach (var lista in BloqueCondicionalWhile)
                codigo += lista.GenerarCodigo() + "\n";
            codigo += "}";
            return codigo;
        }
    }
}
