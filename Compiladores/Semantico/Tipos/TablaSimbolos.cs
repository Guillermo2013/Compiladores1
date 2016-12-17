using Compiladores.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Compiladores.Semantico.Tipos
{
   
    public class TablaSimbolos
    {
        public static TablaSimbolos _instance = null;
        public Dictionary<string, TiposBases> _variables;
        public Dictionary<string, Value> _values;
        public TablaSimbolos()
        {
            _variables = new Dictionary<string, TiposBases>();
            _values = new Dictionary<string, Value>();
        }

        public static TablaSimbolos Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TablaSimbolos();
                }
                return _instance;
            }
        }

        public void DeclareVariable(string name, TiposBases type)
        {
            _variables.Add(name, type);
            _values.Add(name, type.GetDefaultValue());  
        }
        public void SetVariableValue(string name, Value value)
        {
            _values[name] = value;
        }

        public TiposBases GetVariable(string name)
        {
            if (_variables.Count == 0)
                return null;
            return _variables[name];
        }
        public Value GetVariableValue(string name)
        {
            if (_values.Count == 0)
                return null;
            return _values[name];
        }
        public bool VariableExist(string name)
        {
            if (_variables.Count == 0)
                return false;
            return _variables.ContainsKey(name);
        }

    }
}
