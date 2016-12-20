using Compiladores.Arbol.Identificador;
using Compiladores.Arbol.UnaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class CallFunctionStamentNode:StatementNode
    {
        public string identificador;
        public List<ExpressionNode> ListaDeParametros;

        public override void ValidSemantic()
        {

            
            TiposBases tipo = null;

            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(identificador))
                    tipo =  stack.GetVariable(identificador);
            };
            if(tipo == null)
                throw new SemanticoException("la funcion " + identificador + " no existe fila "+ _TOKEN.Fila+ " columna "+ _TOKEN.Columna);

            if (tipo is FuncionTipo)
            {
                Type fieldsType = typeof(FuncionTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var Lista = fields[0].GetValue(tipo);
                var ListaFinal = (Dictionary<string,TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma fila" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaDeParametros[index] is YporBitNode)
                    {
                        if (!(ListaFinal.ToList()[index].Value  is MulpilicadorOperdadorReferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }

                    }else if(ListaDeParametros[index] is OperdadorMultiplicacionUnaryNode){
                        if (!(ListaFinal.ToList()[index].Value  is YreferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                            
                    }else if (ListaFinal.ToList()[index].Value.GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {
                       
                        throw new SemanticoException( "el orden de la declaracion es incorrenta debe ser " + ListaDeParametros);
                    }
                }

            }
            else if (tipo is VoidTipo)
            {
                Type fieldsType = typeof(VoidTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var Lista = fields[0].GetValue(tipo);
                var ListaFinal = (Dictionary<string, TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma funcion" + identificador);
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaDeParametros[index] is YporBitNode)
                    {
                        if (!(ListaFinal.ToList()[index].Value is MulpilicadorOperdadorReferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }

                    }
                    else if (ListaDeParametros[index] is OperdadorMultiplicacionUnaryNode)
                    {
                        if (!(ListaFinal.ToList()[index].Value is YreferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }

                    }
                    else if (ListaFinal.ToList()[index].Value.GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {

                        throw new SemanticoException("el orden de la declaracion es incorrenta debe ser " + ListaDeParametros);
                    }
                }
            }
           
            
        }
        public override void Interpret()
        {
             TiposBases tipo = null;
             foreach (var stack in ContenidoStack.InstanceStack.Stack)
                 if (stack.VariableExist(identificador))
                     tipo = stack.GetVariable(identificador);


              Dictionary<string, TiposBases> paramentros = (tipo is VoidTipo) ? (tipo as VoidTipo).listaParametros : (tipo as FuncionTipo).listaParametros;
              List<StatementNode> sentencias = (tipo is VoidTipo) ? (tipo as VoidTipo).sentencias : (tipo as FuncionTipo).sentencias;
                 foreach (var parametro in paramentros)
                 {
                      ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(parametro.Key, parametro.Value);
                 }
                 var i = 0;
                 foreach (var parametrosInicial in ListaDeParametros)
                 {
                     var parametro = parametrosInicial;
                     var parametroFuncion = paramentros.ToList()[i];
                     if(parametro is IdentificadorNode)
                     ContenidoStack.InstanceStack.Stack.Peek().SetVariableValue(paramentros.ToList()[i].Key, (parametro as IdentificadorNode).Interpret()); 
                         i++;
                 }
                 foreach (var sentencia in sentencias)
                 {
                     
                     sentencia.Interpret();

                 }
                 

                 
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (identificador != null)
                codigo += identificador + "(";
            foreach (var lista in ListaDeParametros)
                codigo += lista.GenerarCodigo();
            codigo += ")";

            codigo += ";\n";
            return codigo;
        }
    }
}
