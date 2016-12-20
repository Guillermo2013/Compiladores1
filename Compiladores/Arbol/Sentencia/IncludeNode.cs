using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class IncludeNode:StatementNode
    {
        public string direccion;
        public override void ValidSemantic()
        {
               
        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
        public override string GenerarCodigo()
        {
            string codigo = "#include ";

            if (direccion != null)
                codigo += '\"' + direccion + '\"'+ "\n";
            

            return codigo;
        }
    }
}
