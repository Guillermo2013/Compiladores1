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
                throw new SemanticoException("la funcion " + identificador + " no existe fila "+ _TOKEN.Fila + "columna "+ _TOKEN.Columna);

            if(tipo is VoidTipo)
                throw new SemanticoException("la funcion " + identificador + " debe se de retorno no void fila " + _TOKEN.Fila + "columna " + _TOKEN.Columna);
            
            
                Type fieldsType = typeof(FuncionTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var Lista = fields[0].GetValue(tipo);
                var tipoRetorno = fields[1].GetValue(tipo);
                var ListaFinal = (List<TiposBases>)Lista;
                if (ListaDeParametros.Count != ListaFinal.Count)
                {
                    throw new SemanticoException("la cantidad de parametros no es la misma fila " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                }
                for (int index = 0; index < ListaDeParametros.Count; index++)
                {
                    if (ListaDeParametros[index] is YporBitNode)
                    {
                        if (!(ListaFinal[index] is MulpilicadorOperdadorReferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                        Type fieldsTypeReferencia = typeof(YporBitNode);
                        FieldInfo[] fieldsReferencia = fieldsTypeReferencia.GetFields(BindingFlags.Public | BindingFlags.Instance);
                        var operadorReferencia = fieldsReferencia[1].GetValue(ListaDeParametros[index]);
                        var tipofinarRefencia = operadorReferencia.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0].GetValue(operadorReferencia);
                        TiposBases tipoLLamarFuncion = null;
                        foreach (var stack in ContenidoStack.InstanceStack.Stack)
                        {
                            if (stack.VariableExist((string)tipofinarRefencia))
                                tipoLLamarFuncion = stack.GetVariable((string)tipofinarRefencia);
                        }


                        Type fieldsTypeReferencia2 = typeof(MulpilicadorOperdadorReferenciaTipo);
                        FieldInfo[] fieldsReferencia2 = fieldsTypeReferencia2.GetFields(BindingFlags.Public | BindingFlags.Instance);
                        var operadorReferencia2 = fieldsReferencia2[0].GetValue(ListaFinal[index]);
                        if (tipoLLamarFuncion.GetType() != operadorReferencia2.GetType())
                        {
                            throw new SemanticoException(" el tipo de los parametros no es correcto" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                        if (tipoLLamarFuncion is ArrayTipo)
                        {
                            var tipodeArreglo2 = operadorReferencia2.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0].GetValue(operadorReferencia2);
                            Type fieldsTypeArreglo1 = typeof(ArrayTipo);
                            FieldInfo[] fieldsArreglos = fieldsTypeArreglo1.GetFields(BindingFlags.Public | BindingFlags.Instance);
                            var arreglo1 = fieldsArreglos[0].GetValue(tipoLLamarFuncion);
                            if (arreglo1.GetType() != tipodeArreglo2.GetType())
                            {
                                throw new SemanticoException(" el tipo de los los arreglo de parametros no es correcto" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                            }
                        }

                    }
                    else if (ListaDeParametros[index] is OperdadorMultiplicacionUnaryNode)
                    {
                        if (!(ListaFinal[index] is YreferenciaTipo))
                        {
                            throw new SemanticoException(" los paramentos son por refencia no por valor " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                        Type fieldsTypeReferencia = typeof(OperdadorMultiplicacionUnaryNode);
                        FieldInfo[] fieldsReferencia = fieldsTypeReferencia.GetFields(BindingFlags.Public | BindingFlags.Instance);
                        var operadorReferencia = fieldsReferencia[1].GetValue(ListaDeParametros[index]);
                        var tipofinarRefencia = operadorReferencia.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0].GetValue(operadorReferencia);
                        TiposBases tipoLLamarFuncion = null;
                        foreach (var stack in ContenidoStack.InstanceStack.Stack)
                        {
                            if (stack.VariableExist((string)tipofinarRefencia))
                                tipoLLamarFuncion = stack.GetVariable((string)tipofinarRefencia);
                        }


                        Type fieldsTypeReferencia2 = typeof(YreferenciaTipo);
                        FieldInfo[] fieldsReferencia2 = fieldsTypeReferencia2.GetFields(BindingFlags.Public | BindingFlags.Instance);
                        var operadorReferencia2 = fieldsReferencia2[0].GetValue(ListaFinal[index]);
                        if (tipoLLamarFuncion.GetType() != operadorReferencia2.GetType())
                        {
                            throw new SemanticoException(" el tipo de los parametros no es correcto" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                        }
                        if (tipoLLamarFuncion is ArrayTipo)
                        {
                            var tipodeArreglo2 = operadorReferencia2.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0].GetValue(operadorReferencia2);
                            Type fieldsTypeArreglo1 = typeof(ArrayTipo);
                            FieldInfo[] fieldsArreglos = fieldsTypeArreglo1.GetFields(BindingFlags.Public | BindingFlags.Instance);
                            var arreglo1 = fieldsArreglos[0].GetValue(tipoLLamarFuncion);
                            if (arreglo1.GetType() != tipodeArreglo2.GetType())
                            {
                                throw new SemanticoException(" el tipo de los los arreglo de parametros no es correcto" + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                            }
                        }


                    }
                    else if (ListaFinal[index].GetType() != ListaDeParametros[index].ValidateSemantic().GetType())
                    {
                        throw new SemanticoException(identificador + "el orden de la declaracion es incorrenta debe ser o el tipo " + ListaDeParametros+" fila " + ListaDeParametros[0]._TOKEN.Fila + "columna " + ListaDeParametros[0]._TOKEN.Columna);
                    }
                }

                return (TiposBases)tipoRetorno;
        }
    }
}
