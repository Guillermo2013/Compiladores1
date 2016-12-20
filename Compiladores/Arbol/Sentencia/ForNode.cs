using Compiladores.Arbol.BinaryOperador;
using Compiladores.Arbol.Identificador;
using Compiladores.Arbol.UnaryOperador;
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
    public class ForNode: StatementNode
    {
        public ExpressionNode ExpresionDeclaracion;
        public ExpressionNode ExpresionCondicional;
        public ExpressionNode ExpresionIncremento;
        public List<StatementNode> BloqueCondicionalFor;
        public override void ValidSemantic()
        {

            var declaracion = ExpresionDeclaracion;
            var condicional = ExpresionCondicional.ValidateSemantic();
            var incremento = ExpresionIncremento;
            if (incremento != null)
               if(!(incremento.ValidateSemantic() is IntTipo))
                   throw new Semantico.SemanticoException("la se debe asignar Int");

            if (declaracion != null)
            {
                declaracion.ValidateSemantic();
                if (!(condicional is BooleanTipo))
                    throw new Semantico.SemanticoException("la expresion debe se booleana");
            }
            ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
            foreach (var lista in BloqueCondicionalFor)
                lista.ValidSemantic();
            ContenidoStack.InstanceStack.Stack.Pop();

        }
        public override void Interpret()
        {
            if(ExpresionDeclaracion != null)
            ExpresionDeclaracion.Interpret();
            while ((ExpresionCondicional.Interpret() as BoolValue).Value)
            {
               var incremento =  ExpresionIncremento.Interpret();
                foreach (var statementNode in BloqueCondicionalFor)
                {
                    statementNode.Interpret();
                    if (statementNode is BreakNode)
                        break;
                    if (statementNode is ReturnNode)
                        return;
                }
                if(ExpresionIncremento is AutoOperacionDecrementoPre||ExpresionIncremento is AutoOperacionDecrementoPos||
                    ExpresionIncremento is AutoOperacionIncrementoPre||ExpresionIncremento is AutoOperacionIncrementoPos)
                foreach(var stack in ContenidoStack.InstanceStack.Stack)
                    if(stack.VariableExist(((ExpresionDeclaracion as AsignacionNode).OperadorIzquierdo as IdentificadorNode).value))
                        stack.SetVariableValue(((ExpresionDeclaracion as AsignacionNode).OperadorIzquierdo as IdentificadorNode).value, incremento);
               
            }
         }
        public override string GenerarCodigo()
        {
            string codigo = "for ( ";
            if (ExpresionDeclaracion != null)
                codigo += ExpresionDeclaracion.GenerarCodigo() + ";";
            if (ExpresionCondicional != null)
                codigo += ExpresionCondicional.GenerarCodigo() + ";";
            if (ExpresionIncremento != null)
                codigo += ExpresionIncremento.GenerarCodigo() + ";){\n";

            foreach (var lista in BloqueCondicionalFor)
                codigo += lista.GenerarCodigo() + "\n";
            codigo += "};\n";

            return codigo;
        }
    }
}
