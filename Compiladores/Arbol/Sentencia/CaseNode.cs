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
            if (condicionalTipo is IntTipo || condicionalTipo is StringTipo || condicionalTipo is FloatTipo||condicionalTipo is CharTipo
                || condicionalTipo is DateTipo || condicionalTipo is ConstTipo)
            {
                foreach (StatementNode sentenciaAevaluar in BloqueCondicionalCase){
                    sentenciaAevaluar.ValidSemantic();
                }
                return condicionalTipo;
            }
            throw new SemanticoException("la expresion no debe ser de tipo "+ condicionalTipo+" " +condicional._TOKEN.Fila +" columna "+ condicional._TOKEN.Columna);
        }
        public override void ValidSemantic()
        {

        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
       
    }
}
