﻿using Compiladores.Semantico.Tipos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Semantico
{
    public class ContenidoStack
    {
        public static ContenidoStack _StackInstance = null;
        public Stack<TablaSimbolos> Stack;


        private ContenidoStack()
        {

            Stack = new Stack<TablaSimbolos>();
            Stack.Push( new TablaSimbolos());
        }

        public static ContenidoStack InstanceStack
        {
            get
            {
                if (_StackInstance == null)
                {
                    _StackInstance = new ContenidoStack();
                }
                return _StackInstance;
            }
        }

    }
}