using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class EnumNode:StatementNode
    {
        public string identificador;
        public List<ExpressionNode> ListaEnum;
        public override void ValidSemantic()
        {
            if (!TablaSimbolos.Instance.VariableExist(identificador))
                TablaSimbolos.Instance.DeclareVariable(identificador, new EnumTipo());
            else
                throw new Sintactico.SintanticoException("la variable " + identificador + "ya existe ");
        }
    }
}
