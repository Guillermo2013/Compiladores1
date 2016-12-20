using Compiladores.Arbol.Identificador;
using Compiladores.Implementacion;
using Compiladores.Semantico;
using Compiladores.Semantico.Tipos;
using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiladores.Arbol.Sentencia
{
    public class ReadNode:StatementNode
    {
        public IdentificadorNode Variable { get; set; }

        public override void ValidSemantic()
        {
            Variable.ValidateSemantic();
        }

        public override void Interpret()
        {
            var value = Console.ReadLine();
            Value tipo = null;
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (stack.VariableExist(Variable.value))
                    tipo = stack.GetVariableValue(Variable.value);


            }
            foreach (var stack in ContenidoStack.InstanceStack.Stack)
            {
                if (tipo is StringValue)
                   stack.SetVariableValue(Variable.value, new StringValue { Value = value });
                if (tipo is IntValue)
                    stack.SetVariableValue(Variable.value, new IntValue { Value = int.Parse(value) });
                if (tipo is FloatValue)
                    stack.SetVariableValue(Variable.value, new FloatValue { Value = float.Parse(value) });
                if (tipo is CharValue)
                    stack.SetVariableValue(Variable.value, new CharValue { Value = char.Parse(value) });
                if (tipo is BoolValue)
                    stack.SetVariableValue(Variable.value, new BoolValue { Value = bool.Parse(value) });
                if (tipo is DateValue)
                    stack.SetVariableValue(Variable.value, new DateValue { Value = DateTime.Parse(value) });
            }
        }
        public override string GenerarCodigo()
        {
            string codigo = "";

            return codigo;
        }
    }
}
