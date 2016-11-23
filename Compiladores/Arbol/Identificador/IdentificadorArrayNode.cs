using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol.Accesores;
using Compiladores.Arbol.Sentencia;
namespace Compiladores.Arbol.Identificador
{
   public class IdentificadorArrayNode:StatementNode
    {
        public GeneralDeclarationNode identificador;
        public AccesoresNode unidimesionarioArray;
        public AccesoresNode BidimesionarioArray;
        public ExpressionNode inicializacion;

    }
}
