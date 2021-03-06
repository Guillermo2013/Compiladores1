﻿using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.BinaryOperador
{
    public class AutoOperacionYlogicoNode:BinaryOperatorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion1 = OperadorDerecho.ValidateSemantic();
            if (OperadorIzquierdo == null)
                return expresion1;
            var expresion2 = OperadorIzquierdo.ValidateSemantic();
            if (expresion1 is BooleanTipo && expresion2 is BooleanTipo)
                return expresion1;
            if ((expresion1 is BooleanTipo || expresion2 is IntTipo) && (expresion2 is BooleanTipo || expresion1 is IntTipo))
                return new IntTipo();
            throw new SemanticoException("no se puede auto operacion o logico no se puede  " + expresion1 + " con " + expresion2 + "fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
        }
    }
}
