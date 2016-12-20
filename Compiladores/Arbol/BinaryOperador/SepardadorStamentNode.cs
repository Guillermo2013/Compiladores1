using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class SepardadorStamentNode: StatementNode
    {
        public string valor { get; set; }
        public StatementNode OperadorDerecho;
        public StatementNode OperadorIzquierdo;
        public override void ValidSemantic()
        {
            OperadorDerecho.ValidSemantic();
            OperadorIzquierdo.ValidSemantic();
        }
        public override void Interpret()

        {
            OperadorDerecho.Interpret();
            OperadorIzquierdo.Interpret();
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (OperadorIzquierdo != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            codigo += ",";
            if (OperadorDerecho != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            return codigo;
        }
    }
}
