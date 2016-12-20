using Compiladores.Arbol.Identificador;
using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
   public class AutoDecrementoPosNode:StatementNode
    {
        public ExpressionNode operadador;
        public override void ValidSemantic()
        {
            var expresion = operadador.ValidateSemantic();
            if (!(operadador is IdentificadorNode))
                throw new SemanticoException("no se puede autoIncrementarLiterales literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if (!(expresion is FloatTipo) &&!( expresion is IntTipo) && !(expresion is CharTipo) )
                throw new Semantico.SemanticoException("el tipo del identificador tiene que ser Int fila " + _TOKEN.Fila + " colummna " + _TOKEN.Columna);
        }
        public override void Interpret()
        {

            Value valorID = operadador.Interpret();
            string nombre = " ";
            if (operadador is IdentificadorNode)
            {
                nombre = (operadador as IdentificadorNode).value;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                    if (stack.VariableExist(nombre))
                    {
                        if (valorID is IntValue)
                            stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value-- });
                        if (valorID is FloatValue)
                            stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value-- });
                        if (valorID is CharValue)
                            stack.SetVariableValue(nombre, new CharValue { Value = (valorID as CharValue).Value-- });
                    }
            }
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (operadador != null)
                codigo += operadador.GenerarCodigo()+"++";
           
            codigo += ";\n";
            return codigo;
        }
    }
}
