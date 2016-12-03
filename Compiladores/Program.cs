﻿using Compiladores.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            string codigo = "";
            System.IO.StreamReader file = new System.IO.StreamReader("C://Users/Dell/Documents/Visual Studio 2013/Projects/Compiladores/Compiladores/test.c");
            codigo = file.ReadToEnd().ToString();
            Lexico lexico = new Lexico(codigo);
            SintacticoClass parser = new SintacticoClass(lexico);
           
             var Arbol = parser.Parser();
             foreach (var statementNode in Arbol)
             {
                 Console.WriteLine(statementNode);
                 statementNode.ValidSemantic();
             }
            Console.ReadKey();

              
                

        }
    }
}