using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Identificador
{
   
    public class IdentificadorStamNode : StatementNode
    {
        public string value { get; set; }
        public List<ExpressionNode> pointer;
        public ExpressionNode inicializacion;
        public List<AccesoresNode> Asesores;
        public string tipo;
        public override void ValidSemantic()
        {
            if (tipo != null)
            {
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(value))
                        throw new SintanticoException("variable " + value + " existe");
                };
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(value, obtenerTipo(tipo));
            }
            else
            {
                if(ContenidoStack.InstanceStack.Stack.Peek()._variables.Count == 0)
                    throw new SintanticoException("variable " + value + " no existe");
                bool existe = false;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(value))
                        existe = true;
                };
                if(existe == false)
                    throw new SintanticoException("variable " + value + " no existe");
            }
            if (inicializacion != null)
            {
              var inicial = inicializacion.ValidateSemantic();
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
            else if (tipo == "struct")
                return new StructTipo();
            else if (tipo == "void")
                return new VoidTipo();
            else if (tipo == "const")
                return new VoidTipo();
            return null;

        }
    }
}
