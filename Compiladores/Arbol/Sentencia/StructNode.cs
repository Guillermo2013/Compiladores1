using Compiladores.Arbol.BinaryOperador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class StructNode: StatementNode
    {
        public string identificador;
        public List<StatementNode> bloqueStruct;
        public List<ExpressionNode> pointer;
        public string variableNombre;
        public AsignacionNode asignacion;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
