﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol
{
     public class AccesoresNode:StatementNode
    {
        public string valor { get; set; }
        public StatementNode OperadorDerecho;
        public StatementNode OperadorIzquierdo;
    }
}
