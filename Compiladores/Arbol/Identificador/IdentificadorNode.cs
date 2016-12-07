using Compiladores.Arbol.Accesores;
using Compiladores.Arbol.BinaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Identificador
{
    public class IdentificadorNode : ExpressionNode
    {
        public string value;
        public List<ExpressionNode> pointer;
        public List<AccesoresNode> Asesores;
        public override TiposBases ValidateSemantic()
        {
            bool existe = false;
            TiposBases tipo = null;
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(value))
                {
                    tipo = stack.GetVariable(value);
                     existe = true;
                }
                    
            };
            if(existe == false)
            throw new SemanticoException("la variable " + value + " no existe");

           if(tipo is ConstTipo){
               Type fieldsType = typeof(ConstTipo);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var tiporetorn =  fields[0].GetValue(tipo);
               return (TiposBases)tiporetorn;
           }
           if (tipo is ArrayTipo)
           {
               Type fieldsType = typeof(ArrayTipo);
               FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var unidimensional =(bool) fields[1].GetValue(tipo);
                var bidimensional = (bool)fields[2].GetValue(tipo);
               var tipodeArray =(TiposBases) fields[0].GetValue(tipo);
               if (Asesores == null)
                   return tipo;
               foreach (var accesores in Asesores)
               {
                   if (accesores is LogicaStructNode)
                       throw new SintanticoException(value+"es un arreglo no una structura");
                   if(accesores is PuntoNode)
                       throw new SintanticoException(value + "es un arreglo no una enum");
                   accesores.ValidSemantic();
               }
               if (bidimensional == true && Asesores.Count == 2)
                   return tipodeArray;
               if (bidimensional == true && Asesores.Count == 1)
                   return new ArrayTipo();
               if (bidimensional == false && Asesores.Count > 1)
                   throw new SintanticoException("el elemento no es un arreglo de mas de una dimension");
               if (bidimensional == false && Asesores.Count == 1 && unidimensional == true)
                   return tipodeArray;
               if (bidimensional == false && Asesores.Count == 0 && unidimensional == true)
                   return new ArrayTipo();
               if (bidimensional == false && Asesores.Count == 0 && unidimensional == false)
                   throw new SintanticoException("no es un arreglo ");
               if (bidimensional == true && Asesores.Count == 0)
                   return new ArrayTipo();
           }
           if (tipo is EnumTipo)
           {
               Type fieldsType = typeof(EnumTipo);
               FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
               var enumeracion = (Dictionary<string, int>)fields[0].GetValue(tipo);

               if (Asesores == null)
                   return new EnumTipo();
               foreach (var accesores in Asesores)
               {
                   if (accesores is LogicaStructNode)
                       throw new SintanticoException(value + "es un enum no una structura");
                   if (accesores is ArrayAsesorNode)
                       throw new SintanticoException(value + "es un enum no una arreglo");
               }
               if(Asesores.Count > 1)
                   throw new SintanticoException(value + " solo se puede acceder a un elemento de el enum");

               Type fieldsTypeAcessor = typeof(PuntoNode);
               FieldInfo[] fieldsAcessor = fieldsTypeAcessor.GetFields(BindingFlags.Public | BindingFlags.Instance);
               var elementoDeEnum = fieldsAcessor[0].GetValue(Asesores[0]);
               var elementoNombre = elementoDeEnum.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
               var elemento = (string)elementoNombre[0].GetValue(elementoDeEnum);
               if (elemento == null)
                   return new EnumTipo();
               var listadeAcesores = (List<AccesoresNode>)elementoNombre[2].GetValue(elementoDeEnum); 
               if (!enumeracion.ContainsKey(elemento))
               {
                   throw new SintanticoException(elemento + " no es un elemento del enum");
               }
               if (listadeAcesores != null)
               {
                   throw new SintanticoException(value + " los elementos de enum no son arreglos");
               }
               return new StringTipo();
           }
           if (tipo.GetType() is StructTipo)
           {
               Type fieldsType = typeof(StructTipo);
               FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
               Dictionary<string, TiposBases> enumeracion = (Dictionary<string, TiposBases>)fields[0].GetValue(tipo);
               if (Asesores == null)
                   return new StructTipo();
               foreach (var accesores in Asesores)
               {
                   if (accesores is PuntoNode)
                       throw new SintanticoException(value + "es un struct no una enum");
                   if (accesores is ArrayAsesorNode)
                       throw new SintanticoException(value + "es un struct no una arreglo");
               }
               if (Asesores.Count > 1)
                   throw new SintanticoException(value + " solo se puede acceder a un elemento de el struct");

               Type fieldsTypeAcessor = typeof(LogicaStructNode);
               FieldInfo[] fieldsAcessor = fieldsTypeAcessor.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var elementoDestruct = fieldsAcessor[0].GetValue(Asesores[0]);
                var elementoId = elementoDestruct.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                var id = (string)elementoId[0].GetValue(elementoDestruct);
                var acesores = (List<AccesoresNode>)elementoId[2].GetValue(elementoDestruct);
                if (enumeracion.ContainsKey(id))
                {
                          var tipoDeElemento = enumeracion[id];
                    if (tipoDeElemento is ArrayTipo)
                    {
                        Type fieldsType1 = typeof(ArrayTipo);
                        FieldInfo[] fields1 = fieldsType1.GetFields(BindingFlags.Public | BindingFlags.Instance);
                        var unidimensional = (bool)fields1[1].GetValue(tipoDeElemento);
                        var bidimensional = (bool)fields1[2].GetValue(tipoDeElemento);
                        var tipodeArray = (TiposBases)fields1[0].GetValue(tipoDeElemento);
                        if (bidimensional == true && acesores.Count == 2)
                                return tipodeArray;
                        if (bidimensional == true && acesores.Count == 1)
                            return  new ArrayTipo();
                        if (bidimensional == false && acesores.Count > 1)
                            throw new SintanticoException("el elemento no es un arreglo de mas de una dimension");
                        if (bidimensional == false && acesores.Count == 1 && unidimensional == true )
                            return tipodeArray;
                        if (bidimensional == false && acesores.Count == 0 && unidimensional == true)
                            return new ArrayTipo();
                        if (bidimensional == false && acesores.Count == 0 && unidimensional == false)
                            throw new SintanticoException("no es un arreglo ");
                        if (bidimensional == true && acesores.Count == 0)
                            return new ArrayTipo();
                    }
                    else
                    {
                        return tipoDeElemento;
                    }
                    
                }
                throw new SintanticoException(value + " no es un elemento de el struct");
           }

            return tipo;
        }
        
    }
    
}
