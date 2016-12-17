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
        public List<StatementNode> declaracionDeFuncion =null;
        public override void ValidSemantic()
        {
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(identificador.identificador))
                    throw new SintanticoException("variable " + identificador.identificador + " existe fila "+identificador._TOKEN.Fila+" colunma" +identificador._TOKEN.Columna);
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
                    var referencia = new YreferenciaTipo();
                    referencia.tipoReferencia = obtenerTipo((string)tipo);
                    listaParametros.Add(referencia);
                }
                if (lista is Identificador.IdentificadorStamNode)
                {
                    Type fieldsType = typeof(Identificador.IdentificadorStamNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var tipo = fields[3].GetValue(lista);
                    var referenciaApuntador = fields[1].GetValue(lista);
                    if(referenciaApuntador != null){
                       var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                        apuntador.tipoReferencia = obtenerTipo((string)tipo);
                         listaParametros.Add(apuntador);
                    }
                    else{
                        listaParametros.Add(obtenerTipo((string)tipo));
                        
                    }
                }
                if (lista is Identificador.IdentificadorArrayNode)
                {
                    Type fieldsType = typeof(Identificador.IdentificadorArrayNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var declaracion = fields[0].GetValue(lista);
                   
                    var tipo = declaracion.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var puntero = tipo[1].GetValue(declaracion);
                    var unidimesional = fields[1].GetValue(lista);
                    var bidimesional = fields[2].GetValue(lista);
                    var array = new ArrayTipo();
                    array.tipo = obtenerTipo( (string)tipo[0].GetValue(declaracion));
                    array.unidimensional = (unidimesional != null)?true:false;
                    array.bidimensional = (bidimesional != null) ? true : false;
               
                    if (puntero != null)
                    {
                        var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                        apuntador.tipoReferencia = array;
                        listaParametros.Add(apuntador);
                    }
                    else
                    {
                        listaParametros.Add(array);

                    }
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
             if (declaracionDeFuncion.Count != 0)
             {
                 foreach (var lista in declaracionDeFuncion)
                 {
                     if (identificador.tipo == "void")
                     {
                         if (lista is ReturnNode)
                             throw new SemanticoException("la funcion " + identificador.identificador + " es una funcion void y no es de retorno");
                     }

                     lista.ValidSemantic();
                 }
                 if (!(declaracionDeFuncion.Last() is ReturnNode))
                 {
                     throw new SemanticoException("la funcion " + identificador.identificador + " debe tener una funcion de retorno ");
                 }
             }
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
            else if (tipo == "Array")
                return new ArrayTipo();
            return null;

        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
    
}

    