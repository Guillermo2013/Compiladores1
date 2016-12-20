using Compiladores.Arbol.Identificador;
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
             
            Dictionary<string, TiposBases> listaParametros = new Dictionary<string, TiposBases>();
        
           if(paramentros != null)
            foreach (var lista in paramentros)
            {
                if (lista is YporBitStamentNode)
                {

                    var identificadorNode = (lista as YporBitStamentNode).OperandoStament;
                    var tipo = (identificadorNode as IdentificadorStamNode).tipo;
                    var referencia = new YreferenciaTipo();
                    referencia.tipoReferencia = obtenerTipo(tipo);
                    listaParametros.Add( (identificadorNode as IdentificadorStamNode).value, referencia);
                }
                if (lista is IdentificadorStamNode)
                {

                    var tipo = (lista as IdentificadorStamNode).tipo;
                    var referenciaApuntador = (lista as IdentificadorStamNode).pointer;
                    if(referenciaApuntador != null){
                       var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                        apuntador.tipoReferencia = obtenerTipo((string)tipo);
                         listaParametros.Add((lista as IdentificadorStamNode).value , apuntador);
                    }
                    else{
                        listaParametros.Add((lista as IdentificadorStamNode).value,obtenerTipo(tipo));
                        
                    }
                }
                if (lista is IdentificadorArrayNode)
                {

                    var declaracion = (lista as IdentificadorArrayNode).identificador;

                    var tipo = (declaracion as GeneralDeclarationNode).tipo;
                    var puntero = (declaracion as GeneralDeclarationNode).pointer;
                    var unidimesional = (lista as IdentificadorArrayNode).unidimesionarioArray;
                    var bidimesional = (lista as IdentificadorArrayNode).BidimesionarioArray;
                    var array = new ArrayTipo();
                    array.tipo = obtenerTipo(tipo);
                    array.unidimensional = (unidimesional != null)?true:false;
                    array.bidimensional = (bidimesional != null) ? true : false;
               
                    if (puntero != null)
                    {
                        var apuntador = new MulpilicadorOperdadorReferenciaTipo();
                        apuntador.tipoReferencia = array;
                        listaParametros.Add((declaracion as GeneralDeclarationNode).identificador, apuntador);
                    }
                    else
                    {
                        listaParametros.Add((declaracion as GeneralDeclarationNode).identificador,array);

                    }
                }
                

            }
            
            if (identificador.tipo == "void")
            {
                var funcion = new VoidTipo();
                if (paramentros != null)
                    funcion.listaParametros = listaParametros;
                if (declaracionDeFuncion != null)
                    funcion.sentencias = declaracionDeFuncion;
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador.identificador, funcion);
            }
            else
            {
                var funcion = new FuncionTipo();
                if (paramentros != null)
                    funcion.listaParametros = listaParametros;
                funcion.retorno = obtenerTipo(identificador.tipo);
                funcion.sentencias = declaracionDeFuncion;
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
                 if(identificador.tipo != "void")
                 if (!(declaracionDeFuncion[declaracionDeFuncion.Count-1] is ReturnNode))
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
            
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            
            if (identificador != null)
                codigo += identificador.GenerarCodigo() + " (";
            if(paramentros !=null)
            foreach (var lista in paramentros)
                codigo += lista.GenerarCodigo() + " ";
            codigo += "){";
            foreach (var lista in declaracionDeFuncion)
                codigo += lista.GenerarCodigo() + "\n ";
            codigo += "};";
            return codigo;
        }
    }
    
}

    