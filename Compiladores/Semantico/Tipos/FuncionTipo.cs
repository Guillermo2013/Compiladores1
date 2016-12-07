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

        public override string ToString()
        {
            return "Funcion";
        }
    }
}
