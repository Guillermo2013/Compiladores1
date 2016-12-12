using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class GeneralDeclarationNode:StatementNode
    {
        public string tipo;
        public List<ExpressionNode> pointer;
        public string identificador;
        public override void ValidSemantic()
        {
            if (ContenidoStack.InstanceStack.Stack == null)
                if (pointer != null)
                {
                    var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                    apuntador.tipoReferencia = obtenerTipo(tipo);
                    ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador,apuntador );
                }
                else
                    ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador, obtenerTipo(tipo));

            else
            {
                bool existe = false;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(identificador))
                        existe = true;
                };
                if (existe == true)
                    throw new SintanticoException("variable " + identificador + " ya existe fila:"+ _TOKEN.Fila+" columna "+_TOKEN.Columna);
                if (existe == false)
                    if (pointer != null && pointer.Count>0)
                    {
                        var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                        apuntador.tipoReferencia = obtenerTipo(tipo);
                        ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador, apuntador);
                    }
                    else
                        ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador, obtenerTipo(tipo));
            }
        }
        private TiposBases obtenerTipo(String tipo)
        {
            if (tipo == "bool")
                return new BooleanTipo();
            else if (tipo == "char")
                return new CharTipo();
            else if (tipo == "date")
                return new DateTipo();
            else if (tipo == "enum")
                return new EnumTipo();
            else if (tipo == "float")
                return new FloatTipo();
            else if (tipo == "int")
                return new IntTipo();
            else if (tipo == "string")
                return new StringTipo();
            else if (tipo =="struct")
                return new StructTipo();
            else if (tipo == "void")
                return new VoidTipo();
            else if (tipo == "const")
                return new VoidTipo();
            return null;

        }

    }
}
