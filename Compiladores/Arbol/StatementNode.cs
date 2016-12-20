using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol
{
    public abstract class StatementNode
    {
        public abstract void ValidSemantic();
        public Token _TOKEN = null;
        public abstract void Interpret();
        public abstract string GenerarCodigo();
    }
}
