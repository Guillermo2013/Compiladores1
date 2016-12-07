using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class FuntionDeclarationNode: StatementNode
    {
        public GeneralDeclarationNode identificador;
        public List<StatementNode> paramentros;
        public List<StatementNode> declaracionDeFuncion;
        public override void ValidSemantic()
        {
            identificador.ValidSemantic();
            ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
            foreach (var paramentrosLista in paramentros)
                paramentrosLista.ValidSemantic();
            foreach (var sentencia in declaracionDeFuncion)
                sentencia.ValidSemantic();
            ContenidoStack.InstanceStack.Stack.Pop();

        }
    }
}
