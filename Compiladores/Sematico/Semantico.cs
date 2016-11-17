using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Sematico
{
    class Semantico
    {
            private Lexico lexico;
            private Token _TokenActual;
            private Dictionary<string, int> _variables;
            public Semantico(Lexico Lexico)
            {
                lexico = Lexico;
             
                ObtenerSiguienteTokenC();
                _variables = new Dictionary<string, int>();
            }

            private void ObtenerSiguienteTokenC()
            {
                _TokenActual = lexico.ObtenerSiguienteToken();
                if (_TokenActual.Tipo == TokenTipos.HtmlToken|| _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }
            public void Parser()
            {
                CCode();
                if (_TokenActual.Tipo != TokenTipos.EndOfFile)
                    throw new Exception("Se esperaba Eof");

            }
            void CCode()
            {
                ListOfSentences();
            }
            private void ListOfSpecialSentences()
            {
                if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf || _TokenActual.Tipo == TokenTipos.PalabraReservadaWhile || _TokenActual.Tipo == TokenTipos.PalabraReservadaDo ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaFor || _TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch || _TokenActual.Tipo == TokenTipos.Identificador ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaStruct || _TokenActual.Tipo == TokenTipos.PalabraReservadaConst || _TokenActual.Tipo == TokenTipos.PalabraReservadaBreak ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaContinue || _TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaEnum || _TokenActual.Tipo == TokenTipos.Directiva || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaReturn || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento ||_TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                    || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong
                    )
                {
                    SpecialSentence();
                    ListOfSpecialSentences();
                }
                else
                {

                }
            }

            private void SpecialSentence()
            {
                
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    SPECIALDECLARATION();
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
                else if (_TokenActual.Tipo == TokenTipos.Identificador
                    ||_TokenActual.Tipo==TokenTipos.OperacionMultiplicacion
                    ||_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                    ||_TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                {
                    AssignmentOrFunctionCall();
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
                else if (_TokenActual.Tipo == TokenTipos.Directiva)
                {
                    INCLUDE();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
                {
                    ENUM();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaReturn)
                {
                    Return();
                }
               
            }

            private void Return()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaReturn)
                {
                    ObtenerSiguienteTokenC();
                    if(_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                        EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba ;");

                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void SPECIALDECLARATION()
            {
               if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                   || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    GeneralDeclaration();
                    TypeOfDeclarationForFunction();
                }
            
            }

            private void TypeOfDeclarationForFunction()
            {
                
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    IsArrayDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {
                    ObtenerSiguienteTokenC();
                }
                else
                {
                    ValueForId();
                    MultiDeclaration();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("Se esperaba un ;");
                    }
                    ObtenerSiguienteTokenC();
                }
            }
            private void ListOfSentences()
            {
                if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                //ListOfSentences ->Sentence ListOfSentences
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf || _TokenActual.Tipo == TokenTipos.PalabraReservadaWhile || _TokenActual.Tipo == TokenTipos.PalabraReservadaDo ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaFor || _TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch || _TokenActual.Tipo == TokenTipos.Identificador ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaStruct || _TokenActual.Tipo == TokenTipos.PalabraReservadaConst || _TokenActual.Tipo == TokenTipos.PalabraReservadaBreak ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaContinue || _TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaEnum || _TokenActual.Tipo == TokenTipos.Directiva || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaVoid || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                    || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong 
                    ||  _TokenActual.Tipo == TokenTipos.PalabraReservadaExtern
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
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate|| _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
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
                else if (_TokenActual.Tipo == TokenTipos.Identificador
                    || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                {
                    AssignmentOrFunctionCall();
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
                else if (_TokenActual.Tipo == TokenTipos.Directiva)
                {
                    INCLUDE();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
                {
                    ENUM();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaVoid)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificador");
                    }
                    ObtenerSiguienteTokenC();
                    IsFunctionDeclaration();
               }
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaExtern)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.LiteralString)
                    {
                        ObtenerSiguienteTokenC();
                    }
                    if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                    {
                        ObtenerSiguienteTokenC();
                        ListOfSentences();

                        if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                        {
                            throw new SintanticoException("se esperaba un } ");
                        }
                        ObtenerSiguienteTokenC();
                    }
                    else {
                        DECLARATION();
                    }
                    
                }
            }

            private void CONTINUE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba ;");

                    }
                }

            }

            private void ENUM()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificador");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("se esperaba un {");
                    }
                    ObtenerSiguienteTokenC();
                    EnumeratorList();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("se esperaba un }");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba un ;");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void EnumeratorList()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    EnumItem();
                    OptionalEnumItem();
                }
                else
                {

                }
            }

            private void OptionalEnumItem()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                     ObtenerSiguienteTokenC();
                     EnumeratorList();
                }
                else
                {

                }
            }

            private void EnumItem()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    ObtenerSiguienteTokenC();
                    OptionalIndexPosition();
                }
            }

            private void OptionalIndexPosition()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Numero && _TokenActual.Tipo != TokenTipos.NumeroHexagecimal && _TokenActual.Tipo != TokenTipos.NumeroOctal && _TokenActual.Tipo != TokenTipos.NumeroFloat)
                    {
                        throw new SintanticoException("se esperaba un numero");
                    }
                    ObtenerSiguienteTokenC();
                }
                else
                {

                }
            }

            private void INCLUDE()
            {
                if (_TokenActual.Tipo == TokenTipos.Directiva)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInclude)
                    {
                        throw new SintanticoException("se esperaba un include");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.LiteralString)
                    {
                        throw new SintanticoException("se esperaba un include");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void CONST()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInt && _TokenActual.Tipo != TokenTipos.PalabraReservadaFloat &&
                   _TokenActual.Tipo != TokenTipos.PalabraReservadaChar && _TokenActual.Tipo != TokenTipos.PalabraReservadaBool && _TokenActual.Tipo != TokenTipos.PalabraReservadaString &&
                   _TokenActual.Tipo != TokenTipos.PalabraReservadaDate&& _TokenActual.Tipo != TokenTipos.PalabraReservadaLong)
                    {
                        throw new SintanticoException("se esperaba un tipo de dato");
                    }
                    ObtenerSiguienteTokenC();
                   if (_TokenActual.Tipo != TokenTipos.Identificador){
                       throw new SintanticoException("se esperaba un identificador");
                   }
                   ObtenerSiguienteTokenC();
                   if (_TokenActual.Tipo != TokenTipos.Asignacion)
                   {
                       throw new SintanticoException("se esperaba un =");
                   }
                   ObtenerSiguienteTokenC();
                   EXPRESSION();
                   if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                   {
                       throw new SintanticoException("se esperaba un ;");
                   }
                   ObtenerSiguienteTokenC();
                }
            }

            private void STRUCT()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaStruct)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificado ");
                    }
                    ObtenerSiguienteTokenC();
                    StructDeclarationOrInitialization();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba un ; ");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void StructDeclarationOrInitialization()
            {
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    MembersList();
                    if  (_TokenActual.Tipo != TokenTipos.CorcheteDerecho){
                        throw new SintanticoException("se esperaba un } ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.Identificador)
                    {
                        ObtenerSiguienteTokenC();
                        OptionalInitOfStruct();
                        MultiDeclarationStructs();
                    }
                }
                else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion||_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificador ");
                    }
                    ObtenerSiguienteTokenC();
                    OptionalInitOfStruct();
                    MultiDeclarationStructs();
                }
               
            }

            private void MultiDeclarationStructs()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    ObtenerSiguienteTokenC();
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificador ");
                    }
                    ObtenerSiguienteTokenC();
                    OptionalInitOfStruct();
                    MultiDeclarationStructs();
                }
                else
                {

                }
            }

            private void OptionalInitOfStruct()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ValueForId();
                }
            }

           

            private void OptionalStruct()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    ObtenerSiguienteTokenC();
                }
            }

            private void MembersList()
            {
                DeclarationOfStruct();
            }

            private void DeclarationOfStruct()
            {
                GeneralDeclaration();
                ArrayIdentifier();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                OptionalMember();
            }

            private void OptionalMember()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    DeclarationOfStruct();
                }
                else
                {

                }
            }

            private void ArrayIdentifier()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    SizeForArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("se espera ]");
                    }
                    ObtenerSiguienteTokenC();
                }else { 
                }
            }

            private void AssignmentOrFunctionCall()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    ObtenerSiguienteTokenC();
                    IsArray();
                    ArrowOrDot();
                         ValueForPreId();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se espera ;");
                    }
                    ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
                {
                    ObtenerSiguienteTokenC();
                    if(_TokenActual.Tipo != TokenTipos.Identificador )
                        throw new SintanticoException("se espera identificador");
                    ObtenerSiguienteTokenC();
                    IsArray();
                    ArrowOrDot();
                        ValueForPointerId();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se espera ;");
                    }
                    ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo == TokenTipos.AutoOperacionDecremento || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento)
                {
                    ObtenerSiguienteTokenC();
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                        throw new SintanticoException("se espera identificador");
                    ObtenerSiguienteTokenC();
                    IsArray();
                    ArrowOrDot();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se espera ;");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void ValueForPointerId()
            {
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                {
                       ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo == TokenTipos.Asignacion
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionSuma
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionResta
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionMultiplicacion
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionDivision
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionResiduo
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionOlogico
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionXor
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionYlogico
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda

                    )
                {
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                }
                else 
                {
                   
                }
            }

            private void ArrowOrDot()
            {
                if (_TokenActual.Tipo == TokenTipos.Punto || _TokenActual.Tipo == TokenTipos.LogicaStruct)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se espera identificador");
                    }
                    ObtenerSiguienteTokenC();
                    IsArray();   
                }
                else
                {

                }
            }

            private void ValueForPreId()
            {
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                {
                    ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo == TokenTipos.Asignacion
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionSuma
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionResta
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionMultiplicacion
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionDivision
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionResiduo
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionOlogico
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionXor
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionYlogico
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha
                    || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda
                   
                    )
                {
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                }
                else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    CallFunction();
                }
            }

            private void FORLOOP()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaFor)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba un ( ");
                    }
                    ObtenerSiguienteTokenC();
                    ForOrForEach();
                }
            }

            private void ForOrForEach()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("es esperaba un identificador ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba un :");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("es esperaba un identificador ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba un ) ");
                    }
                    ObtenerSiguienteTokenC();
                    BlOCKFORLOOP();
                }
                else
                {
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    { 
                         throw new SintanticoException("es esperaba un ; ");
                    }
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                     if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    { 
                         throw new SintanticoException("es esperaba un ; ");
                    }
                    ObtenerSiguienteTokenC();
                    
                    EXPRESSION();
                     if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    { 
                         throw new SintanticoException("es esperaba un ) ");
                    }
                    ObtenerSiguienteTokenC();
                    BlOCKFORLOOP();
                    
                    
                }
            }

            private void DO()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
                {
                    ObtenerSiguienteTokenC();
                    BlOCKFORLOOP();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaWhile)
                    {
                        throw new SintanticoException("es esperaba la palabra while ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("es esperaba ; ");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void BlOCKFORLOOP()
            {
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                     ObtenerSiguienteTokenC();
                     ListOfSentences();
                        if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                        {
                                throw new SintanticoException("es esperaba } ");
                       }
                        ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    
                    Sentence();
                }
               else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                    {
                        ObtenerSiguienteTokenC();
                    }
                    
            }

            private void SWITCH()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ) ");
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("es esperaba { ");
                    }
                    ObtenerSiguienteTokenC();
                    ListOfCase();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("es esperaba } ");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void ListOfCase()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaCase)
                {
                    CASE();
                    ListOfCase();
                }
                else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDefault)
                {
                    DefaultCase();
                }
                else
                {

                }
            }

            private void DefaultCase()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDefault)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba : ");
                    }
                    ObtenerSiguienteTokenC();
                    ListOfSpecialSentences();
                    

                }
            }

            private void CASE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaCase)
                {
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba : ");
                    }
                    ObtenerSiguienteTokenC();
                    ListOfSpecialSentences();
                    BREAK();
                    
                }
            }

            

            private void BREAK()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("es esperaba ; ");
                    }
                    ObtenerSiguienteTokenC();
                }
                else
                {

                }
            }

            private void WHILE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
                {
                     ObtenerSiguienteTokenC();
            
                     if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                     {
                         throw new SintanticoException("es esperaba ( ");
                     }
                     ObtenerSiguienteTokenC();
                     EXPRESSION();
                     if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                     {
                         throw new SintanticoException("es esperaba ) ");
                     }
                     ObtenerSiguienteTokenC();
                     BlOCKFORLOOP();
                }
            }

            private void IF()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ) ");
                    }
                    ObtenerSiguienteTokenC();
                    BlOCKFORIF();
                    ELSE();
                  }
            }

            private void ELSE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaElse)
                {
                    ObtenerSiguienteTokenC();
                    BlOCKFORIF();
                }
                else { }
            }

            private void BlOCKFORIF()
            {
                
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    ListOfSentences();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("es esperaba } ");
                    }
                    ObtenerSiguienteTokenC();
                }
                else if(_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    Sentence();
                }
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {
                    ObtenerSiguienteTokenC();
                }
            }

            private void DECLARATION()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    GeneralDeclaration();
                    TypeOfDeclaration();
                }
            }
            private void GeneralDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                      _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                      _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
                {
                    ObtenerSiguienteTokenC();
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba un identificador");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void IsPointer()
            {
                if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
                {
                    ObtenerSiguienteTokenC();
                    IsPointer();
                }
                else
                {
                    
                }
            }
            private void TypeOfDeclaration()
            {
                
                 if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    IsArrayDeclaration();
                    MultiDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    IsFunctionDeclaration();
                }
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {
                    ObtenerSiguienteTokenC();
                }
                else
                {
                   ValueForId();
                    MultiDeclaration();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                            throw new SintanticoException("Se esperaba un ;");
                    }
                    ObtenerSiguienteTokenC();
                }
            }
            private void ValueForId()
            {
                //ValueForId-> = EXPRESSION
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                    {
                        ObtenerSiguienteTokenC();
                        ListOfExpressions();
                        if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                        {
                            throw new SintanticoException("Se esperaba un }");
                        }
                        ObtenerSiguienteTokenC();
                    }
                    else {
                        EXPRESSION();
                    }
                    
                   
                }
                else//ValueForId-> epsilon
                {

                }
            }

            private void MultiDeclaration()
            {
                
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    OptionalId();
                }
                else
                {

                }
            }
            private void OptionalId()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    ObtenerSiguienteTokenC();
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
                ObtenerSiguienteTokenC();
                
                 BidArray();                
                OtherIdOrValue();
            }
            private void OtherIdOrValue()
            {
                   ValueForId();
                    OptionalId();
                
            }
            private void IsArrayDeclaration()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    SizeForArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba  ]");
                    }
                    ObtenerSiguienteTokenC();
                    BidArray();
                    OptionalInitOfArray();
                    MultiDeclaration();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("Se esperaba  ;");
                    }
                    ObtenerSiguienteTokenC();
                }
            }
            private void SizeForArray()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.NumeroFloat
                    || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal || _TokenActual.Tipo == TokenTipos.NumeroOctal)
                {
                    ObtenerSiguienteTokenC();
                }
                else
                {

                }
            }
            private void BidArray()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba un factor");
                    }
                    
                    EXPRESSION();
                    
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba  ]");
                    }
                    ObtenerSiguienteTokenC();
                }
                else
                {

                }
            }
            
            private void OptionalInitOfArray()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("Se esperaba  un corchete");
                    }
                    ObtenerSiguienteTokenC();
                    ListOfExpressions();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("Se esperaba  un corchete");
                    }
                    ObtenerSiguienteTokenC();
                }
            }

            private void ListOfExpressions()
            {
                EXPRESSION();
                OptionalListOfExpressions();
            }

            private void OptionalListOfExpressions()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    ObtenerSiguienteTokenC();
                    ListOfExpressions();
                }
                else
                {

                }
            }
            private void OptionalExpression()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    ObtenerSiguienteTokenC();
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
                    ObtenerSiguienteTokenC();
                    ParameterList();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("Se esperaba  un parentesis");
                    }
                    ObtenerSiguienteTokenC();
                    OptinalDeclaretionFuntion();
                }
            }

            private void OptinalDeclaretionFuntion()
            {
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    ListOfSpecialSentences();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("Se esperaba  un parentesis");
                    }
                    ObtenerSiguienteTokenC();
                }
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {

                    ObtenerSiguienteTokenC();
                }
               
            }
            private void ParameterList()
            {
               if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt 
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat 
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaChar 
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool 
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaString
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaVoid
)
                {
                    ObtenerSiguienteTokenC();
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
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba  un identificador");
                    }
                    ObtenerSiguienteTokenC();
                    IsArray();
                }
                else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
                {
                    IsPointer();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba  un identificador");
                    }
                    ObtenerSiguienteTokenC();
                    IsArray();
                   
                }
                else if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    ObtenerSiguienteTokenC();
                     IsArray();
                }
                 
            }

            private void IsArray()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    SizeForArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                            
                    throw new SintanticoException("Se esperaba  un ]");
                
                    }
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.LlaveDerecho)
                    {

                        throw new SintanticoException("Se esperaba  un ]");

                    }
                    BidArray();
                }
            }

            private void OptionaListOfParams()
            {
                if (_TokenActual.Tipo == TokenTipos.Separador)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt 
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat 
                        ||_TokenActual.Tipo == TokenTipos.PalabraReservadaChar 
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool 
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaString 
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaVoid)
                    {
                        ObtenerSiguienteTokenC();
                        CHOOSE_ID_TYPE();
                        OptionaListOfParams();
                    }
                    else
                    {
                      
                    }
                }
            }
            private void EXPRESSION()
            {
                RelationalExpression();
            }

            private void RelationalExpression()
            {
                ExpressionAdicion();
                RelationalExpressionPrima();
            }

            private void RelationalExpressionPrima()
            {//falta <<=
                if (_TokenActual.Tipo == TokenTipos.RelacionalMenor
                   || _TokenActual.Tipo == TokenTipos.RelacionalMenorOIgual
                   || _TokenActual.Tipo == TokenTipos.RelacionalMayor
                   || _TokenActual.Tipo == TokenTipos.RelacionalMayorOIgual
                   || _TokenActual.Tipo == TokenTipos.LogicosO
                   || _TokenActual.Tipo == TokenTipos.LogicosY
                   || _TokenActual.Tipo == TokenTipos.YPorBit
                   || _TokenActual.Tipo == TokenTipos.OPorBit
                   || _TokenActual.Tipo == TokenTipos.CorrimientoDerecha
                   || _TokenActual.Tipo == TokenTipos.CorrimientoIzquierda 
                   || _TokenActual.Tipo == TokenTipos.RelacionalIgual
                   || _TokenActual.Tipo == TokenTipos.RelacionalNoIgual
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionSuma
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionResta
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionMultiplicacion
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionDivision
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionResiduo
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionYlogico
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionXor
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionOlogico
                   || _TokenActual.Tipo == TokenTipos.Negacion
                   || _TokenActual.Tipo == TokenTipos.Asignacion
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha
                   || _TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda
                   || _TokenActual.Tipo == TokenTipos.OExclusivoPorBit
                    ){
                    ObtenerSiguienteTokenC();
                     ExpressionAdicion();
                    RelationalExpressionPrima();
                  
                }
               else
               {
                  
               }
            }
            private void ExpressionAdicion()
            {
                ExpressionMul();
                ExpressionAdicionPrima();
            }

            private void ExpressionAdicionPrima()
            {
                if (_TokenActual.Tipo == TokenTipos.OperacionSuma 
                    || _TokenActual.Tipo == TokenTipos.OperacionResta)
                {
                    ObtenerSiguienteTokenC();
                    ExpressionMul();
                    ExpressionAdicionPrima();
                
                }
                else
                {

                }
            }
            private void ExpressionMul()
            {
                ExpressionUnary();
                ExpressionMulPrima();
            }

            private void ExpressionMulPrima()
            {
                if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
                    ||_TokenActual.Tipo == TokenTipos.OperacionDivision
                    ||_TokenActual.Tipo == TokenTipos.OperacionDivisionResiduo)
                {
                    ObtenerSiguienteTokenC();
                    ExpressionUnary();
                    ExpressionMulPrima();
                   
                }
                else
                {
                    
                }
            }

            private void ExpressionUnary()
            {
                if(_TokenActual.Tipo == TokenTipos.NegacionPorBit
                    ||_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                    ||_TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                    || _TokenActual.Tipo == TokenTipos.YPorBit
                    ||_TokenActual.Tipo == TokenTipos.OPorBit
                   )
               {
                UnaryOperators();
                Factor();
               }
                else if (_TokenActual.Tipo == TokenTipos.Identificador 
                    || _TokenActual.Tipo == TokenTipos.Numero
                    || _TokenActual.Tipo == TokenTipos.NumeroOctal
                    || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal
                    || _TokenActual.Tipo == TokenTipos.NumeroFloat
                    || _TokenActual.Tipo == TokenTipos.LiteralString
                    || _TokenActual.Tipo ==TokenTipos.PalabraReservadaFalse
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaTrue 
                    || _TokenActual.Tipo == TokenTipos.LiteralDate
                    || _TokenActual.Tipo == TokenTipos.LiteralChar
                    || _TokenActual.Tipo == TokenTipos.ParentesisIzquierdo
                    )
                {
                    Factor();
                }
                
                
            }
            private void UnaryOperators()
            {
                if (_TokenActual.Tipo == TokenTipos.NegacionPorBit
                     || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                     || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                     || _TokenActual.Tipo == TokenTipos.YPorBit
                     || _TokenActual.Tipo == TokenTipos.OPorBit
                    )
               {
                   ObtenerSiguienteTokenC();
               }               
            }
            private void Factor()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    ObtenerSiguienteTokenC();
                    OptionalIncrementOrDecrement();
                    FactorFunctionOrArray();
                       
                    
                }
                else if (_TokenActual.Tipo == TokenTipos.Numero
                        || _TokenActual.Tipo == TokenTipos.NumeroOctal
                        || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal
                        || _TokenActual.Tipo == TokenTipos.NumeroFloat
                        || _TokenActual.Tipo == TokenTipos.LiteralString
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaFalse
                        || _TokenActual.Tipo == TokenTipos.PalabraReservadaTrue
                        || _TokenActual.Tipo == TokenTipos.LiteralDate
                        || _TokenActual.Tipo == TokenTipos.LiteralChar
                        )
                {
                    ObtenerSiguienteTokenC();
                }
                else if(_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo){
                         ObtenerSiguienteTokenC();
                        EXPRESSION();
                        if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                         {
                        throw new SintanticoException("Se esperaba un }");
                        }
                        ObtenerSiguienteTokenC();

                }
                else
                {
                    throw new SintanticoException("Se esperaba un factor");
                }
                
            }

            private void OptionalIncrementOrDecrement()
            {
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                {
                    ObtenerSiguienteTokenC();
                }
                else
                {

                }
            }

            private void FactorFunctionOrArray()
            {
                if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    CallFunction();
                }
                else if(_TokenActual.Tipo==TokenTipos.LlaveIzquierdo
                    || _TokenActual.Tipo == TokenTipos.LogicaStruct
                    ||_TokenActual.Tipo == TokenTipos.Punto){
                    IndexOrArrowAccess();
                }
                else
                {

                }
            }    
            private void CallFunction()
            {
                if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    ListOfExpressions();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("Se esperaba ) ");
                    }
                    ObtenerSiguienteTokenC();
                }
            }
            private void IndexOrArrowAccess()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("Se esperaba ] ");
                    }
                    ObtenerSiguienteTokenC();
                    IndexOrArrowAccess();
                   
                }
                else if (_TokenActual.Tipo == TokenTipos.LogicaStruct || _TokenActual.Tipo == TokenTipos.Punto)
                {
                    ObtenerSiguienteTokenC();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba identificador ");
                    }
                    ObtenerSiguienteTokenC();
                    IndexOrArrowAccess();
                }
                else
                {
                    
                }

            }
   
        }

    }   
