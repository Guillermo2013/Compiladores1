using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ReturnNode:StatementNode
    {
        public ExpressionNode returnExpression;
        public override void ValidSemantic(){
      
            var expresion = returnExpression.ValidateSemantic(); 
            
            }
        public override void Interpret()
        {
           
        }
        public Value InterpetReturm()
        {
            return returnExpression.Interpret();
        }
        public override string GenerarCodigo()
        {
            string codigo = "return ";

            if (returnExpression != null)
                codigo += returnExpression.GenerarCodigo() + ";\n";


            return codigo;
        }
    }
}
