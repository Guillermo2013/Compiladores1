using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol
{
     public class AccesoresNode:StatementNode
    {
        public string valor { get; set; }
        public StatementNode OperadorDerecho;
        public StatementNode OperadorIzquierdo;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
        public override string GenerarCodigo()
        {
            return "";
        }
    }
}
