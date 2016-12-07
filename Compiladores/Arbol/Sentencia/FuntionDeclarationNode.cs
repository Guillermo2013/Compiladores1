using Compiladores.Arbol.UnaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(identificador.identificador))
                    throw new SintanticoException("variable " + identificador + " existe");
            }
             List<TiposBases> listaParametros = new List<TiposBases>();
           if(paramentros != null)
            foreach (var lista in paramentros)
            {
                if (lista is YporBitStamentNode)
                {
                    Type fieldsType = typeof(YporBitStamentNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var identificadorNode = fields[1].GetValue(lista);
                    var identificadorOperando = identificadorNode.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var tipo = identificadorOperando[3].GetValue(identificadorNode);
                    listaParametros.Add(obtenerTipo((string)tipo));
                }
                if (lista is Identificador.IdentificadorStamNode)
                {
                    Type fieldsType = typeof(Identificador.IdentificadorStamNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var tipo = fields[3].GetValue(lista);
                    listaParametros.Add(obtenerTipo((string)tipo));
                }
            }
            
            if (identificador.tipo == "void")
            {
                var funcion = new VoidTipo();
                if (paramentros != null)
                    funcion.listaParametros = listaParametros;
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador.identificador, funcion);
            }
            else
            {
                var funcion = new FuncionTipo();
                if (paramentros != null)
                    funcion.listaParametros = listaParametros;
                funcion.retorno = obtenerTipo(identificador.tipo);
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador.identificador, funcion);
            }
             ContenidoStack.InstanceStack.Stack.Push(new TablaSimbolos());
             if (paramentros != null)
                 foreach (var lista in paramentros)
                     lista.ValidSemantic();
             if (declaracionDeFuncion != null)
                 foreach (var lista in declaracionDeFuncion)
                     lista.ValidSemantic();
             ContenidoStack.InstanceStack.Stack.Pop();
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
    