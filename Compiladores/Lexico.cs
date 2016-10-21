using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiladores
{
    class Lexico
    {
        private string _codigoFuente;
        private int _columnaActual;
        private Dictionary<string, TokenTipos> _palabrasReservadas;
        private Dictionary<string, TokenTipos> _simbolos;
        private Dictionary<string, TokenTipos> _simbolosArimetricos;
        private Dictionary<string, TokenTipos> _simbolosAutoOperaciones;
        private Dictionary<string, TokenTipos> _simbolosAumentarODisminuir;
        private Dictionary<string, TokenTipos> _simbolosRelacionales;
        private Dictionary<string, TokenTipos> _simbolosLogicos;

        public Lexico(string _codigoFuente)
        {
            // TODO: Complete member initialization
            this._codigoFuente = _codigoFuente;
            this._columnaActual = 0;
            this._palabrasReservadas = new Dictionary<string,TokenTipos>();
            this._simbolos = new Dictionary<string, TokenTipos>();
            this._simbolosArimetricos = new Dictionary<string, TokenTipos>();
            this._simbolosAutoOperaciones = new Dictionary<string, TokenTipos>();
            this._simbolosAumentarODisminuir = new Dictionary<string, TokenTipos>();
            this._simbolosRelacionales = new Dictionary<string, TokenTipos>();
            this._simbolosLogicos = new Dictionary<string, TokenTipos>();
            _simbolos.Add("=", TokenTipos.Asignacion);
            _simbolos.Add(";", TokenTipos.FinalDeSentencia);
            _simbolos.Add(",", TokenTipos.Separador);
            _simbolos.Add("!", TokenTipos.Negacion);
            _simbolos.Add("&", TokenTipos.YPorBit);
            _simbolos.Add("|", TokenTipos.OPorBit);
            _simbolos.Add("~", TokenTipos.NegacionPorBit);
            _simbolos.Add("^", TokenTipos.OExclusivoPorBit);
            _simbolos.Add("(", TokenTipos.ParentesisIzquierdo);
            _simbolos.Add(")", TokenTipos.ParentesisDerecho);
            _simbolos.Add("{", TokenTipos.CorcheteIzquierdo);
            _simbolos.Add("}", TokenTipos.CorcheteDerecho);
            _simbolos.Add("[", TokenTipos.LlaveIzquierdo);
            _simbolos.Add("]", TokenTipos.LlaveDerecho);
            _simbolosArimetricos.Add("*", TokenTipos.OperacionMultiplicacion);
            _simbolosArimetricos.Add("+", TokenTipos.OperacionSuma);
            _simbolosArimetricos.Add("-", TokenTipos.OperacionResta);
            _simbolosArimetricos.Add("/", TokenTipos.OperacionDivision);
            _simbolosArimetricos.Add("%", TokenTipos.OperacionDivisionResiduo);
            _simbolosAutoOperaciones.Add("+=", TokenTipos.AutoOperacionSuma);
            _simbolosAutoOperaciones.Add("-=", TokenTipos.AutoOperacionResta);
            _simbolosAutoOperaciones.Add("*=", TokenTipos.AutoOperacionMultiplicacion);
            _simbolosAutoOperaciones.Add("/=", TokenTipos.AutoOperacionDivision);
            _simbolosAumentarODisminuir.Add("++", TokenTipos.AutoOperacionIncremento);
            _simbolosAumentarODisminuir.Add("--", TokenTipos.AutoOperacionDecremento);
            _simbolosRelacionales.Add(">", TokenTipos.RelacionalMayor);
            _simbolosRelacionales.Add(">=", TokenTipos.RelacionalMayorOIgual);
            _simbolosRelacionales.Add("<", TokenTipos.RelacionalMenor);
            _simbolosRelacionales.Add("<=", TokenTipos.RelacionalMenorOIgual);
            _simbolosRelacionales.Add("==", TokenTipos.RelacionalIgual);
            _simbolosRelacionales.Add("!=", TokenTipos.RelacionalNoIgual);
            _simbolosLogicos.Add("&&", TokenTipos.LogicosY);
            _simbolosLogicos.Add("||", TokenTipos.LogicosO);


        }

        public Token ObtenerSiguienteToken()
        {
            char simboloTemporal = ObtenerSimboloActual();
            string lexema = "";
            while (char.IsWhiteSpace(simboloTemporal))
            {
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            if (simboloTemporal == '\0')
            {
                return new Token { Tipo = TokenTipos.EndOfFile };
            }
            if (char.IsLetter(simboloTemporal))
            {
                lexema += simboloTemporal;
                _columnaActual++;
                return ObtenerIdentificador(lexema);
            }
            if (char.IsDigit(simboloTemporal))
            {
                lexema += simboloTemporal;
                _columnaActual++;
                return ObtenerDigito(lexema);
            }
            if (_simbolosArimetricos.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeAutoOperaciones = obtenerTokenDeDiccionario(_simbolosAutoOperaciones, lexema + simboloTemporal);
                Token TokenDeIncrementoODecremento = obtenerTokenDeDiccionario(_simbolosAumentarODisminuir, lexema + simboloTemporal);
                if (TokenDeAutoOperaciones != null )
                     return TokenDeAutoOperaciones;
                if (TokenDeIncrementoODecremento != null)
                    return TokenDeIncrementoODecremento; 
                return new Token { Tipo = _simbolosArimetricos[lexema], Lexema = lexema, Columna = _columnaActual };
            }
            if (_simbolosRelacionales.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeSimbolosRelacionales = obtenerTokenDeDiccionario(_simbolosRelacionales, lexema + simboloTemporal);
                if (TokenDeSimbolosRelacionales != null)
                    return TokenDeSimbolosRelacionales;
                return new Token { Tipo = _simbolosRelacionales[lexema], Lexema = lexema };
            }
           
            if (_simbolos.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeSimbolosRelacionales = obtenerTokenDeDiccionario(_simbolosRelacionales, lexema + simboloTemporal);
                Token TokenDeSimbolosLogicos = obtenerTokenDeDiccionario(_simbolosLogicos, lexema + simboloTemporal);
                if (TokenDeSimbolosRelacionales != null)
                    return TokenDeSimbolosRelacionales;
                if (TokenDeSimbolosLogicos != null)
                    return TokenDeSimbolosLogicos;
                return new Token { Tipo = _simbolos[lexema], Lexema = lexema, Columna = _columnaActual };
            }
            if (simboloTemporal == '"')
            {
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
                while (char.IsLetterOrDigit(simboloTemporal) || char.IsWhiteSpace(simboloTemporal) || char.IsSymbol(simboloTemporal) || char.IsPunctuation(simboloTemporal))
                {
                    lexema += simboloTemporal;
                    _columnaActual++;
                    simboloTemporal = ObtenerSimboloActual();
                    if (simboloTemporal == '"')
                    {
                        lexema += simboloTemporal;
                        _columnaActual++;
                        break;
                    }
                }
                return new Token { Tipo = TokenTipos.LiteralString, Lexema = lexema, Columna = _columnaActual };
            }
            throw new LexicoException("Símbolo inesperado encontrado");
        }

        private Token ObtenerDigito(string lexema)
        {
             var simboloTemporal = ObtenerSimboloActual();
            while(char.IsDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token { Tipo = TokenTipos.Numero, Lexema = lexema ,Columna = _columnaActual};
        }

        private Token ObtenerIdentificador(string lexema)
        {
            var simboloTemporal = ObtenerSimboloActual();
            while(char.IsLetterOrDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _columnaActual++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token {Tipo = _palabrasReservadas.ContainsKey(lexema)?_palabrasReservadas[lexema]:TokenTipos.Identificador
                 , Lexema = lexema,Columna = _columnaActual};
        }

        private char ObtenerSimboloActual()
        {
            if (_columnaActual < _codigoFuente.Length)
            {
                return _codigoFuente[_columnaActual];
            }
            return '\0';
        }
        private Token obtenerTokenDeDiccionario(Dictionary<string, TokenTipos> _Diccionario,string _simbolo)
        {
            if (_Diccionario.ContainsKey(_simbolo))
            {
                _columnaActual++;
                return new Token { Tipo = _Diccionario[_simbolo], Lexema = _simbolo, Columna = _columnaActual };
            }
            return null;
        }
    }
}
