﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class EnumNode:StatementNode
    {
        public string identificador;
        public List<ExpressionNode> ListaEnum;
        public override void ValidSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
