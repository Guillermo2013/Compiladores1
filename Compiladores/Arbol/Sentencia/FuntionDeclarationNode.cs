using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class FuntionDeclarationNode: StatementNode
    {
        public StatementNode paramentros;
        public List<StatementNode> declaracionDeFuncion;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
