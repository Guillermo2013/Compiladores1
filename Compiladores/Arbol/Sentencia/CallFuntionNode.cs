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
    public class CallFuntionNode:ExpressionNode
    {
        public string identificador;
        public List<ExpressionNode> ListaDeParametros;
        public override TiposBases ValidateSemantic()
        {
            TiposBases tipo = null;
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(identificador))
                    tipo = stack.GetVariable(identificador);
            };
            if (tipo == null)
                throw new SemanticoException("la funcion " + identificador + " no existe");

            if(tipo is VoidTipo)
                throw new SemanticoException("la funcion " + identificador + " debe se de retorno no void");
            
            
                Type fieldsType = typeof(FuncionTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var Lista = fields[0].GetValue(tipo);
                var tipoRetorno = fields[1].GetValue(tipo);
                var ListaFinal = (List<TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma");
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaFinal[index].GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {
                        throw new SemanticoException(identificador + "el orden de la declaracion es incorrenta debe ser " + ListaDeParametros);
                    }
                }

                return (TiposBases)tipoRetorno;
        }
    }
}
