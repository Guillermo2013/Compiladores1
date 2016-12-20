using Compiladores.Arbol.BinaryOperador;
using Compiladores.Arbol.TiposDeDatos;
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
    public class EnumNode:StatementNode
    {
        public string identificador;
        public List<ExpressionNode> ListaEnum;
        public override void ValidSemantic()
        {

            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(identificador))
                    throw new SintanticoException("variable " + identificador + " existe fila "+ _TOKEN.Fila +" columna "+ _TOKEN.Columna);
            };
            Dictionary<string, int> elementos = new Dictionary<string, int>();
            foreach (var lista in ListaEnum)
            {
                if (lista is Identificador.IdentificadorNode)
                {
                    Type fieldsType = typeof(Identificador.IdentificadorNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var identificadorElemento = fields[0].GetValue(lista);
                    elementos.Add((string)identificadorElemento, 0);
                }
                if (lista is AsignacionNode)
                {
                     
                    Type fieldsType = typeof(AsignacionNode);
                    FieldInfo[] fields = fieldsType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    var indexNombre = fields[1].GetValue(lista);
                    var indexNumero = fields[0].GetValue(lista);
                    var nombre = indexNombre.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)[0].GetValue(indexNombre);
                    var numero = indexNumero.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)[0].GetValue(indexNumero);
                    elementos.Add((string)nombre,(int) numero);
                }
                
            }
            var enumInstance = new EnumTipo();
            enumInstance.elementos = elementos;
            ContenidoStack.InstanceStack.Stack.Peek()._variables.Add(identificador, enumInstance);
           
        }
        public override void Interpret()
        {
            
        }
        public override string GenerarCodigo()
        {
            string codigo = "enum";
            if (identificador != null)
                codigo += identificador+"{";
            foreach (var lista in ListaEnum)
                codigo += lista.GenerarCodigo() + "\n";
            codigo += "};\n";
            
              return codigo;
        }
    }
}
