using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Sematico
{
    class Semantico
    {
        Lexico lexico;
        Token _TokenActual;
        public class Parser
        {
            private Lexico lexico;
            private Token _TokenActual;
            private Dictionary<string, int> _variables;
            public Parser(Lexico Lexico)
            {
                lexico = Lexico;
                _TokenActual = lexico.ObtenerSiguienteToken();
                _variables = new Dictionary<string, int>();
            }


            public void Parse()
            {
                CCode();
                if (_TokenActual.Tipo != TokenTipos.EndOfFile)
                    throw new Exception("Se esperaba Eof");

            }
            void CCode()
            {
                ListOfSentences();
            }

            private void ListOfSentences()
            {
                //ListOfSentences ->Sentence ListOfSentences
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf || _TokenActual.Tipo == TokenTipos.PalabraReservadaWhile || _TokenActual.Tipo == TokenTipos.PalabraReservadaDo ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaFor || _TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch || _TokenActual.Tipo == TokenTipos.Identificador ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaStruct || _TokenActual.Tipo == TokenTipos.PalabraReservadaConst || _TokenActual.Tipo == TokenTipos.PalabraReservadaBreak ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaContinue || _TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                   )
                {
                    Sentence();
                    ListOfSentences();
                }
                //Lista_Sentencia->Epsilon
                else
                {

                }

            }

            private void Sentence()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    DECLARATION();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
                {
                    IF();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
                {
                    WHILE();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
                {
                    DO();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaFor)
                {
                    FORLOOP();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
                {
                    SWITCH();
                }
                else if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    PREID();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaStruct)
                {
                    STRUCT();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
                {
                    CONST();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
                {
                    BREAK();

                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
                {
                    CONTINUE();
                }
                else
                {
                    throw new SintanticoException("expresion no soportada");
                }
            }

            private void DECLARATION()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    GeneralDeclaration();
                    // _TokenActual = lexico.ObtenerSiguienteToken();
                    TypeOfDeclaration();
                }
            }



            private void GeneralDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                      _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                      _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba un identificador");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void IsPointer()
            {
                if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    IsPointer();
                }
                else
                {

                }
            }
            private void TypeOfDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ValueForId();
                    MultiDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    IsArrayDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    IsFunctionDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {
                    throw new SintanticoException("Se esperaba un ;");
                }
            }

            private void ValueForId()
            {
                //ValueForId-> = EXPRESSION
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                }
                else//ValueForId-> epsilon
                {

                }
            }

            private void MultiDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    OptionalId();
                }
                else
                {

                }
            }

            private void OptionalId()
            {
                if (_TokenActual.Tipo != TokenTipos.Separador)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfId();
                }
                else
                {

                }
            }

            private void ListOfId()
            {
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba un identificador");
                }
                _TokenActual = lexico.ObtenerSiguienteToken();
                OtherIdOrValue();
            }

            private void OtherIdOrValue()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ValueForId();
                    OptionalId();
                }else{

                }
            }
            private void IsArrayDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    SizeForArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba  ]");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    BidArray();
                    OptionalInitOfArray();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("Se esperaba  ;");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();

                }
            }

            private void SizeForArray()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.NumeroFloat
                    || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal || _TokenActual.Tipo == TokenTipos.NumeroOctal)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {

                }
            }
            private void BidArray()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    SizeBidArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba  ]");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {

                }
            }

            private void SizeBidArray()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.NumeroFloat
                    || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal || _TokenActual.Tipo == TokenTipos.NumeroOctal)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {
                    throw new SintanticoException("Se esperaba  un numero ");
                }

            }
            private void OptionalInitOfArray()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("Se esperaba  un corchete");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfExpressions();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("Se esperaba  un corchete");
                    }
                }
            }

            private void ListOfExpressions()
            {
                EXPRESSION();
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    OptionalExpression();
                }
            }



            private void OptionalExpression()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfExpressions();
                }
                else
                {

                }
            }

            private void IsFunctionDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ParameterList();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("Se esperaba  un parentesis");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfSpecialSentences();
                }
            }

            private void ParameterList()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                       _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                       _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    CHOOSE_ID_TYPE();
                    OptionaListOfParams();
                }
                else
                {

                }
            }

            private void CHOOSE_ID_TYPE()
            {
                if (_TokenActual.Tipo == TokenTipos.YPorBit)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba  un identificador");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
                {
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba  un identificador");
                    }
                }
                else if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba  un identificador");
                }
                _TokenActual = lexico.ObtenerSiguienteToken();
            }

            private void OptionaListOfParams()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                          _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                          _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                    {
                        _TokenActual = lexico.ObtenerSiguienteToken();
                        CHOOSE_ID_TYPE();
                        OptionaListOfParams();
                    }
                    else
                    {
                        throw new SintanticoException("Se esperaba tipo de variable");
                    }
                }
            }
        }
    }   
}