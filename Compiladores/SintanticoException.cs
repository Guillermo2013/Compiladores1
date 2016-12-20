using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Sintactico
{
    class SintanticoException : Exception
    {
        public SintanticoException() { }
        public SintanticoException(string mensaje)
            : base(mensaje)
        {
        }
    }
}