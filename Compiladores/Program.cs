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
            string codigo= "";
            System.IO.StreamReader file = new System.IO.StreamReader("C:/Users/Dell/Documents/GitHub/Compiladores1/Compiladores/test.txt");
            codigo = file.ReadToEnd().ToString();
            
           var lexico = new Lexico(codigo);
            var TokenActual = lexico.ObtenerSiguienteToken();
           while (TokenActual.Tipo!= TokenTipos.EndOfFile)
           {
             Console.WriteLine(TokenActual.ToString());
             TokenActual = lexico.ObtenerSiguienteToken();
           }
            
            Console.ReadKey();
        }
    }
}