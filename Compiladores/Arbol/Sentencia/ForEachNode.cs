using Compiladores.Implementacion;
using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class ForEachNode:StatementNode
    {
        public GeneralDeclarationNode inicializacionForEach;
        public string ListaForEach;
        public List<StatementNode> BloqueCondicionalForEach;
        public override void ValidSemantic()
        {
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
                if (stack.VariableExist(ListaForEach))
                {
                    if (!(stack.GetVariableValue(ListaForEach) is ArrayValue))
                        throw new Semantico.SemanticoException("la lista tiene que ser un arreglo fila" + _TOKEN.Fila + "columna " + _TOKEN.Columna);
                }
            foreach (var sentencia in BloqueCondicionalForEach){
                sentencia.ValidSemantic();
            }
        }
        public override void Interpret()
        {

        }
        public override string GenerarCodigo()
        {
            string codigo = "foreach ( ";
            if (inicializacionForEach.tipo != null)
                codigo += inicializacionForEach.tipo ;
            if (inicializacionForEach.identificador != null)
                codigo += inicializacionForEach.identificador + " : ";
            if (ListaForEach != null)
                codigo += ListaForEach + "){\n";
            foreach (var lista in BloqueCondicionalForEach)
                codigo += lista.GenerarCodigo() + "\n";
            codigo += "};\n";

            return codigo;
        }
    }
}
