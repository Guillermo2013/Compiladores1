using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol.Accesores;
using Compiladores.Arbol.Sentencia;
using Compiladores.Semantico.Tipos;
using Compiladores.Semantico;
namespace Compiladores.Arbol.Identificador
{
   public class IdentificadorArrayNode:StatementNode
    {
        public GeneralDeclarationNode identificador;
        public AccesoresNode unidimesionarioArray;
        public AccesoresNode BidimesionarioArray;
        public List<ExpressionNode> inicializacion;
        public override void ValidSemantic()
        {
            identificador.ValidSemantic();
            if (unidimesionarioArray!= null)
                unidimesionarioArray.ValidSemantic();
            if (BidimesionarioArray != null)
                 BidimesionarioArray.ValidSemantic();
            if (inicializacion != null)
                inicializacion[0].ValidateSemantic();
        }
        

    }
}
