using Compiladores.Arbol.Identificador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AssignStamentExpresionNode:StatementNode
    {
        public IdentificadorStamNode OperadorIzquierdo;
        public ExpressionNode expresion;
        public override void ValidSemantic()
        {
            OperadorIzquierdo.ValidSemantic();
            var expresionValidado = expresion.ValidateSemantic();

            TiposBases Tipo = null; 
         foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(OperadorIzquierdo.value))
                    Tipo = stack.GetVariable(OperadorIzquierdo.value);
            };
            if (Tipo.GetType() != expresionValidado.GetType())
            {
                if (expresion is AutoOperacionSumaNode)
                 validarAutoOperacionSuma(Tipo, expresionValidado);
                if(expresion is AutoOperacionRestaNode)
                    validarAutoOperacionResta(Tipo, expresionValidado);
                if (expresion is AutoOperacionDivisionNode)
                    validarAutoOperacionDivision(Tipo, expresionValidado);
                if(expresion is AutoOperacionMultiplicacionNode)
                    validarAutoOperacionMultiplicacion(Tipo, expresionValidado);
                if (expresion is OperacionDivisionResiduoNode)
                    validarOperacionDivisionResiduoNode(Tipo, expresionValidado);
            }
            
        }
        private TiposBases validarOperacionDivisionResiduoNode(TiposBases expresion1, TiposBases expresion2)
        {
           
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new IntTipo();
            if (expresion1 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede obtener residuo de esta division " + expresion1 + " con " + expresion2);
        }
        private TiposBases validarAutoOperacionMultiplicacion(TiposBases expresion1, TiposBases expresion2)
        {
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new IntTipo();
            if (expresion1 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede multiplicar" + expresion1 + " con " + expresion2);
        }
        private TiposBases validarAutoOperacionDivision(TiposBases expresion1, TiposBases expresion2) {
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new IntTipo();
            if (expresion1 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede dividir" + expresion1 + " con " + expresion2);
        }
        private TiposBases validarAutoOperacionResta(TiposBases expresion1, TiposBases expresion2)
        {

            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new FloatTipo();
            if (expresion1 is IntTipo && expresion2 is IntTipo)
                return new IntTipo();

            throw new SemanticoException("no se puede restar" + expresion1 + " con " + expresion2);
        }
        private TiposBases validarAutoOperacionSuma(TiposBases expresion1, TiposBases expresion2)
        {
            if (expresion1 is StructTipo || expresion2 is StructTipo || expresion1 is VoidTipo || expresion2 is VoidTipo)
                throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2);

            if (expresion1 is StringTipo || expresion2 is StringTipo)
                if (!(expresion2 is EnumTipo) && !(expresion1 is EnumTipo))
                    if (!(expresion2 is DateTipo) && !(expresion1 is DateTipo))
                    return new StringTipo();

            if (expresion1 is CharTipo && expresion2 is CharTipo)
                return new StringTipo();
            if (expresion1.GetType() == expresion2.GetType())
                return expresion1;
            if ((expresion1 is IntTipo && expresion2 is FloatTipo) || (expresion2 is IntTipo && expresion1 is FloatTipo))
                return new FloatTipo();
            if (expresion1 is IntTipo && expresion2 is DateTipo)
                return new IntTipo();
            if (expresion2 is IntTipo && expresion1 is DateTipo)
                return new DateTipo();
            if ((expresion1 is CharTipo && expresion2 is IntTipo) || (expresion2 is CharTipo && expresion1 is IntTipo))
                return new IntTipo();
            if ((expresion1 is BooleanTipo && expresion2 is IntTipo) || (expresion2 is BooleanTipo && expresion1 is IntTipo))
                return new IntTipo();
            throw new SemanticoException("no se puede aito sumar " + expresion1 + " con " + expresion2);
           
        }
      

    }
}
