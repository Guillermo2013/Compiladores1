using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class YporBitStamentNode:StatementNode
    {
        public string Value;
        public StatementNode OperandoStament;
        public override void ValidSemantic()
        {
            OperandoStament.ValidSemantic();
        }
        public override void Interpret()
        {
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (OperandoStament != null)
                codigo = "&" + OperandoStament.GenerarCodigo();
            return codigo;
        }
    }
}
