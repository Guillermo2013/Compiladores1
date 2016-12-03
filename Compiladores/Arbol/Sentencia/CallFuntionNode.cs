using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class CallFuntionNode:ExpressionNode
    {
        public string identificador;
        public ExpressionNode ListaDeParametros;
        public override TiposBases ValidateSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
