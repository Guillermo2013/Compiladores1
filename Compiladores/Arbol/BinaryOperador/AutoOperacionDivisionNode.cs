using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AutoOperacionDivisionNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            
            var Derecho = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return Derecho;
            var Izquierdo = OperadorIzquierdo.ValidateSemantic();
            if(Izquierdo is BooleanTipo || Izquierdo is EnumTipo || Izquierdo is StructTipo||
                Derecho is BooleanTipo || Derecho is EnumTipo || Derecho is StructTipo)
                    throw new SemanticoException("El tipo no puede ser divido");
       
            throw new SemanticoException("No se puede dividir"+Izquierdo+" con " + Derecho);
        }
    }
}
