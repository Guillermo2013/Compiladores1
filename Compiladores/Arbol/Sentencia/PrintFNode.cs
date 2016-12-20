using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class PrintFNode:StatementNode
    {
        public ExpressionNode Value { get; set; }

        public override void ValidSemantic()
        {
            Value.ValidateSemantic();
        }

        public override void Interpret()
        {
            dynamic value = Value.Interpret();
            Console.WriteLine(value.Value);
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            
            return codigo;
        }
    }
}
