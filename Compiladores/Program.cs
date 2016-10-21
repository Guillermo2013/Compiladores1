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
            string codigo= @"#include <stdio.h>

                            int main()
                            {
                               int numero;
                                //suma numero multiplos de 3
                                /*que otro rollo yo 


                                */
                               for ( numero = -15 ; numero <= -3 ; numero += 3 )
                               {
                                 printf( "+'"'+"%d"+'"'+@", numero );
                               }

                               return 0;
                            } ";
           
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