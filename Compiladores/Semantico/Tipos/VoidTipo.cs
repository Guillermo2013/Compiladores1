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
        public List<TiposBases> listaParametros = new List<TiposBases>();
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
