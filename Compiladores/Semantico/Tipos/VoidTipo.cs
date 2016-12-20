using Compiladores.Arbol;
using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class VoidTipo:TiposBases
    {
        public Dictionary<string, TiposBases> listaParametros = new Dictionary<string, TiposBases>();
        public List<StatementNode> sentencias = new List<StatementNode>();
        public override string ToString()
        {
            return "Void";
        }
        public override Value GetDefaultValue()
        {
            return new IntValue();
        }

    }
}
