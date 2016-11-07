using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiladores.Sematico
{
    class SintanticoException : Exception
    {
        public SintanticoException() { }
        public SintanticoException(string mensaje) : base(mensaje) { }

    }
}
