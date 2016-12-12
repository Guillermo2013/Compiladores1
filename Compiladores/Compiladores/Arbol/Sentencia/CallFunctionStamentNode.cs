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
                var ListaFinal = (List<TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma fila" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaDeParametros[index] is YporBitNode)
                    {
                        if (!(ListaFinal[index].GetType() is OperdadorMultiplicacionUnaryNode))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                    }else if (ListaFinal[index].GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {
                        throw new SemanticoException(identificador +"el orden de la declaracion es incorrenta debe ser "+ ListaDeParametros);
                    }
                }

            }
            else if (tipo is VoidTipo)
            {
                Type fieldsType = typeof(VoidTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var Lista = fields[0].GetValue(tipo);
                var ListaFinal = (List<TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma funcion" + identificador);
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaFinal[index].GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {
                        throw new SemanticoException(identificador+" el orden de la declaracion es incorrenta debe ser "+ ListaFinal);
                    }
                }
            }
            
        }
    }
}
