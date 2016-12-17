using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico.Tipos
{
    public class MulpilicadorOperdadorReferenciaTipo:TiposBases
    {
        public TiposBases tipoReferencia;
        public override Value GetDefaultValue()
        {
            return new IntValue();
        }

    }
}
