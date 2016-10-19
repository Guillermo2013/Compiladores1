using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiladores
{
    class Lexico
    {
        private string _codigoFuente;
        private int _apuntadorActual;
        private Dictionary<string, TokenTipos> _palabrasReservadas;

        public Lexico(string _codigoFuente)
        {
            // TODO: Complete member initialization
            this._codigoFuente = _codigoFuente;
            this._apuntadorActual = 0;
            this._palabrasReservadas = new Dictionary<string,TokenTipos>();
            _palabrasReservadas.Add("print",TokenTipos.PalabraReservadaPrint);
        }

        public Token ObtenerSiguienteToken()
        {
            char simboloTemporal = ObtenerSimboloActual();
            string lexema = "";
            while (char.IsWhiteSpace(simboloTemporal))
            {
                _apuntadorActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            if (simboloTemporal == '\0')
            {
                return new Token { Tipo = TokenTipos.EndOfFile };
            }
            if (char.IsLetter(simboloTemporal))
            {
                lexema += simboloTemporal;
                _apuntadorActual++;
                return ObtenerIdentificador(lexema);
            }
            if (char.IsDigit(simboloTemporal))
            {
                lexema += simboloTemporal;
                _apuntadorActual++;
                return ObtenerDigito(lexema);
            }
            throw new LexicoException("Símbolo inesperado encontrado");
        }

        private Token ObtenerDigito(string lexema)
        {
             var simboloTemporal = ObtenerSimboloActual();
            while(char.IsDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _apuntadorActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token { Tipo = TokenTipos.Numero, Lexema = lexema };
        }

        private Token ObtenerIdentificador(string lexema)
        {
            var simboloTemporal = ObtenerSimboloActual();
            while(char.IsLetterOrDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _apuntadorActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token {Tipo = _palabrasReservadas.ContainsKey(lexema)?_palabrasReservadas[lexema]:TokenTipos.Identificador
                 , Lexema = lexema};
        }

        private char ObtenerSimboloActual()
        {
            if (_apuntadorActual < _codigoFuente.Length)
            {
                return _codigoFuente[_apuntadorActual];
            }
            return '\0';
        }
    }
}
