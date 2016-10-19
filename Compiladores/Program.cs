﻿using System;
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
            var lexico = new Lexico("cont cont1 12 print ");
            var TokenActual = lexico.ObtenerSiguienteToken();
            while (TokenActual.Tipo != TokenTipos.EndOfFile)
            {
                Console.WriteLine(TokenActual.ToString());
                TokenActual = lexico.ObtenerSiguienteToken();
            }
            Console.ReadKey();
        }
    }
}
