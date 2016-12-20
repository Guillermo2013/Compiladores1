using Compiladores.Arbol.Identificador;
using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.UnaryOperador
{
    public class AutoOperacionIncrementoPos:UnaryOperadorNode
    {
        public override TiposBases ValidateSemantic()
        {
            var expresion = Operando.ValidateSemantic();
            if (!(Operando is IdentificadorNode))
                throw new SemanticoException("no se puede autoIncrementarLiterales literales  fila " + _TOKEN.Fila + " columna " + _TOKEN.Columna);
            if (expresion is FloatTipo || expresion is IntTipo || expresion is CharTipo || expresion is EnumTipo)
                return expresion;
            throw new Sintactico.SintanticoException("este tipo no puede decrementarse fila" + Operando._TOKEN.Fila + " columna " + Operando._TOKEN.Columna);
        }
        public override Implementacion.Value Interpret()
        {
            Value valorID = null;
            string nombre = " ";
            if (Operando is IdentificadorNode)
            {
                nombre = (Operando as IdentificadorNode).value;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                    if (stack.VariableExist(nombre))
                    {
                        valorID = stack.GetVariableValue(nombre);
                        if (valorID is IntValue)
                            stack.SetVariableValue(nombre, new IntValue { Value = (valorID as IntValue).Value++ });
                        if (valorID is FloatValue)
                            stack.SetVariableValue(nombre, new FloatValue { Value = (valorID as FloatValue).Value++ });
                        if (valorID is CharValue)
                            stack.SetVariableValue(nombre, new CharValue { Value = (valorID as CharValue).Value++ });  
                    }
            }
          
            return valorID;
        }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (Operando != null)
                codigo = Operando.GenerarCodigo() + "++";
            return codigo;
        }
        }
    }

