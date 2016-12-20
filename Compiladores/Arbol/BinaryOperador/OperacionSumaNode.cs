using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class OperacionSumaNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            var expresion2 = OperadorDerecho.ValidateSemantic();

            if (expresion1 is StructTipo || expresion2 is StructTipo || expresion1 is VoidTipo || expresion2 is VoidTipo
                || expresion1 is DateTipo || expresion2 is DateTipo || expresion1 is EnumTipo || expresion2 is EnumTipo)
                throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if (expresion1 is StringTipo && expresion2 is StringTipo)
                return new StringTipo();
            if (expresion1 is StringTipo && expresion2 is CharTipo)
                return new StringTipo();    
            if (expresion1 is CharTipo && expresion2 is CharTipo)
                return new IntTipo();
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
            if((expresion1 is BooleanTipo && expresion2 is IntTipo)||(expresion2 is BooleanTipo && expresion1 is IntTipo))
                return new IntTipo();

            if (expresion1 is StringTipo || expresion2 is StringTipo)
                return new StringTipo();
            throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            var leftV = OperadorIzquierdo.Interpret();
            var rightV = OperadorDerecho.Interpret();
            if(leftV is CharValue && rightV is CharValue)
                return new IntValue { Value = (leftV as CharValue).Value + (rightV as CharValue).Value };
            if (leftV is StringValue && rightV is StringValue)
                return new StringValue { Value = (leftV as StringValue).Value + (rightV as StringValue).Value };
            if (leftV is FloatValue && rightV is IntValue)
                return new FloatValue { Value = (leftV as FloatValue).Value + (rightV as IntValue).Value };
            if (leftV is IntValue && rightV is FloatValue)
                return new FloatValue { Value = (leftV as IntValue).Value + (rightV as FloatValue).Value };
            if (leftV is FloatValue && rightV is FloatValue)
                return new FloatValue { Value = (leftV as FloatValue).Value + (rightV as FloatValue).Value };
            if (leftV is StringValue && rightV is FloatValue)
                return new StringValue { Value = (leftV as StringValue).Value + (rightV as FloatValue).Value };
            if (rightV is StringValue && leftV is FloatValue)
                return new StringValue { Value = (rightV as StringValue).Value + (leftV as FloatValue).Value };
            if (rightV is StringValue && leftV is IntValue)
                return new StringValue { Value = (rightV as StringValue).Value + (leftV as IntValue).Value };
            if (rightV is IntValue && leftV is StringValue)
                return new StringValue { Value = (rightV as IntValue).Value + (leftV as StringValue).Value };
            if (leftV is IntValue && rightV is IntValue)
                return new IntValue { Value = (leftV as IntValue).Value + (rightV as IntValue).Value };
            return null;

        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (OperadorIzquierdo != null)
                codigo += OperadorIzquierdo.GenerarCodigo();
            codigo += "+";
            if (OperadorDerecho != null)
                codigo += OperadorDerecho.GenerarCodigo();
            return codigo;
        }
    }
}
