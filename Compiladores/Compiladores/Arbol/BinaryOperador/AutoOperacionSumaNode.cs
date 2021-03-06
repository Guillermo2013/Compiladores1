﻿using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AutoOperacionSumaNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            
            var expresion2 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return OperadorDerecho.ValidateSemantic();
            var expresion1 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is StructTipo || expresion2 is StructTipo || expresion1 is VoidTipo || expresion2 is VoidTipo)
                throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2);

            if (expresion1 is StringTipo || expresion2 is StringTipo)
                if (!(expresion2 is EnumTipo) && !(expresion1 is EnumTipo))
                    return new StringTipo();

            if (expresion1 is CharTipo && expresion2 is CharTipo)
                return new StringTipo();
            if (expresion1 == expresion2)
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


            throw new SemanticoException("no se puede sumar" + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
    }
}
