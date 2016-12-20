using Compiladores.Arbol;
using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class FuncionTipo:TiposBases
    {
        public Dictionary<string, TiposBases> listaParametros = new Dictionary<string, TiposBases>();
        public TiposBases retorno;
        public List<StatementNode> sentencias = new List<StatementNode>();
        public Value valorRetorno;
        public override string ToString()
        {
            return "Funcion";
        }
        public override Value GetDefaultValue()
        {
            return null;
        }
    }
}
