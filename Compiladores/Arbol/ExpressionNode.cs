using Compiladores.Implementacion;
using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol
{
    public abstract class ExpressionNode
    {
        public abstract TiposBases ValidateSemantic();
        public Token _TOKEN;
        public abstract Value Interpret();
        public abstract string GenerarCodigo(); 

    }
}
