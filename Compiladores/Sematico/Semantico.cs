using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol.BinaryOperador;
using Compiladores.Arbol.UnaryOperador;
using Compiladores.Arbol.Identificador;
using Compiladores.Arbol.TiposDeDatos;
using Compiladores.Arbol;
using System.Collections;
using Compiladores.Arbol.Sentencia;
using Compiladores.Arbol.Accesores;

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
            if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
            {
                _TokenActual = lexico.ObtenerSiguienteToken();
            }
            if (_TokenActual.Tipo == TokenTipos.HtmlToken || _TokenActual.Tipo == TokenTipos.CcodeClose)
            {
                _TokenActual = lexico.ObtenerSiguienteToken();
            }
        }
        public List<StatementNode> Parser()
        {
            var code = CCode();
            if (_TokenActual.Tipo != TokenTipos.EndOfFile)
                throw new Exception("Se esperaba Eof");
            return code;
        }
        List<StatementNode> CCode()
        {
            return ListOfSentences();
        }
        private List<StatementNode> ListOfSpecialSentences()
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
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaReturn || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong
                )
            {
                SpecialSentence();
                ListOfSpecialSentences();
                var statement = SpecialSentence();
                var statementList = ListOfSpecialSentences();
                statementList.Insert(0, statement);
                return statementList;

            }
            else
            {
                return new List<StatementNode>();
            }
        }

        private StatementNode SpecialSentence()
        {

            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                return SPECIALDECLARATION();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
            {
                return IF();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
            {
                return WHILE();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
            {
                return DO();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaFor)
            {
                return FORLOOP();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
            {
                return SWITCH();
            }
            else if (_TokenActual.Tipo == TokenTipos.Identificador
                || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
                || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
            {
                return AssignmentOrFunctionCall();
            }

            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
            {
                return CONST();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
            {
                return BREAK();

            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
            {
                return CONTINUE();
            }
            else if (_TokenActual.Tipo == TokenTipos.Directiva)
            {
                return INCLUDE();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
            {
                return ENUM();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaReturn)
            {
                return Return();
            }
            return null;
        }

        private StatementNode Return()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaReturn)
            {

                ObtenerSiguienteTokenC();
                var expression = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se esperaba ;");

                }
                ObtenerSiguienteTokenC();
                return new ReturnNode { returnExpression = expression };
            }
            return null;

        }

        private StatementNode SPECIALDECLARATION()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                 _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                 _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                var declaracion = GeneralDeclaration();
                return TypeOfDeclarationForFunction(declaracion);
            }
            return null;

        }



        private StatementNode TypeOfDeclarationForFunction(GeneralDeclarationNode declaracion)
        {

            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                return IsArrayDeclaration(declaracion);
            }
            else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
            {
                ObtenerSiguienteTokenC();
                return declaracion;
            }
            else
            {
                var identificador = new IdentificadorStamNode { inicializacion = ValueForId(), pointer = declaracion.pointer, tipo = declaracion.tipo, value = declaracion.identificador };
                var declaracionTotal = MultiDeclaration(declaracion.tipo, identificador);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("Se esperaba un ;");
                }
                ObtenerSiguienteTokenC();
                return declaracionTotal;
            }
        }
        private List<StatementNode> ListOfSentences()
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
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaExtern

               )
            {
                var statement = Sentence();
                var statementList = ListOfSentences();
                statementList.Insert(0, statement);
                return statementList;
            }
            //Lista_Sentencia->Epsilon
            else
            {
                return new List<StatementNode>();
            }
        }
        private StatementNode Sentence()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                return DECLARATION();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
            {
                return IF();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
            {
                return WHILE();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
            {
                return DO();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaFor)
            {
                return FORLOOP();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
            {
                return SWITCH();
            }
            else if (_TokenActual.Tipo == TokenTipos.Identificador
                || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
                || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
            {
                return AssignmentOrFunctionCall();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaStruct)
            {
                return STRUCT();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
            {
                return CONST();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
            {
                return BREAK();

            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
            {
                return CONTINUE();
            }
            else if (_TokenActual.Tipo == TokenTipos.Directiva)
            {
                return INCLUDE();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
            {
                return ENUM();
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaVoid)
            {
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se esperaba un identificador");
                }
                var identificador = _TokenActual;
                var generalDeclaracion = new GeneralDeclarationNode{ tipo = tipo.Lexema, identificador = identificador.Lexema };
                ObtenerSiguienteTokenC();
             return IsFunctionDeclaration(generalDeclaracion); 
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaExtern)
            {
                
                ObtenerSiguienteTokenC();
                var literalString = _TokenActual;
                if (_TokenActual.Tipo == TokenTipos.LiteralString)
                {
                    ObtenerSiguienteTokenC();
                }
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                  var lista =  ListOfSentences();

                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("se esperaba un } ");
                    }
                    ObtenerSiguienteTokenC();
                    return new ExternNode { literalString = literalString.Lexema, listadesentencias = lista };
                }

                else
                {
                    var declaracion =  DECLARATION();
                    return new ExternNode { literalString = literalString.Lexema, declaracion = declaracion };
                }

            }
            return null;
        }

        private StatementNode CONTINUE()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaContinue)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se esperaba ;");

                }
                ObtenerSiguienteTokenC();
                return new ContinueNode();
            }
            return null;

        }

        private StatementNode ENUM()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaEnum)
            {
                ObtenerSiguienteTokenC();
                var identificador = _TokenActual;
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
                var declaracion = EnumeratorList();
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
                return new EnumNode { identificador = identificador.Lexema, ListaEnum = declaracion };
            }
            return null;
        }

        private List<ExpressionNode> EnumeratorList()
        {
            if (_TokenActual.Tipo == TokenTipos.Identificador)
            {
                var enumItem = EnumItem();
                var enumListItem = OptionalEnumItem();
                enumListItem.Insert(0, enumItem);
                return enumListItem;
            }
            else
            {
                return null;
            }
        }

        private List<ExpressionNode> OptionalEnumItem()
        {
            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                ObtenerSiguienteTokenC();
                return EnumeratorList();
            }
            else
            {
                var test = new List<ExpressionNode>();
                return test;
            }
        }

        private ExpressionNode EnumItem()
        {
            if (_TokenActual.Tipo == TokenTipos.Identificador)
            {
                ObtenerSiguienteTokenC();
                return OptionalIndexPosition(_TokenActual);
            }
            return null;
        }

        private ExpressionNode OptionalIndexPosition(Token TokenActual)
        {
            if (_TokenActual.Tipo == TokenTipos.Asignacion)
            {
                ObtenerSiguienteTokenC();
                var index = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Numero && _TokenActual.Tipo != TokenTipos.NumeroHexagecimal && _TokenActual.Tipo != TokenTipos.NumeroOctal)
                {
                    throw new SintanticoException("se esperaba un numero");
                }
                ObtenerSiguienteTokenC();
                if (index.Tipo == TokenTipos.Numero)
                    return new AsignacionNode { OperadorIzquierdo = new IdentificadorNode { value = TokenActual.Lexema }, valor = "=", OperadorDerecho = new LiteralIntNode { valor = Int32.Parse(index.Lexema) } };
                if (index.Tipo == TokenTipos.NumeroHexagecimal)
                    return new AsignacionNode { OperadorIzquierdo = new IdentificadorNode { value = TokenActual.Lexema }, valor = "=", OperadorDerecho = new LiteralIntNode { valor = Convert.ToInt32(index.Lexema, 16) } };
                else
                    return new AsignacionNode { OperadorIzquierdo = new IdentificadorNode { value = TokenActual.Lexema }, valor = "=", OperadorDerecho = new LiteralIntNode { valor = Convert.ToInt32(index.Lexema, 8) } };

            }
            else
            {
                return new IdentificadorNode { value = _TokenActual.Lexema };
            }
        }

        private StatementNode INCLUDE()
        {
            if (_TokenActual.Tipo == TokenTipos.Directiva)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInclude)
                {
                    throw new SintanticoException("se esperaba un include");
                }
                ObtenerSiguienteTokenC();
                var dirrecion = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.LiteralString)
                {
                    throw new SintanticoException("se esperaba un direccion");
                }
                ObtenerSiguienteTokenC();
                return new IncludeNode { direccion = _TokenActual.Lexema };
            }
            return null;
        }

        private StatementNode CONST()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaConst)
            {
                ObtenerSiguienteTokenC();
                var tipo = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.PalabraReservadaInt && _TokenActual.Tipo != TokenTipos.PalabraReservadaFloat &&
               _TokenActual.Tipo != TokenTipos.PalabraReservadaChar && _TokenActual.Tipo != TokenTipos.PalabraReservadaBool && _TokenActual.Tipo != TokenTipos.PalabraReservadaString &&
               _TokenActual.Tipo != TokenTipos.PalabraReservadaDate && _TokenActual.Tipo != TokenTipos.PalabraReservadaLong)
                {
                    throw new SintanticoException("se esperaba un tipo de dato");
                }
                ObtenerSiguienteTokenC();
                var pointe = IsPointer();
                var identificador = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se esperaba un identificador");
                }
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.Asignacion)
                {
                    throw new SintanticoException("se esperaba un =");
                }
                ObtenerSiguienteTokenC();
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se esperaba un ;");
                }
                ObtenerSiguienteTokenC();
                return new ConstNode { identificador = identificador.Lexema, pointer = pointe, expresion = expresion, Tipo = tipo.Lexema };
            }
            return null;
        }

        private StatementNode STRUCT()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaStruct)
            {
                ObtenerSiguienteTokenC();
                var identificador = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se esperaba un identificado ");
                }
                ObtenerSiguienteTokenC();
                var Struct = StructDeclarationOrInitialization(identificador);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se esperaba un ; ");
                }
                ObtenerSiguienteTokenC();
                return Struct;
            }
            return null;
        }

        private StatementNode StructDeclarationOrInitialization(Token identificador)
        {
            if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var bloqueStruct = MembersList();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("se esperaba un } ");
                }
                ObtenerSiguienteTokenC();
                  var pointer = IsPointer();
                    if (_TokenActual.Tipo == TokenTipos.Identificador)
                    {
                        var structadefinicion = new StructNode { identificador = identificador.Lexema, bloqueStruct = bloqueStruct, variableNombre = _TokenActual.Lexema, pointer= pointer };
                        ObtenerSiguienteTokenC();
                        var expresion = OptionalInitOfStruct();
                        if (expresion != null)
                            structadefinicion.asignacion.OperadorIzquierdo = expresion;
                        return MultiDeclarationStructs(structadefinicion);
                    }
                
                return new StructNode { identificador = identificador.Lexema, bloqueStruct = bloqueStruct };
            }
            else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion || _TokenActual.Tipo == TokenTipos.Identificador)
            {
                var pointer = IsPointer();
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se esperaba un identificador ");
                }
                var structadefinicion = new StructNode { identificador = identificador.Lexema, variableNombre = _TokenActual.Lexema, pointer = pointer };
                ObtenerSiguienteTokenC();
                structadefinicion.asignacion.OperadorIzquierdo = OptionalInitOfStruct();
                return MultiDeclarationStructs(structadefinicion);
            }
            return null;
        }

        private StatementNode MultiDeclarationStructs(StatementNode structVariable)
        {
            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                var separador = _TokenActual;
                ObtenerSiguienteTokenC();
                var pointer = IsPointer();
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se esperaba un identificador ");
                }
                var identificador = new IdentificadorStamNode { pointer = pointer, value = _TokenActual.Lexema };

                ObtenerSiguienteTokenC();
                var separadorStament = new SepardadorStamentNode { valor = separador.Lexema, OperadorDerecho = structVariable, OperadorIzquierdo = identificador };
                identificador.inicializacion= OptionalInitOfStruct();

               return MultiDeclarationStructs(separadorStament);
            }
            else
            {
                return structVariable;
            }
        }

        private ExpressionNode OptionalInitOfStruct()
        {
            if (_TokenActual.Tipo == TokenTipos.Asignacion)
            {
                var token = _TokenActual;  
               return ValueForId();
            }
            return null;
        }



        private List<StatementNode> MembersList()
        {
            return DeclarationOfStruct();
        }

        private List<StatementNode> DeclarationOfStruct()
        {
            var general = GeneralDeclaration();
            var IsArray = ArrayIdentifier();
            var arrayBi = BidArray();
            if (IsArray == null)
            {
                var declaracion = general;
                var declaracion2 = MultiDeclaration(general.tipo, general);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                var declaracionlista = OptionalMember(); 
                declaracionlista.Insert(0, declaracion2);
               return declaracionlista;
            }
            else
            {
                var declarcion = new IdentificadorArrayNode { identificador = general, unidimesionarioArray = IsArray, BidimesionarioArray = arrayBi };
                var declaracion2 = MultiDeclaration(general.tipo, declarcion);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                var declaracionlista = OptionalMember();
                declaracionlista.Insert(0, declaracion2);
                return declaracionlista;
            }

        }

        private List<StatementNode> OptionalMember()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                return DeclarationOfStruct();
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        private ArrayAsesorNode ArrayIdentifier()
        {
            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var tamaño = SizeForArray();
                if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                {
                    throw new SintanticoException("se espera ]");
                }
                ObtenerSiguienteTokenC();
                return new ArrayAsesorNode { tamaño = tamaño };
            }
            else
            {
                return null;
            }
        }

        private StatementNode AssignmentOrFunctionCall()
        {
            if (_TokenActual.Tipo == TokenTipos.Identificador)
            {
                var identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                var ArrayOrIdentificador = IsArray(identificador, new List<ExpressionNode>(), " ");
                var identificadorConAsesor = ArrowOrDot(ArrayOrIdentificador);
                var expresion = ValueForPreId();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                return new AssignStamentExpresionNode { OperadorIquierdo = identificadorConAsesor, expresion = expresion };
            }
            else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
            {
                var pointer = IsPointer();
                var identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                var ArrayOrIdentificador = IsArray(identificador, pointer,null);
                var identificadorConAsesor = ArrowOrDot(ArrayOrIdentificador);
                var expresion = ValueForPointerId();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                return new AssignStamentExpresionNode { OperadorIquierdo = identificadorConAsesor, expresion = expresion };
            }
            else if (_TokenActual.Tipo == TokenTipos.AutoOperacionDecremento || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento)
            {
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                var pointer = IsPointer();
                var identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                var ArrayOrIdentificador = IsArray(identificador, pointer, " ");
                var identificadorConAsesor = ArrowOrDot(ArrayOrIdentificador);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("se espera ;");
                }
                ObtenerSiguienteTokenC();
                if (token.Tipo == TokenTipos.AutoOperacionDecremento)
                    return new AssignStamentExpresionNode { OperadorIquierdo = identificadorConAsesor, expresion = new AutoOperacionDecrementoPre {  Value = token.Lexema} };
                return new AssignStamentExpresionNode { OperadorIquierdo = identificadorConAsesor, expresion = new AutoOperacionIncrementoPre { Value = token.Lexema } };
            }
            return null;
        }

      

        private ExpressionNode ValueForPointerId()
        {
            if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
            {
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                if (tipo.Tipo == TokenTipos.AutoOperacionIncremento) return new AutoOperacionIncrementoPos { Value = tipo.Lexema };
                else return new AutoOperacionDecrementoPos { Value = tipo.Lexema };
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
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                var expression = EXPRESSION();
                if (_TokenActual.Tipo == TokenTipos.Asignacion) return new AsignacionNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionSuma) return new AutoOperacionSumaNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionResta) return new AutoOperacionRestaNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionMultiplicacion) return new AutoOperacionMultiplicacionNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionDivision) return new AutoOperacionDivisionNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionResiduo) return new AutoOperacionResiduoNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionOlogico) return new AutoOperacionOlogicoNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionXor) return new AutoOperacionXorNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionYlogico) return new AutoOperacionYlogicoNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha) return new AutoOperacionCorrimientoDerechaNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda) return new AutoOperacionCorrimientoIzquierdaNode { valor = tipo.Lexema, OperadorDerecho = expression };
            }
            else
            {
                return null;
            }
            return null;
        }

        private StatementNode ArrowOrDot(StatementNode identificador)
        {
            if (_TokenActual.Tipo == TokenTipos.Punto || _TokenActual.Tipo == TokenTipos.LogicaStruct)
            {
                var acesor = _TokenActual;
                var accesorPunto = new PuntoNode { valor = _TokenActual.Lexema , OperadorIzquierdo = identificador};
                var accesorLogicaStruct = new LogicaStructNode { valor = _TokenActual.Lexema, OperadorIzquierdo = identificador };
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("se espera identificador");
                }
                var Identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                var indentificadorOArray = IsArray(Identificador,new List<ExpressionNode>()," ");
                if (acesor.Tipo == TokenTipos.Punto)
                {
                    accesorPunto.OperadorDerecho = indentificadorOArray;
                    return accesorPunto;
                }
                else
                {
                    accesorLogicaStruct.OperadorDerecho = indentificadorOArray;
                    return accesorPunto;
                }
                    

            }
            else
            {
                return identificador;
            }
        }

        private ExpressionNode ValueForPreId()
        {
            if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
            {
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                if (tipo.Tipo == TokenTipos.AutoOperacionIncremento) return new AutoOperacionIncrementoPos { Value = tipo.Lexema };
                else return new AutoOperacionDecrementoPos { Value = tipo.Lexema };
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
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                var expression = EXPRESSION();
                if (_TokenActual.Tipo == TokenTipos.Asignacion) return new AsignacionNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionSuma) return new AutoOperacionSumaNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionResta) return new AutoOperacionRestaNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionMultiplicacion) return new AutoOperacionMultiplicacionNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionDivision) return new AutoOperacionDivisionNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionResiduo) return new AutoOperacionResiduoNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionOlogico) return new AutoOperacionOlogicoNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionXor) return new AutoOperacionXorNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionYlogico) return new AutoOperacionYlogicoNode { valor = tipo.Lexema, OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha) return new AutoOperacionCorrimientoDerechaNode { valor = tipo.Lexema,  OperadorDerecho = expression };
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda) return new AutoOperacionCorrimientoIzquierdaNode { valor = tipo.Lexema,  OperadorDerecho = expression };
            }
            else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                return CallFunction();
            }
            return null;
        }

        private StatementNode FORLOOP()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaFor)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                {
                    throw new SintanticoException("es esperaba un ( ");
                }
                ObtenerSiguienteTokenC();
                return ForOrForEach();
            }
            return null;
        }

        private StatementNode ForOrForEach()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                ObtenerSiguienteTokenC();
                var inicializacion = _TokenActual;
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
                var Lista = _TokenActual;
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
                var bloqueloop = BlOCKFORLOOP();
                return new ForEachNode { inicializacionForEach = inicializacion.Lexema, BloqueCondicionalForEach = bloqueloop, ListaForEach = Lista.Lexema };
            }
            else
            {
                var expresionInicial = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("es esperaba un ; ");
                }
                ObtenerSiguienteTokenC();
                var expresionComparacion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("es esperaba un ; ");
                }
                ObtenerSiguienteTokenC();

                var expresionAumento = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("es esperaba un ) ");
                }
                ObtenerSiguienteTokenC();
                var bloqueloop = BlOCKFORLOOP();
                return new ForNode { ExpresionDeclaracion = expresionInicial, ExpresionCondicional = expresionComparacion, ExpresionIncremento = expresionAumento, BloqueCondicionalFor = bloqueloop };

            }
        }

        private StatementNode DO()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDo)
            {
                ObtenerSiguienteTokenC();
                var bloqueLoop = BlOCKFORLOOP();
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
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("es esperaba ) ");
                }
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("es esperaba ; ");
                }
                ObtenerSiguienteTokenC();
                return new DoWhileNode { condicional = expresion, BloqueCondicionalDoWhile = bloqueLoop };
            }
            return null;
        }

        private List<StatementNode> BlOCKFORLOOP()
        {
            if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var listaSentencia = ListOfSpecialSentences();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("es esperaba } ");
                }
                ObtenerSiguienteTokenC();
                return listaSentencia;
            }
            else if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
            {
                List<StatementNode> lista = new List<StatementNode>();
                lista.Add(Sentence());
                return lista;
            }
            else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
            {
                ObtenerSiguienteTokenC();
                return null;
            }
            return null;
        }

        private StatementNode SWITCH()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaSwitch)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                {
                    throw new SintanticoException("es esperaba ( ");
                }
                ObtenerSiguienteTokenC();
                var expression = EXPRESSION();
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
                var listadecaso = ListOfCase();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("es esperaba } ");
                }
                ObtenerSiguienteTokenC();
                return new SwitchNode { condicional = expression, BloqueCondicionalCase = listadecaso };
            }
            return null;
        }

        private List<CaseNode> ListOfCase()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaCase)
            {
                var caso = CASE();
                var listadecasos = ListOfCase();
                listadecasos.Insert(0, caso);
                return listadecasos;
            }
            else if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDefault)
            {
                var caso = DefaultCase();
                var listadecasos = new List<CaseNode>();
                listadecasos.Add(caso);
                return listadecasos;
            }
            else
            {
                return null;
            }
        }

        private DefaultCaseNode DefaultCase()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaDefault)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                {
                    throw new SintanticoException("es esperaba : ");
                }
                ObtenerSiguienteTokenC();
                var listaSentencia = ListOfSpecialSentences();
                return new DefaultCaseNode { BloqueCondicionalCase = listaSentencia };

            }
            return null;
        }

        private CaseNode CASE()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaCase)
            {
                ObtenerSiguienteTokenC();
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.DosPuntos)
                {
                    throw new SintanticoException("es esperaba : ");
                }
                ObtenerSiguienteTokenC();
                var listaSentencias = ListOfSpecialSentences();
                var breakNod = BREAK();
                return new CaseNode { condicional = expresion, BloqueCondicionalCase = listaSentencias, breakNode = breakNod };

            }
            return null;
        }



        private BreakNode BREAK()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaBreak)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("es esperaba ; ");
                }
                ObtenerSiguienteTokenC();
                return new BreakNode();
            }
            else
            {
                return null;
            }

        }

        private StatementNode WHILE()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaWhile)
            {
                ObtenerSiguienteTokenC();

                if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                {
                    throw new SintanticoException("es esperaba ( ");
                }
                ObtenerSiguienteTokenC();
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("es esperaba ) ");
                }
                ObtenerSiguienteTokenC();
                var bloqueLoop = BlOCKFORLOOP();
                return new WhileNode { condicional = expresion, BloqueCondicionalWhile = bloqueLoop };
            }
            return null;
        }

        private StatementNode IF()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaIf)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo != TokenTipos.ParentesisIzquierdo)
                {
                    throw new SintanticoException("es esperaba ( ");
                }
                ObtenerSiguienteTokenC();
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("es esperaba ) ");
                }
                ObtenerSiguienteTokenC();
                var bloqueIf = BlOCKFORIF();
                var bloqueElse = ELSE();
                return new IfNode() { condicional = expresion, BloqueCondicionalTrue = bloqueIf, BloqueCondicionalFalse = bloqueElse };
            }
            return null;
        }

        private List<StatementNode> ELSE()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaElse)
            {
                ObtenerSiguienteTokenC();
                return BlOCKFORIF();
            }
            else
            {
                return null;
            }
        }

        private List<StatementNode> BlOCKFORIF()
        {

            if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var listaSentencia = ListOfSentences();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("es esperaba } ");
                }
                ObtenerSiguienteTokenC();
                return listaSentencia;
            }
            else if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
            {
                List<StatementNode> lista = new List<StatementNode>();
                lista.Add(Sentence());
                return lista;
            }
            else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
            {
                ObtenerSiguienteTokenC();
                return null;
            }
            return null;
        }

        private StatementNode DECLARATION()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                var declaracion = GeneralDeclaration();
                return TypeOfDeclaration(declaracion);
            }
            return null;
        }
        private GeneralDeclarationNode GeneralDeclaration()
        {
            if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat ||
                  _TokenActual.Tipo == TokenTipos.PalabraReservadaChar || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool || _TokenActual.Tipo == TokenTipos.PalabraReservadaString ||
                  _TokenActual.Tipo == TokenTipos.PalabraReservadaDate || _TokenActual.Tipo == TokenTipos.PalabraReservadaDouble
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong)
            {
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                var puntero = IsPointer();
                var identificador = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba un identificador");
                }
                ObtenerSiguienteTokenC();
                return new GeneralDeclarationNode { identificador = identificador.Lexema, pointer = puntero, tipo = tipo.Lexema };
            }
            return null;
        }

        private List<ExpressionNode> IsPointer()
        {
            if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
            {
                var puntero = _TokenActual;
                ObtenerSiguienteTokenC();
                var listapuntero = IsPointer();
                listapuntero.Insert(0, new OperacionMultiplicacionNode { valor = puntero.Lexema });
                return listapuntero;
            }
            else
            {
                return new List<ExpressionNode>();
            }
        }
        private StatementNode TypeOfDeclaration(GeneralDeclarationNode declaracion)
        {

            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                var identificador = IsArrayDeclaration(declaracion);
                return MultiDeclaration(declaracion.tipo, identificador);
            }
            else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                return IsFunctionDeclaration(declaracion);
            }
            else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
            {
                ObtenerSiguienteTokenC();
            }
            else
            {
                var inicializacion = ValueForId();
                var identificador = new IdentificadorStamNode { inicializacion = inicializacion, pointer = declaracion.pointer, tipo = declaracion.tipo, value = declaracion.identificador };
                var declaracionTotal = MultiDeclaration(declaracion.tipo, identificador);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("Se esperaba un ;");
                }
                ObtenerSiguienteTokenC();
                return declaracionTotal;
            }
            return null;
        }
        private ExpressionNode ValueForId()
        {
            //ValueForId-> = EXPRESSION
            if (_TokenActual.Tipo == TokenTipos.Asignacion)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
                {
                    ObtenerSiguienteTokenC();
                    var lista = ListOfExpressions();
                    if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                    {
                        throw new SintanticoException("Se esperaba un }");
                    }
                    ObtenerSiguienteTokenC();
                    return lista;
                }
                else
                {
                    return EXPRESSION();
                }


            }
            else//ValueForId-> epsilon
            {
                return null;
            }
        }

        private StatementNode MultiDeclaration(string declaracionTipo, StatementNode declaracion)
        {

            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                return OptionalId(declaracionTipo, declaracion);
            }
            else
            {
                return declaracion;
            }
        }
        private StatementNode OptionalId(string declaracionTipo, StatementNode declaracion)
        {
            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                var lista = ListOfId(declaracionTipo);
                return new SepardadorStamentNode { OperadorDerecho = declaracion, valor = token.Lexema, OperadorIzquierdo = lista };
            }
            else
            {
                return declaracion;
            }

        }
        private StatementNode ListOfId(string declaracionTipo)
        {
            var apuntador = IsPointer();
            if (_TokenActual.Tipo != TokenTipos.Identificador)
            {
                throw new SintanticoException("Se esperaba un identificador");
            }
            var identificador = _TokenActual;
            ObtenerSiguienteTokenC();
            var esArreglo = BidArray();
            var biarreglo = BidArray();
            var inicio = ValueForId();
            if (esArreglo == null)
            {
                var declaracion = new IdentificadorStamNode { value = identificador.Lexema, pointer = apuntador, inicializacion = inicio, tipo = declaracionTipo };
                return OtherIdOrValue(declaracionTipo, declaracion);
            }
            else
            {
                var declaracion = new IdentificadorArrayNode { identificador = new GeneralDeclarationNode { pointer = apuntador, identificador = identificador.Lexema, tipo = declaracionTipo }, unidimesionarioArray = esArreglo, BidimesionarioArray = biarreglo, inicializacion = inicio };
                return OtherIdOrValue(declaracionTipo, declaracion);
            }

        }
        private StatementNode OtherIdOrValue(string declaracionTipo, StatementNode declaracion)
        {

            return OptionalId(declaracionTipo, declaracion);

        }
        private StatementNode IsArrayDeclaration(GeneralDeclarationNode declaracion)
        {
            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var sizeUnaDimesional = SizeForArray();
                if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                {
                    throw new SintanticoException("Se esperaba  ]");
                }
                ObtenerSiguienteTokenC();
                var bidimensional = BidArray();
                var Inicializacion = OptionalInitOfArray();
                var ArrayDeclaracion = new IdentificadorArrayNode { identificador = declaracion, unidimesionarioArray = new ArrayAsesorNode { tamaño = sizeUnaDimesional }, BidimesionarioArray = bidimensional, inicializacion = Inicializacion };
                var multideclaracion = MultiDeclaration(ArrayDeclaracion.identificador.tipo, ArrayDeclaracion);
                if (_TokenActual.Tipo != TokenTipos.FinalDeSentencia)
                {
                    throw new SintanticoException("Se esperaba  ;");
                }
                ObtenerSiguienteTokenC();
                return declaracion;
            }
            return null;
        }
        private ExpressionNode SizeForArray()
        {
            if (_TokenActual.Tipo == TokenTipos.Identificador || _TokenActual.Tipo == TokenTipos.Numero || _TokenActual.Tipo == TokenTipos.NumeroFloat
                || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal || _TokenActual.Tipo == TokenTipos.NumeroOctal)
            {
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                if (token.Tipo == TokenTipos.Identificador) return new IdentificadorNode { value = token.Lexema };
                if (token.Tipo == TokenTipos.Numero) return new LiteralIntNode { valor = Int32.Parse(token.Lexema) };
                if (token.Tipo == TokenTipos.NumeroFloat) return new LiteralFloatNode { valor = float.Parse(token.Lexema) };
                if (token.Tipo == TokenTipos.NumeroHexagecimal) return new LiteralIntNode { valor = Convert.ToInt32(token.Lexema, 16) };
                if (token.Tipo == TokenTipos.NumeroOctal) return new LiteralIntNode { valor = Convert.ToInt32(token.Lexema, 16) };
            }
            else
            {
                return null;
            }
            return null;
        }
        private ArrayAsesorNode BidArray()
        {
            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo == TokenTipos.LlaveDerecho)
                {
                    throw new SintanticoException("Se esperaba un factor");
                }

                var expression = EXPRESSION();

                if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                {
                    throw new SintanticoException("Se esperaba  ]");
                }
                ObtenerSiguienteTokenC();
                return new ArrayAsesorNode { tamaño = expression };
            }
            else
            {
                return null;
            }
        }

        private ExpressionNode OptionalInitOfArray()
        {
            if (_TokenActual.Tipo == TokenTipos.Asignacion)
            {
                ObtenerSiguienteTokenC();
                var token = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.CorcheteIzquierdo)
                {
                    throw new SintanticoException("Se esperaba  un corchete");
                }
                ObtenerSiguienteTokenC();
                var lista = ListOfExpressions();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("Se esperaba  un corchete");
                }
                ObtenerSiguienteTokenC();
                return lista;
            }
            return null;
        }

        private ExpressionNode ListOfExpressions()
        {
            var expression = EXPRESSION();
            return OptionalListOfExpressions(expression);

        }

        private ExpressionNode OptionalListOfExpressions(ExpressionNode expression)
        {
            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                var lista = ListOfExpressions();
                return new SeparadorNode { OperadorDerecho = expression, OperadorIzquierdo = lista, valor = token.Lexema };
            }
            else
            {
                return null;
            }

        }

        private StatementNode IsFunctionDeclaration(GeneralDeclarationNode declaracion)
        {
            if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                ObtenerSiguienteTokenC();
               var Paramentros = ParameterList();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("Se esperaba  un parentesis");
                }
                ObtenerSiguienteTokenC();
                var DeclarationFuntion = OptionalDeclaretionFuntion();
                return new FuntionDeclarationNode { paramentros = Paramentros, declaracionDeFuncion = DeclarationFuntion };
            }
            return null;
        }

        private List<StatementNode> OptionalDeclaretionFuntion()
        {
            if (_TokenActual.Tipo == TokenTipos.CorcheteIzquierdo)
            {
                ObtenerSiguienteTokenC();
              var lista =  ListOfSpecialSentences();
                if (_TokenActual.Tipo != TokenTipos.CorcheteDerecho)
                {
                    throw new SintanticoException("Se esperaba  un parentesis");
                }
                ObtenerSiguienteTokenC();
                return lista;
            }
            else if (_TokenActual.Tipo == TokenTipos.FinalDeSentencia)
            {

                ObtenerSiguienteTokenC();
                return null;
            }
            return null;
        }
        private StatementNode ParameterList()
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
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                var parameto = CHOOSE_ID_TYPE(tipo);
               return OptionaListOfParams(parameto);
            }
            else
            {
                return null;
            }
        }
        private StatementNode CHOOSE_ID_TYPE(Token tipo)
        {
            if (_TokenActual.Tipo == TokenTipos.YPorBit)
            {
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                var identificador = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba  un identificador");
                }
                ObtenerSiguienteTokenC();
                return new YporBitStamentNode { OperandoStament = IsArray(identificador, null, tipo.Lexema), Value = token.Lexema };
            }
            else if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion)
            {
               var pointer =  IsPointer();
                var identificador = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba  un identificador");
                }
                ObtenerSiguienteTokenC();
                return IsArray(identificador, pointer, tipo.Lexema);

            }
            else if (_TokenActual.Tipo == TokenTipos.Identificador)
            {
                var identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                return IsArray(identificador, null, tipo.Lexema);
            }
            return null;
        }

        private StatementNode IsArray(Token identifiador, List<ExpressionNode> pointer, string tipo)
        {
            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var tamaños = SizeForArray();
                if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                {

                    throw new SintanticoException("Se esperaba  un ]");

                }
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo == TokenTipos.LlaveDerecho)
                {

                    throw new SintanticoException("Se esperaba  un ]");

                }
                var biarray = BidArray();
                return new IdentificadorArrayNode { identificador = new GeneralDeclarationNode { identificador = identifiador.Lexema, pointer = pointer , tipo = tipo }, unidimesionarioArray = new ArrayAsesorNode { tamaño = tamaños }, BidimesionarioArray = biarray };
            }
            return new IdentificadorStamNode {  value = identifiador.Lexema,  pointer = pointer, tipo = tipo};
        }

        private StatementNode OptionaListOfParams(StatementNode izquierdo)
        {
            if (_TokenActual.Tipo == TokenTipos.Separador)
            {
                var separador = _TokenActual;
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo == TokenTipos.PalabraReservadaInt
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaFloat
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaChar
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaBool
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaString
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaDate
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaLong
                    || _TokenActual.Tipo == TokenTipos.PalabraReservadaVoid)
                {
                    var tipo = _TokenActual;
                    ObtenerSiguienteTokenC();
                    var parameto = CHOOSE_ID_TYPE(tipo);
                    return new SepardadorStamentNode { valor = separador.Lexema, OperadorIzquierdo = izquierdo, OperadorDerecho = OptionaListOfParams(parameto) };
                    
                }
                else
                {
                    throw new SintanticoException("Se esperaba  una parametro");
                }
            }
            return izquierdo;
        }
        private ExpressionNode EXPRESSION()
        {
            return RelationalExpression();
        }

        private ExpressionNode RelationalExpression()
        {
            var expresionAdicion = ExpressionAdicion();
            return RelationalExpressionPrima(expresionAdicion);
        }

        private ExpressionNode RelationalExpressionPrima(ExpressionNode expresionAdicion)
        {
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
                )
            {
                var Simbolo = _TokenActual;
                BinaryOperatorNode tipoDeSimbolo = ObtenerTipo(Simbolo);
                ObtenerSiguienteTokenC();
                tipoDeSimbolo.OperadorIzquierdo = ExpressionAdicion();
                tipoDeSimbolo.OperadorDerecho = RelationalExpressionPrima(tipoDeSimbolo.OperadorIzquierdo);
                tipoDeSimbolo.valor = Simbolo.Lexema;
                return tipoDeSimbolo;
            }
            else
            {
                return expresionAdicion;
            }

        }

        private BinaryOperatorNode ObtenerTipo(Token Simbolo)
        {

            if (Simbolo.Tipo == TokenTipos.RelacionalMenor)
                return new RelacionalMenorNode();
            else if (Simbolo.Tipo == TokenTipos.RelacionalMenorOIgual)
                return new RelacionalMenorOIgualNode();
            else if (Simbolo.Tipo == TokenTipos.RelacionalMayor)
                return new RelacionalMayorNode();
            else if (Simbolo.Tipo == TokenTipos.RelacionalMayorOIgual)
                return new RelacionalMayorOIgualNode();
            else if (Simbolo.Tipo == TokenTipos.LogicosO)
                return new LogicosONode();
            else if (Simbolo.Tipo == TokenTipos.LogicosY)
                return new LogicosYNode();
            else if (Simbolo.Tipo == TokenTipos.YPorBit)
                return new YporBitBinaryOperadorNode();
            else if (Simbolo.Tipo == TokenTipos.OPorBit)
                return new OPorBitNode();
            else if (Simbolo.Tipo == TokenTipos.CorrimientoDerecha)
                return new CorrimientoDerechaNode();
            else if (Simbolo.Tipo == TokenTipos.CorrimientoIzquierda)
                return new CorrimientoIzquierdaNode();
            else if (Simbolo.Tipo == TokenTipos.RelacionalIgual)
                return new RelacionalIgualNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionSuma)
                return new AutoOperacionSumaNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionResta)
                return new AutoOperacionRestaNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionMultiplicacion)
                return new AutoOperacionMultiplicacionNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionDivision)
                return new AutoOperacionDivisionNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionResiduo)
                return new AutoOperacionResiduoNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionYlogico)
                return new AutoOperacionYlogicoNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionXor)
                return new AutoOperacionXorNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionOlogico)
                return new AutoOperacionOlogicoNode();
            else if (Simbolo.Tipo == TokenTipos.Asignacion)
                return new AsignacionNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionCorrimientoDerecha)
                return new AutoOperacionCorrimientoDerechaNode();
            else if (Simbolo.Tipo == TokenTipos.AutoOperacionCorrimientoIzquierda)
                return new AutoOperacionCorrimientoIzquierdaNode();
            else if (Simbolo.Tipo == TokenTipos.OExclusivoPorBit)
                return new OExclusivoPorBitNode();
            else
                return null;

        }
        private ExpressionNode ExpressionAdicion()
        {
            var expresion = ExpressionMul();
            return ExpressionAdicionPrima(expresion);
        }

        private ExpressionNode ExpressionAdicionPrima(ExpressionNode expresion)
        {
            if (_TokenActual.Tipo == TokenTipos.OperacionSuma
                || _TokenActual.Tipo == TokenTipos.OperacionResta)
            {
                var TokenActualTemp = _TokenActual;
                ObtenerSiguienteTokenC();
                BinaryOperatorNode expresionDerechar = (TokenActualTemp.Tipo == TokenTipos.OperacionSuma) ? expresionDerechar = new OperacionSumaNode() : expresionDerechar = new OperacionRestaNode();
                var expresions = ExpressionMul();
                expresionDerechar.OperadorDerecho = ExpressionAdicionPrima(expresions);
                expresionDerechar.valor = TokenActualTemp.Lexema;
                expresionDerechar.OperadorIzquierdo = expresion;
                return expresionDerechar;
            }
            else
            {
                return expresion;
            }
        }
        private ExpressionNode ExpressionMul()
        {
            var expresion = ExpressionUnary();
            return ExpressionMulPrima(expresion);
        }

        private ExpressionNode ExpressionMulPrima(ExpressionNode expresion)
        {
            if (_TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
                || _TokenActual.Tipo == TokenTipos.OperacionDivision
                || _TokenActual.Tipo == TokenTipos.OperacionDivisionResiduo)
            {

                var TokenActualTemp = _TokenActual;
                BinaryOperatorNode expresionDerechar = (TokenActualTemp.Tipo == TokenTipos.OperacionMultiplicacion) ? expresionDerechar = new OperacionMultiplicacionNode() :
                    (TokenActualTemp.Tipo == TokenTipos.OperacionDivision) ? expresionDerechar = new OperacionDivisionNode() : expresionDerechar = new OperacionDivisionResiduoNode();
                ObtenerSiguienteTokenC();

                var expresionIzquierda = ExpressionUnary();
                expresionDerechar.valor = TokenActualTemp.Lexema;
                expresionDerechar.OperadorIzquierdo = expresion;
                expresionDerechar.OperadorDerecho = ExpressionMulPrima(expresionIzquierda);
                return expresionDerechar;

            }
            else
            {
                return expresion;
            }

        }

        private ExpressionNode ExpressionUnary()
        {
            if (_TokenActual.Tipo == TokenTipos.NegacionPorBit
                || _TokenActual.Tipo == TokenTipos.AutoOperacionIncremento
                || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento
                || _TokenActual.Tipo == TokenTipos.YPorBit
                || _TokenActual.Tipo == TokenTipos.OPorBit
                || _TokenActual.Tipo == TokenTipos.OperacionResta
                || _TokenActual.Tipo == TokenTipos.OperacionMultiplicacion
               )
            {
                var Unary = UnaryOperators();
                Unary.Operando = Factor();
                return Unary;
            }
            else if (_TokenActual.Tipo == TokenTipos.Identificador
                || _TokenActual.Tipo == TokenTipos.Numero
                || _TokenActual.Tipo == TokenTipos.NumeroOctal
                || _TokenActual.Tipo == TokenTipos.NumeroHexagecimal
                || _TokenActual.Tipo == TokenTipos.NumeroFloat
                || _TokenActual.Tipo == TokenTipos.LiteralString
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaFalse
                || _TokenActual.Tipo == TokenTipos.PalabraReservadaTrue
                || _TokenActual.Tipo == TokenTipos.LiteralDate
                || _TokenActual.Tipo == TokenTipos.LiteralChar
                || _TokenActual.Tipo == TokenTipos.ParentesisIzquierdo
                )
            {
                return Factor();
            }
            else
            {
                throw new SintanticoException("Se esperaba un factor");
            }


        }
        private UnaryOperadorNode UnaryOperators()
        {

            var token = _TokenActual;
            ObtenerSiguienteTokenC();
            if (token.Tipo == TokenTipos.NegacionPorBit) return new NegacionPorBitNode { Value = token.Lexema };
            if (token.Tipo == TokenTipos.AutoOperacionIncremento) return new AutoOperacionIncrementoPre { Value = token.Lexema };
            if (token.Tipo == TokenTipos.AutoOperacionDecremento) return new AutoOperacionDecrementoPre { Value = token.Lexema };
            if (token.Tipo == TokenTipos.YPorBit) return new YporBitNode { Value = token.Lexema };
            if (token.Tipo == TokenTipos.Negacion) return new NegacionNode { Value = token.Lexema };
            if (token.Tipo == TokenTipos.OperacionResta) return new NegativoNumeroNode { Value = token.Lexema };
            if (token.Tipo == TokenTipos.OperacionMultiplicacion) return new OperdadorMultiplicacionUnaryNode { Value = token.Lexema };

            return null;    

        }
        private ExpressionNode Factor()
        {
            if (_TokenActual.Tipo == TokenTipos.Identificador)
            {
                var identificador = _TokenActual;
                ObtenerSiguienteTokenC();
                if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento || _TokenActual.Tipo == TokenTipos.AutoOperacionDecremento)
                    return OptionalIncrementOrDecrement(identificador);

               
                return FactorFunctionOrArray(identificador);


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
                var token = _TokenActual;
                ObtenerSiguienteTokenC();
                
                if (token.Tipo == TokenTipos.Numero) return new LiteralIntNode { valor = Int32.Parse(token.Lexema) };
                if (token.Tipo == TokenTipos.NumeroOctal) return new LiteralIntNode { valor = Convert.ToInt32(token.Lexema, 8) };
                if (token.Tipo == TokenTipos.NumeroHexagecimal) return new LiteralIntNode { valor = Convert.ToInt32(token.Lexema, 16) };
                if (token.Tipo == TokenTipos.NumeroFloat) return new LiteralFloatNode { valor = float.Parse(token.Lexema) };
                if (token.Tipo == TokenTipos.LiteralString) return new LiteralStringNode { valor = token.Lexema };
                if (token.Tipo == TokenTipos.PalabraReservadaFalse) return new LiteralBooleanaNode { valor = false };
                if (token.Tipo == TokenTipos.PalabraReservadaTrue) return new LiteralBooleanaNode { valor = true };
                if (token.Tipo == TokenTipos.LiteralChar) return new LiteralCharNode { valor = Char.Parse(token.Lexema) };


            }
            else if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var expresion = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("Se esperaba un }");
                }
                ObtenerSiguienteTokenC();
                return expresion;
            }
            else
            {
                throw new SintanticoException("Se esperaba un factor");
            }
            return null;
        }



        private ExpressionNode OptionalIncrementOrDecrement(Token identificador)
        {
            var tokenInODe = _TokenActual;
            ObtenerSiguienteTokenC();
            if (_TokenActual.Tipo == TokenTipos.AutoOperacionIncremento)
                return new AutoOperacionIncrementoPos { Operando = new IdentificadorNode { value = identificador.Lexema }, Value = tokenInODe.Lexema };
            else
                return new AutoOperacionDecrementoPos { Operando = new IdentificadorNode { value = identificador.Lexema }, Value = tokenInODe.Lexema };

        }

        private ExpressionNode FactorFunctionOrArray(Token _TOKEN)
        {
            if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                return CallFunction(_TOKEN);
            }
            else if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo
                || _TokenActual.Tipo == TokenTipos.LogicaStruct
                || _TokenActual.Tipo == TokenTipos.Punto)
            {
                List<AccesoresNode> lista = new List<AccesoresNode>();
                return IndexOrArrowAccess(_TOKEN,lista);
            }
            else
            {
                return new IdentificadorNode {  value = _TOKEN.Lexema};
            }
        }
        private ExpressionNode CallFunction(Token _TOKEN)
        {
            if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var ListaExpressions = ListOfExpressions();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("Se esperaba ) ");
                }
                ObtenerSiguienteTokenC();
                return new CallFuntionNode { identificador = _TOKEN.Lexema, ListaDeParametros = ListaExpressions };
            }
            return null;
        }
        private ExpressionNode CallFunction()
        {
            if (_TokenActual.Tipo == TokenTipos.ParentesisIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var ListaExpressions = ListOfExpressions();
                if (_TokenActual.Tipo != TokenTipos.ParentesisDerecho)
                {
                    throw new SintanticoException("Se esperaba ) ");
                }
                ObtenerSiguienteTokenC();
                return new CallFuntionNode { ListaDeParametros = ListaExpressions };
            }
            return null;
        }
        private ExpressionNode IndexOrArrowAccess(Token identificador,List<AccesoresNode> acesores)
        {
            if (_TokenActual.Tipo == TokenTipos.LlaveIzquierdo)
            {
                ObtenerSiguienteTokenC();
                var tamaño = EXPRESSION();
                if (_TokenActual.Tipo != TokenTipos.LlaveDerecho)
                {
                    throw new SintanticoException("Se esperaba ] ");
                }
                ObtenerSiguienteTokenC();
                acesores.Add(new ArrayAsesorNode { tamaño = tamaño });
                IndexOrArrowAccess(null,acesores);
                return new IdentificadorNode { value = identificador.Lexema, Asesores = acesores };
            }
            else if (_TokenActual.Tipo == TokenTipos.LogicaStruct || _TokenActual.Tipo == TokenTipos.Punto)
            {
                var tipo = _TokenActual;
                ObtenerSiguienteTokenC();
                var identificadorInterno = _TokenActual;
                if (_TokenActual.Tipo != TokenTipos.Identificador)
                {
                    throw new SintanticoException("Se esperaba identificador ");
                }
                ObtenerSiguienteTokenC();
                var izquierdo = new IdentificadorNode { value = identificadorInterno.Lexema };
                List<AccesoresNode> lista = new List<AccesoresNode>();
                if (tipo.Tipo == TokenTipos.LogicaStruct)
                {
                   var derecho = IndexOrArrowAccess(identificadorInterno, lista);
                   return new LogicaStuctExpressionNode { OperadorIzquierdo = izquierdo, valor = tipo.Lexema, OperadorDerecho = derecho };
                }
                else
                {
                    var derecho = IndexOrArrowAccess(identificadorInterno, lista);
                    return new PuntoExpressionNode { OperadorIzquierdo = izquierdo, valor = tipo.Lexema, OperadorDerecho = derecho };

                }
            }
            else
            {
                return null;
            }

        }

    }

}
