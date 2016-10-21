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
        private int _FilaActual;
        private int _cursor;
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
            this._FilaActual = 1;
            this._cursor = 0;
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
            _simbolos.Add("#", TokenTipos.Directiva);
            _simbolos.Add(".", TokenTipos.Punto);
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
            _palabrasReservadas.Add("auto",TokenTipos.PalabraReservadaAuto);
            _palabrasReservadas.Add("break", TokenTipos.PalabraReservadaBreak);
            _palabrasReservadas.Add("case", TokenTipos.PalabraReservadaCase);
            _palabrasReservadas.Add("char", TokenTipos.PalabraReservadaChar);
            _palabrasReservadas.Add("const", TokenTipos.PalabraReservadaConst);
            _palabrasReservadas.Add("continue", TokenTipos.PalabraReservadaContinue);
            _palabrasReservadas.Add("default", TokenTipos.PalabraReservadaDefault);
            _palabrasReservadas.Add("do", TokenTipos.PalabraReservadaDo);
            _palabrasReservadas.Add("double", TokenTipos.PalabraReservadaDouble);
            _palabrasReservadas.Add("else", TokenTipos.PalabraReservadaElse);
            _palabrasReservadas.Add("enum", TokenTipos.PalabraReservadaEnum);
            _palabrasReservadas.Add("extern", TokenTipos.PalabraReservadaExtern);
            _palabrasReservadas.Add("float", TokenTipos.PalabraReservadaFloat);
            _palabrasReservadas.Add("for", TokenTipos.PalabraReservadaFor);
            _palabrasReservadas.Add("goto", TokenTipos.PalabraReservadaGoto);
            _palabrasReservadas.Add("if", TokenTipos.PalabraReservadaIf);
            _palabrasReservadas.Add("int", TokenTipos.PalabraReservadaInt);
            _palabrasReservadas.Add("long", TokenTipos.PalabraReservadaLong);
            _palabrasReservadas.Add("register", TokenTipos.PalabraReservadaRegister);
            _palabrasReservadas.Add("short", TokenTipos.PalabraReservadaShort);
            _palabrasReservadas.Add("signed", TokenTipos.PalabraReservadaSigned);
            _palabrasReservadas.Add("sizeof", TokenTipos.PalabraReservadaSizeof);
            _palabrasReservadas.Add("static", TokenTipos.PalabraReservadaStatic);
            _palabrasReservadas.Add("struct", TokenTipos.PalabraReservadaStruct);
            _palabrasReservadas.Add("switch", TokenTipos.PalabraReservadaSwitch);
            _palabrasReservadas.Add("typedef", TokenTipos.PalabraReservadaTypedef);
            _palabrasReservadas.Add("unsigned", TokenTipos.PalabraReservadaUnsigned);
            _palabrasReservadas.Add("void", TokenTipos.PalabraReservadaVoid);
            _palabrasReservadas.Add("volatile", TokenTipos.PalabraReservadaVolatile);
            _palabrasReservadas.Add("while", TokenTipos.PalabraReservadaWhile);
    
        }

        public Token ObtenerSiguienteToken()
        {
            char simboloTemporal = ObtenerSimboloActual();
            string lexema = "";
         
            while (char.IsWhiteSpace(simboloTemporal) )
            {
                _cursor++;
                if (simboloTemporal == '\n')
                {
                    _FilaActual++;
                    _columnaActual = 0;
                }
                simboloTemporal = ObtenerSimboloActual();
            }
           
            if (simboloTemporal == '\0')
            {
                return new Token { Tipo = TokenTipos.EndOfFile };
            }
           
            if (char.IsLetter(simboloTemporal))
            {
                lexema += simboloTemporal;
                _cursor++;
                return ObtenerIdentificador(lexema);
            }
            if (char.IsDigit(simboloTemporal))
            {
                lexema += simboloTemporal;
                _cursor++;
                return ObtenerDigito(lexema);
            }
            if (_simbolosArimetricos.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _cursor++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeAutoOperaciones = obtenerTokenDeDiccionario(_simbolosAutoOperaciones, lexema + simboloTemporal);
                Token TokenDeIncrementoODecremento = obtenerTokenDeDiccionario(_simbolosAumentarODisminuir, lexema + simboloTemporal);
                if (TokenDeAutoOperaciones != null )
                     return TokenDeAutoOperaciones;
                if (TokenDeIncrementoODecremento != null)
                    return TokenDeIncrementoODecremento;
                if (lexema[0] == '/' && simboloTemporal == '/')
                    return new Token { Tipo = TokenTipos.Comentario, Lexema = ObtenerTexto('\r'), Columna = _columnaActual, Fila = _FilaActual };
                if (lexema[0] == '/' && simboloTemporal == '*')
                {
                    var texto = ObtenerTexto('*');
                    _columnaActual += 2;
                    if (_codigoFuente[_cursor++] == '*' && _codigoFuente[_cursor++] == '/')
                    {
                        if (_codigoFuente[_cursor + 1] == '\n') { _FilaActual++; _columnaActual = 0;  }
                             
                        return new Token { Tipo = TokenTipos.ComentarioBloque, Lexema = texto, Columna = _columnaActual, Fila = _FilaActual };
                    }
                }
                return new Token { Tipo = _simbolosArimetricos[lexema], Lexema = lexema, Columna = _columnaActual, Fila = _FilaActual };
            }
            if (_simbolosRelacionales.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _cursor++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeSimbolosRelacionales = obtenerTokenDeDiccionario(_simbolosRelacionales, lexema + simboloTemporal);
                if (TokenDeSimbolosRelacionales != null)
                    return TokenDeSimbolosRelacionales;
                return new Token { Tipo = _simbolosRelacionales[lexema], Lexema = lexema,Columna = _columnaActual, Fila = _FilaActual };
            }
           
            if (_simbolos.ContainsKey(simboloTemporal.ToString()))
            {
                lexema += simboloTemporal;
                _cursor++;
                simboloTemporal = ObtenerSimboloActual();
                Token TokenDeSimbolosRelacionales = obtenerTokenDeDiccionario(_simbolosRelacionales, lexema + simboloTemporal);
                Token TokenDeSimbolosLogicos = obtenerTokenDeDiccionario(_simbolosLogicos, lexema + simboloTemporal);
                if (TokenDeSimbolosRelacionales != null)
                    return TokenDeSimbolosRelacionales;
                if (TokenDeSimbolosLogicos != null)
                    return TokenDeSimbolosLogicos;
                return new Token { Tipo = _simbolos[lexema], Lexema = lexema, Columna = _columnaActual, Fila = _FilaActual };
            }
            if (simboloTemporal == '"')
            {
                lexema += ObtenerTexto('"');
                _cursor++;
                return new Token { Tipo = TokenTipos.LiteralString, Lexema = lexema, Columna = _columnaActual, Fila = _FilaActual };
            }
           
            throw new LexicoException("Símbolo inesperado encontrado");
        }

        private Token ObtenerDigito(string lexema)
        {
             var simboloTemporal = ObtenerSimboloActual();
            while(char.IsDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _cursor++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token { Tipo = TokenTipos.Numero, Lexema = lexema, Columna = _columnaActual, Fila = _FilaActual };
        }

        private Token ObtenerIdentificador(string lexema)
        {
            var simboloTemporal = ObtenerSimboloActual();
            while(char.IsLetterOrDigit(simboloTemporal)){
                lexema += simboloTemporal;
                _cursor++;
                simboloTemporal = ObtenerSimboloActual();
            }
            return new Token {Tipo = _palabrasReservadas.ContainsKey(lexema)?_palabrasReservadas[lexema]:TokenTipos.Identificador
                 , Lexema = lexema, Columna = _columnaActual, Fila = _FilaActual };
        }

        private char ObtenerSimboloActual()
        {
            if (_cursor < _codigoFuente.Length)
            {
                _columnaActual++;
                return _codigoFuente[_cursor];
            }
            return '\0';
        }
        private Token obtenerTokenDeDiccionario(Dictionary<string, TokenTipos> _Diccionario,string _simbolo)
        {
            if (_Diccionario.ContainsKey(_simbolo))
            {
                _cursor++;
                return new Token { Tipo = _Diccionario[_simbolo], Lexema = _simbolo, Columna = _columnaActual, Fila = _FilaActual };
            }
            return null;
        }
        private string ObtenerTexto(char Delimitador)
        {
            string _texto = "";
            _cursor++;
           var simboloTemporal = ObtenerSimboloActual();
            while (simboloTemporal != Delimitador)
            {
                _texto += simboloTemporal;
                _cursor++;
                if(simboloTemporal == '\n'){
                    _FilaActual++;
                    _columnaActual = 0;
                }
                simboloTemporal = ObtenerSimboloActual();
            }

            return _texto;
        }
    }
}
