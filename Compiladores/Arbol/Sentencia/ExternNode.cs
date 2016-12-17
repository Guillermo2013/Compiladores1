using Compiladores.Arbol.TiposDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ExternNode:StatementNode
    {
        public string literalString;
        public List<StatementNode> listadesentencias;
        public StatementNode declaracion;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
    
}
