﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores
{
    class Token
    {
        public TokenTipos Tipo { get; set; }
        public string Lexema { get; set; }
        public int Columna { get; set; }

        public override string ToString()
        {
            return "Lexema: " + Lexema + " Tipo: " + Tipo + " Columna: " + Columna;
        }
    }
}
