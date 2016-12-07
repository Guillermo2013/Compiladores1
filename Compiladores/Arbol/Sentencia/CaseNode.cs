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
        public override void ValidSemantic()
        {
            var condicionalTipo = condicional.ValidateSemantic();
            if (condicionalTipo is IntTipo || condicionalTipo is StringTipo || condicionalTipo is FloatTipo
                || condicionalTipo is DateTipo || condicionalTipo is ConstTipo)
            {
                foreach (StatementNode sentenciaAevaluar in BloqueCondicionalCase){
                    sentenciaAevaluar.ValidSemantic();
                }

            }
            throw new SemanticoException("la expresion debe ser booleana");
        }
    }
}
