using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Accesores
{
    public class ArrayAsesorNode:AccesoresNode
    {
        public ExpressionNode tamaño { get; set; }
        public override void ValidSemantic()
        {
            var expresionTamaño = tamaño.ValidateSemantic();
            if (!(expresionTamaño is IntTipo))
                throw new Sintactico.SintanticoException("debe ser una expresion numerica entera fila "+ tamaño._TOKEN.Fila +" columna "+tamaño._TOKEN.Columna );
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (tamaño != null)
                codigo = "[" + tamaño.GenerarCodigo() + "]"; 
            return codigo;
        }
    }
}
