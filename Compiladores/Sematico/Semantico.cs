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
                _TokenActual = lexico.ObtenerSiguienteToken();
                _variables = new Dictionary<string, int>();
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
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf || _TokenActual.Tipo == TokenTipos.PalabraReservadaWhile || _TokenActual.Tipo == TokenTipos.PalabraReservadaDo ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaFor || _TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch || _TokenActual.Tipo == TokenTipos.Identificador ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaStruct || _TokenActual.Tipo == TokenTipos.PalabraReservadaConst || _TokenActual.Tipo == TokenTipos.PalabraReservadaBreak ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaContinue || _TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaEnum
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
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
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
                else if (_TokenActual.Tipo == TokenTipos.Identificador)
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
                else
                {
                    throw new SintanticoException("expresion no soportada");
                }
            }

            private void Return()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaReturn)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba ;");

                    }
                }
            }

            private void SPECIALDECLARATION()
            {
               if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    GeneralDeclaration();
                    TypeOfDeclarationForFunction();
                }
            
            }

            private void TypeOfDeclarationForFunction()
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
                else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {
                    throw new SintanticoException("Se esperaba un ;");
                }
            }
            private void ListOfSentences()
            {
                //ListOfSentences ->Sentence ListOfSentences
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf || _TokenActual.Tipo == TokenTipos.PalabraReservadaWhile || _TokenActual.Tipo == TokenTipos.PalabraReservadaDo ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaFor || _TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch || _TokenActual.Tipo == TokenTipos.Identificador ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaStruct || _TokenActual.Tipo == TokenTipos.PalabraReservadaConst || _TokenActual.Tipo == TokenTipos.PalabraReservadaBreak ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaContinue || _TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaEnum 
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
                else
                {
                    throw new SintanticoException("expresion no soportada");
                }
            }

            private void CONTINUE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificador");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("se esperaba un {");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EnumeratorList();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("se esperaba un }");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba un ;");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                     _TokenActual = lexico.ObtenerSiguienteToken();
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    OptionalIndexPosition();
                }
            }

            private void OptionalIndexPosition()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Numero && _TokenActual.Tipo != TokenTipos.NumeroHexagecimal && _TokenActual.Tipo != TokenTipos.NumeroOctal && _TokenActual.Tipo != TokenTipos.NumeroFloat)
                    {
                        throw new SintanticoException("se esperaba un numero");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {

                }
            }

            private void INCLUDE()
            {
                if (_TokenActual.Tipo == TokenTipos.Directiva)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInclude)
                    {
                        throw new SintanticoException("se esperaba un include");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.LiteralString)
                    {
                        throw new SintanticoException("se esperaba un include");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void CONST()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInt && _TokenActual.Tipo != TokenTipos.PalabraReservadaFloat &&
                   _TokenActual.Tipo != TokenTipos.PalabraReservadaChar && _TokenActual.Tipo != TokenTipos.PalabraReservadaBool && _TokenActual.Tipo != TokenTipos.PalabraReservadaString &&
                   _TokenActual.Tipo != TokenTipos.PalabraReservadaDate)
                    {
                        throw new SintanticoException("se esperaba un tipo de dato");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                   if (_TokenActual.Tipo != TokenTipos.Identificador){
                       throw new SintanticoException("se esperaba un identificador");
                   }
                   _TokenActual = lexico.ObtenerSiguienteToken();
                   if (_TokenActual.Tipo != TokenTipos.Asignacion)
                   {
                       throw new SintanticoException("se esperaba un =");
                   }
                   _TokenActual = lexico.ObtenerSiguienteToken();
                   EXPRESSION();
                   if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                   {
                       throw new SintanticoException("se esperaba un =");
                   }
                   _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void STRUCT()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaStruct)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("se esperaba un identificado ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                    {
                        throw new SintanticoException("se esperaba un { ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    MembersList();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("se esperaba un } ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    OptionalStruct();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se esperaba un ; ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void OptionalStruct()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                _TokenActual = lexico.ObtenerSiguienteToken();
                OptionalMember();
            }

            private void OptionalMember()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    SizeForArray();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("se espera ]");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }else { 
                }
            }

            private void AssignmentOrFunctionCall()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                     _TokenActual = lexico.ObtenerSiguienteToken();
                     ValueForPreId();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("se espera ;");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void ValueForPreId()
            {
                if (_TokenActual.Tipo == TokenTipos.Asignacion)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba un ( ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ForOrForEach();
                }
            }

            private void ForOrForEach()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("es esperaba un identificador ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba un :");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.Identificador)
                    {
                        throw new SintanticoException("es esperaba un identificador ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba un ) ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    BlOCKFORLOOP();
                }
                else
                {
                    
                  
                }
            }

            private void DO()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    BlOCKFORLOOP();
                    if (_TokenActual.Tipo != TokenTipos.PalabraReservadaWhile)
                    {
                        throw new SintanticoException("es esperaba la palabra while ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("es esperaba ; ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }

            private void BlOCKFORLOOP()
            {
                if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                {
                    throw new SintanticoException("es esperaba { ");
                }
                _TokenActual = lexico.ObtenerSiguienteToken();
                ListOfSentences();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("es esperaba } ");
                }
                _TokenActual = lexico.ObtenerSiguienteToken();
            }

            private void SWITCH()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ) ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.LlaveIzquierdo)
                    {
                        throw new SintanticoException("es esperaba { ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfCase();
                    if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                    {
                        throw new SintanticoException("es esperaba } ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba : ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfSpecialSentences();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("es esperaba ; ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();

                }
            }

            private void CASE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaCase)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                    {
                        throw new SintanticoException("es esperaba : ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfSpecialSentences();
                    BREAK();
                    
                }
            }

            

            private void BREAK()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                    {
                        throw new SintanticoException("es esperaba ; ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {

                }
            }

            private void WHILE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
                {
                     _TokenActual = lexico.ObtenerSiguienteToken();
            
                     if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                     {
                         throw new SintanticoException("es esperaba ( ");
                     }
                     _TokenActual = lexico.ObtenerSiguienteToken();
                     EXPRESSION();
                     if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                     {
                         throw new SintanticoException("es esperaba ) ");
                     }
                     _TokenActual = lexico.ObtenerSiguienteToken();
                     BlOCKFORLOOP();
                }
            }

            private void IF()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                    {
                        throw new SintanticoException("es esperaba ( ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("es esperaba ) ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    BlOCKFORIF();
                    ELSE();
                  }
            }

            private void ELSE()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaElse)
                {
                    BlOCKFORIF();
                }
                else { }
            }

            private void BlOCKFORIF()
            {
                
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfSentences();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("es esperaba } ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {
                    Sentence();
                }
            }

            private void DECLARATION()
            {
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                    _TokenActual.Tipo == TokenTipos.PalabraReservadaDate)
                {
                    GeneralDeclaration();
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
                   if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                   {
                       throw new SintanticoException("Se esperaba un ;");
                   }
                   _TokenActual = lexico.ObtenerSiguienteToken();
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
            {
               if(_TokenActual.Tipo == TokenTipos.RelacionalMayor||_TokenActual.Tipo == TokenTipos.RelacionalMayorOIgual||_TokenActual.Tipo== TokenTipos.RelacionalMenor||
                   _TokenActual.Tipo== TokenTipos.RelacionalMenorOIgual || _TokenActual.Tipo == TokenTipos.LogicosY||_TokenActual.Tipo== TokenTipos.LogicosO
                ||_TokenActual.Tipo == TokenTipos.RelacionalIgual||_TokenActual.Tipo == TokenTipos.RelacionalNoIgual ){
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                if (_TokenActual.Tipo == TokenTipos.OperacionSuma || _TokenActual.Tipo == TokenTipos.OperacionResta)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
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
                if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion||_TokenActual.Tipo == TokenTipos.OperacionDivision||
                    _TokenActual.Tipo == TokenTipos.OperacionDivisionResiduo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ExpressionUnary();
                    ExpressionMulPrima();
                   
                }
                else
                {
                    
                }
            }

            private void ExpressionUnary()
            {
                if(_TokenActual.Tipo == TokenTipos.NegacionPorBit||_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento||
                   _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento|| _TokenActual.Tipo == TokenTipos.YPorBit||_TokenActual.Tipo == TokenTipos.OPorBit
                   || _TokenActual.Tipo == TokenTipos.OExclusivoPorBit)
               {
                UnaryOperators();
                Factor();
               }
                else if (_TokenActual.Tipo == TokenTipos.Identificador || _TokenActual.Tipo == TokenTipos.Numero
                    || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.LiteralString||_TokenActual.Tipo==TokenTipos.PalabraReservadaFalse
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaTrue || _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.NumeroOctal ||_TokenActual.Tipo == TokenTipos.NumeroHexagecimal
                    || _TokenActual.Tipo == TokenTipos.NumeroFloat || _TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    Factor();
                }
                else
                {
                    throw new SintanticoException("Se esperaba una literal o un operador Unario");
                }
                
            }
            private void UnaryOperators()
            {
               if(_TokenActual.Tipo == TokenTipos.NegacionPorBit||_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento||
                   _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento|| _TokenActual.Tipo == TokenTipos.YPorBit||_TokenActual.Tipo == TokenTipos.OPorBit
                   || _TokenActual.Tipo == TokenTipos.OExclusivoPorBit)
               {
                   _TokenActual = lexico.ObtenerSiguienteToken();
               }               
            }
            private void Factor()
            {
                if (_TokenActual.Tipo == TokenTipos.Identificador)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    FactorFunctionOrArray();
                }
                else if (_TokenActual.Tipo == TokenTipos.Numero
                   || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.LiteralString || _TokenActual.Tipo == TokenTipos.PalabraReservadaFalse
                   || _TokenActual.Tipo == TokenTipos.PalabraReservadaTrue || _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                   || _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.NumeroOctal || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal
                   || _TokenActual.Tipo == TokenTipos.NumeroFloat)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else if(_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo){
                    EXPRESSION();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("Se esperaba un }");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
                else
                {
                    throw new SintanticoException("Se esperaba que inicialice");
                }
            }

            private void FactorFunctionOrArray()
            {
                if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
                {
                    CallFunction();
                }
                else if(_TokenActual.Tipo==TokenTipos.CorcheteIzquierdo || _TokenActual.Tipo == TokenTipos.LogicaStruct
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
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    ListOfExpressions();
                    if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                    {
                        throw new SintanticoException("Se esperaba ) ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }
            }
            private void IndexOrArrowAccess()
            {
                if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    EXPRESSION();
                    if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
                    {
                        throw new SintanticoException("Se esperaba ] ");
                    }
                    IndexOrArrowAccess();
                   
                }
                else if (_TokenActual.Tipo == TokenTipos.LogicaStruct || _TokenActual.Tipo == TokenTipos.Punto)
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    if (_TokenActual.Tipo == TokenTipos.Identificador)
                    {
                        throw new SintanticoException("Se esperaba identificador ");
                    }
                    _TokenActual = lexico.ObtenerSiguienteToken();
                    IndexOrArrowAccess();
                }
                else
                {
                    _TokenActual = lexico.ObtenerSiguienteToken();
                }

            }
   
        }
    }   
