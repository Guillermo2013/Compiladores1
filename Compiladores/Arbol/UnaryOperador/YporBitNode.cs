using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class YporBitNode : UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var identificador = Operando.ValidateSemantic();
            var tipo = new YreferenciaTipo();
            tipo.tipoReferencia = identificador;
            return tipo;
        }
        public override Implementacion.Value Interpret()
        {
            throw new NotImplementedException();
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (Operando != null)
                codigo = "&" + Operando.GenerarCodigo();
            return codigo;
        }
    }
}
