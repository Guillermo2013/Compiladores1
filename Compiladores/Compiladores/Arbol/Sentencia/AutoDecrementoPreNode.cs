using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class AutoDecrementoPreNode:StatementNode
    {
        public ExpressionNode operadador;
        public override void ValidSemantic()
        {
            var operador = operadador.ValidateSemantic();
            if (!(operador is IntTipo))
                throw new Semantico.SemanticoException("el tipo del identificador tiene que ser Int fila " + _TOKEN.Fila + " colummna " + _TOKEN.Columna);
        }
    }
}
