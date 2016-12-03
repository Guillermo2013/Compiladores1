using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ForEachNode:StatementNode
    {
        public string inicializacionForEach;
        public string ListaForEach;
        public List<StatementNode> BloqueCondicionalForEach;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
