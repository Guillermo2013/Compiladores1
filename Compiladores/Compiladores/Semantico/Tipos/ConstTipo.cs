﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class ConstTipo:TiposBases
    {
        public TiposBases tipo=null;
        public override string ToString()
        {
            return "Const";
        }
    }
}
