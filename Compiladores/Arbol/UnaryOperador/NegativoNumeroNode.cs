﻿using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class NegativoNumeroNode:UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (expresion is IntTipo || expresion is FloatTipo)
                return expresion;
            throw new SemanticoException("se esperaba un literal numerica " + Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            var expresion = Operando.Interpret();
            if (expresion is IntValue)
                return new IntValue { Value = -(expresion as IntValue).Value };
            if (expresion is FloatValue)
                return new FloatValue { Value = -(expresion as FloatValue).Value };
            return null;
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (Operando != null)
                codigo = "-" + Operando.GenerarCodigo();
            return codigo;
        }
    }
}
