using Compiladores.Arbol.Accesores;
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
                        throw new SintanticoException("variable " + value + " existe+ fila:"+ _TOKEN.Fila+ " columna " + _TOKEN.Columna);
                };
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(value, obtenerTipo(tipo));
            }
            else
            {
                if(ContenidoStack.InstanceStack.Stack.Peek()._variables.Count == 0)
                    throw new SintanticoException("variable " + value + " no existe" + _TOKEN.Fila + " columna " + _TOKEN.Columna);
                bool existe = false;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(value))
                    {
                            existe = true;
                            tipo = stack.GetVariable(value).ToString();
                    }
                        
                };
                if(existe == false)
                    throw new SintanticoException("variable " + value + " no existe" + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            }
            TiposBases tipoAasignar = obtenerTipo(tipo);
            if (obtenerTipo(tipo) is ConstTipo)
                throw new SintanticoException("variable " + value + " es constante no se puede asignar" + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if (obtenerTipo(tipo) is ArrayTipo)
            {
                Type fieldsType = typeof(ArrayTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var unidimensional = (bool)fields[1].GetValue(tipo);
                var bidimensional = (bool)fields[2].GetValue(tipo);
                var tipodeArray = (TiposBases)fields[0].GetValue(tipo);
                foreach (var accesores in Asesores)
                {
                    if (accesores is LogicaStructNode)
                        throw new SintanticoException(value + "es un arreglo no una structura");
                    if (accesores is PuntoNode)
                        throw new SintanticoException(value + "es un arreglo no una enum");
                    accesores.ValidSemantic();
                }
                if (bidimensional == true && Asesores.Count == 2)
                    tipoAasignar = tipodeArray;
                if (bidimensional == true && Asesores.Count == 1)
                    tipoAasignar=  new ArrayTipo();
                if (bidimensional == false && Asesores.Count > 1)
                    throw new SintanticoException("el elemento " + value + " no es un arreglo de mas de una dimension fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
                if (bidimensional == false && Asesores.Count == 1 && unidimensional == true)
                    tipoAasignar= tipodeArray;
                if (bidimensional == false && Asesores.Count == 0 && unidimensional == true)
                    tipoAasignar=  new ArrayTipo();
                if (bidimensional == false && Asesores.Count == 0 && unidimensional == false)
                    throw new SintanticoException("el elemento " + value + " no es un arreglo");
                if (bidimensional == true && Asesores.Count == 0)
                    tipoAasignar= new ArrayTipo();
            }
            if (obtenerTipo(tipo) is StructTipo)
            {
                var structAhora = obtenerTipo(tipo);
                Type fieldsType = typeof(StructTipo);
                FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                var enumeracion = (Dictionary<string, TiposBases>)fields[0].GetValue(structAhora);

                if (Asesores == null)
                    tipoAasignar = new StructTipo();
                foreach (var accesores in Asesores)
                {
                  
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
                            tipoAasignar = tipodeArray;
                        if (bidimensional == true && acesores.Count == 1)
                            tipoAasignar = new ArrayTipo();
                        if (bidimensional == false && acesores.Count > 1)
                            throw new SintanticoException("el elemento no es un arreglo de mas de una dimension");
                        if (bidimensional == false && acesores.Count == 1 && unidimensional == true)
                            tipoAasignar = tipodeArray;
                        if (bidimensional == false && acesores.Count == 0 && unidimensional == true)
                            tipoAasignar = new ArrayTipo();
                        if (bidimensional == false && acesores.Count == 0 && unidimensional == false)
                            throw new SintanticoException("no es un arreglo ");
                        if (bidimensional == true && acesores.Count == 0)
                            tipoAasignar = new ArrayTipo();
                    }
                    else
                    {
                        tipoAasignar = tipoDeElemento;
                    }

                }
            }
            if (inicializacion != null)
            {
              var inicial = inicializacion.ValidateSemantic();
              if (tipoAasignar.GetType() != inicial.GetType())
                   throw new SintanticoException(" no se puede asignar " + tipo + " con "+inicial);
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
                return new ConstTipo();
            return null;

        }
        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
