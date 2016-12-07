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
    public class ConstNode: StatementNode
    {
        public string identificador { get; set; }
        public List<ExpressionNode> pointer;
        public ExpressionNode expresion;
        public string Tipo;
        public override void ValidSemantic()
        {
            if (ContenidoStack.InstanceStack.Stack == null)
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador, obtenerTipo(Tipo));

            else
            {
                bool existe = false;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(identificador))
                        existe = true;
                };
                if (existe == true)
                    throw new SintanticoException("variable " + identificador + " ya existe");
                if (existe == false) {
                    if (expresion != null)
                    {
                        var tipoevaluar = obtenerTipo(Tipo);
                        var expresionTipo = expresion.ValidateSemantic();
                        if (tipoevaluar.GetType() != expresionTipo.GetType())
                            throw new SintanticoException(" no se puede asignar "+tipoevaluar + " con "+expresionTipo);
                        var constTipo = new ConstTipo();
                        constTipo.tipo = obtenerTipo(Tipo);
                        ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador, constTipo);
                    }
                    else
                    {
                        throw new SintanticoException("la constantes se debe inicializar ");
                    }
                    
                    
                }
                    
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
          
            return null;

        }
    }
}
