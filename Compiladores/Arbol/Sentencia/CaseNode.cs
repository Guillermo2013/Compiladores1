using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class CaseNode:StatementNode
    {
        public ExpressionNode condicional;
        public List<StatementNode> BloqueCondicionalCase;
        public BreakNode breakNode;
        public  TiposBases ValidSemanticReturn()
        {
            var condicionalTipo = condicional.ValidateSemantic();
            if (condicionalTipo is IntTipo || condicionalTipo is StringTipo || condicionalTipo is FloatTipo
                || condicionalTipo is DateTipo || condicionalTipo is ConstTipo)
            {
                foreach (StatementNode sentenciaAevaluar in BloqueCondicionalCase){
                    sentenciaAevaluar.ValidSemantic();
                }
                return condicionalTipo;
            }
            throw new SemanticoException("la expresion debe ser booleana");
        }
        public override void ValidSemantic()
        {

        }
       
    }
}
