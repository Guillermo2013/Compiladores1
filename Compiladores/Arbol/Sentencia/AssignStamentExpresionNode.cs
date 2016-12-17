using Compiladores.Arbol.Identificador;
using Compiladores.Arbol.UnaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AssignStamentExpresionNode:StatementNode
    {
        public ExpressionNode OperadorIzquierdo;
        public ExpressionNode expresion;
        public override void ValidSemantic()
        {
            var Tipo = OperadorIzquierdo.ValidateSemantic();
            var expresionValidado = expresion.ValidateSemantic();
            if (Tipo.GetType() != expresionValidado.GetType())
            {
                if (expresion is AutoOperacionSumaNode)
                 validarAutoOperacionSuma(Tipo, expresionValidado);
                else if(expresion is AutoOperacionRestaNode)
                    validarAutoOperacionResta(Tipo, expresionValidado);
                else if (expresion is AutoOperacionDivisionNode)
                    validarAutoOperacionDivision(Tipo, expresionValidado);
                else if(expresion is AutoOperacionMultiplicacionNode)
                    validarAutoOperacionMultiplicacion(Tipo, expresionValidado);
                else if (expresion is OperacionDivisionResiduoNode)
                    validarOperacionDivisionResiduoNode(Tipo, expresionValidado);

                if (Tipo is MulpilicadorOperdadorReferenciaTipo)
                {
                    Type fieldsType1 = typeof(MulpilicadorOperdadorReferenciaTipo);
                    FieldInfo[] fields1 = fieldsType1.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var tipodeApuntador = (TiposBases)fields1[0].GetValue(Tipo);
                    if (!(expresionValidado is YreferenciaTipo))
                        throw new Semantico.SemanticoException("apuntador se tiene que asignar una direccion de referencia fila"+ _TOKEN.Fila +" columna"+ _TOKEN.Columna);
                    Type fieldsTypereferencia = typeof(YreferenciaTipo);
                    FieldInfo[] fields = fieldsTypereferencia.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var YporBit = (TiposBases)fields[0].GetValue(expresionValidado);
                    if(tipodeApuntador.GetType() != YporBit.GetType())
                        throw new Semantico.SemanticoException("no se puede asignar " + tipodeApuntador + " con " + YporBit + " fila " + _TOKEN.Fila + " columna" + _TOKEN.Columna);

                }
                else
                {
                    throw new Semantico.SemanticoException("no se puede asignar "+Tipo+" con "+expresionValidado +" fila "+ _TOKEN.Fila + " columna" + _TOKEN.Columna);
                }
                  
                
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
            throw new SemanticoException("no se puede obtener residuo de esta division " + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
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
            throw new SemanticoException("no se puede multiplicar" + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        private TiposBases validarAutoOperacionDivision(TiposBases expresion1, TiposBases expresion2) {
            if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
                if (expresion1.GetType() == expresion2.GetType())
                    return expresion1;
            if (expresion1 is FloatTipo || expresion2 is FloatTipo)
                return new IntTipo();
            if (expresion1 is IntTipo && expresion1 is IntTipo)
                return new IntTipo();
            throw new SemanticoException("no se puede dividir" + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
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

            throw new SemanticoException("no se puede restar" + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        private TiposBases validarAutoOperacionSuma(TiposBases expresion1, TiposBases expresion2)
        {
            if (expresion1 is StructTipo || expresion2 is StructTipo || expresion1 is VoidTipo || expresion2 is VoidTipo)
                throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);

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
            
            throw new SemanticoException("no se puede auto sumar " + expresion1 + " con " + expresion2 + " fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
           
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
