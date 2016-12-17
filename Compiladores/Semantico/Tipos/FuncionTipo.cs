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
        public List<TiposBases> listaParametros = new List<TiposBases>();
        public TiposBases retorno = null;
        public List<StatementNode> sentencias = new List<StatementNode>();

        public override string ToString()
        {
            return "Funcion";
        }
        public override Value GetDefaultValue()
        {
            return new IntValue();
        }
    }
}
