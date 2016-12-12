using Compiladores.Arbol.BinaryOperador;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Compiladores.Arbol.Identificador;

namespace Compiladores.Arbol.Sentencia
{
    public class StructNode : StatementNode
    {
        public string identificador;
        public List<StatementNode> bloqueStruct;
        public List<ExpressionNode> pointer;
        public string variableNombre;
        public List<ExpressionNode> asignacion;
        public override void ValidSemantic()
        {
            var existeIdentificador = false;
            Dictionary<string, TiposBases> elementosHeredados = new Dictionary<string, TiposBases>();
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(variableNombre))
                    throw new SintanticoException("variable " + variableNombre + " existe " + _TOKEN.Fila + " columnas " + _TOKEN.Columna);
            };
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                foreach (var tipo in stack._variables.ToList())
                {
                    if (tipo.Value is StructTipo)
                    {
                      Type fieldsType = typeof(StructTipo);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public  | BindingFlags.Instance);
                    var identificadorNode = fields[0].GetValue(tipo.Value);
                       if((string)identificadorNode == identificador){

                        if (bloqueStruct != null)
                            throw new SintanticoException("variable " + identificador + " existe no se puede definir el struct " + _TOKEN.Fila + " columnas " + _TOKEN.Columna);
                       
                            elementosHeredados = (Dictionary<string, TiposBases>)fields[1].GetValue(tipo.Value);
                            existeIdentificador = true;
                       }
                    }
                }
             };

            if (bloqueStruct == null)
            if (ContenidoStack.InstanceStack.Stack.ToList()[0]._variables.Count != 0)
                if (existeIdentificador == false)
                    throw new SintanticoException("variable " + this.identificador + " no existe el struct " + _TOKEN.Fila + " columnas " + _TOKEN.Columna);
                       

            Dictionary<string, TiposBases> elementos = new Dictionary<string, TiposBases>();
            if (bloqueStruct != null)
            foreach (var lista in bloqueStruct) {
                if (lista is IdentificadorArrayNode)
                {
                    Type fieldsType = typeof(IdentificadorArrayNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public  | BindingFlags.Instance);
                    var identificadorNode = fields[0].GetValue(lista);
                    var unidimesional = fields[1].GetValue(lista);
                    var bidimesional = fields[2].GetValue(lista);
                    var tipo = identificadorNode.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0].GetValue(identificadorNode);
                    var identificador =  identificadorNode.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[2].GetValue(identificadorNode);
                    var uniarray = (unidimesional !=null)?true:false;
                    var biarray = (bidimesional != null) ? true : false;
                    var array = new ArrayTipo();
                    array.tipo = obtenerTipo((string)tipo);
                    array.unidimensional = uniarray;
                    array.bidimensional = biarray;
                    elementos.Add((string)identificador,array);
                }
               if (lista is GeneralDeclarationNode)
                {
                    Type fieldsType = typeof(GeneralDeclarationNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    var Tipo = fields[0].GetValue(lista);
                    var identificador = fields[2].GetValue(lista);
                    elementos.Add((string)identificador, obtenerTipo((string)Tipo));
                }
             }

            var structTipo = new StructTipo();
            if (elementosHeredados.Count == 0)
                structTipo.elementos = elementos;
            else
                structTipo.elementos = elementosHeredados;
            structTipo.identificadorStruct = this.identificador;
            ContenidoStack.InstanceStack.Stack.Peek()._variables.Add(variableNombre, structTipo);
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
        
    


