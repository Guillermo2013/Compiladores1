﻿using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Compiladores.Arbol.UnaryOperador
{
    public class OExclusivoPorBitNode : BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            var expresion2 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is BooleanTipo && expresion2 is BooleanTipo)
                return expresion1;
            if ((expresion1 is BooleanTipo || expresion2 is IntTipo) && (expresion2 is BooleanTipo || expresion1 is IntTipo))
                return new IntTipo();
            throw new SemanticoException("no se puede utilizar el O por bit en expesiones "+expresion1 +" y "+ expresion2);
        }
    }
}
