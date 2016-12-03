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
            throw new NotImplementedException();
        }
    }
}
