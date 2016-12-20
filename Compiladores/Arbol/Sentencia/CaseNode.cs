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
            foreach (var stament in BloqueCondicionalCase)
                stament.Interpret();
        }
        public override string GenerarCodigo()
        {
            string codigo = "case ";
            if (condicional != null)
                codigo += condicional.GenerarCodigo() + ":";
            foreach (var lista in BloqueCondicionalCase)
                codigo += lista.GenerarCodigo();
            if(breakNode !=null)
                codigo += breakNode.GenerarCodigo();

            codigo += "\n";
            return codigo;
        }
    }
}
