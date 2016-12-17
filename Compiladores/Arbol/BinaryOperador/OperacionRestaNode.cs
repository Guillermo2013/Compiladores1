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
   public class OperacionRestaNode:BinaryOperatorNode
    {
       public override TiposBases ValidateSemantic()
       {
           var expresion1 = OperadorIzquierdo.ValidateSemantic();
           var expresion2 = OperadorDerecho.ValidateSemantic();


           if ((expresion1 is IntTipo || expresion1 is FloatTipo) && (expresion2 is FloatTipo || expresion2 is IntTipo))
           if (expresion1.GetType() == expresion2.GetType())
               return expresion1;
           if (expresion1 is FloatTipo || expresion2 is FloatTipo)
               return new FloatTipo();
           if (expresion1 is IntTipo && expresion2 is IntTipo)
               return new IntTipo();

           throw new SemanticoException("no se puede restar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);

       }
       public override Implementacion.Value Interpret()
       {
           var leftV = OperadorIzquierdo.Interpret();
           var rightV = OperadorDerecho.Interpret();

           if (leftV is FloatValue && rightV is FloatValue)
               return new FloatValue { Value = (leftV as FloatValue).Value - (rightV as FloatValue).Value };
           if (leftV is IntValue && rightV is FloatValue)
               return new FloatValue { Value = (leftV as IntValue).Value - (rightV as FloatValue).Value };
           if (leftV is FloatValue && rightV is IntValue)
               return new FloatValue { Value = (leftV as FloatValue).Value - (rightV as IntValue).Value };
           if (leftV is IntValue && rightV is IntValue)
               return new FloatValue { Value = (leftV as IntValue).Value - (rightV as IntValue).Value };
           return null;
       }
    }
}
