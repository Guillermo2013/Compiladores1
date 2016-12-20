using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiladores.Arbol.Accesores;
using Compiladores.Arbol.Sentencia;
using Compiladores.Semantico.Tipos;
using Compiladores.Semantico;
using Compiladores.Sintactico;
using Compiladores.Implementacion;
namespace Compiladores.Arbol.Identificador
{
   public class IdentificadorArrayNode:StatementNode
    {
        public GeneralDeclarationNode identificador;
        public AccesoresNode unidimesionarioArray;
        public AccesoresNode BidimesionarioArray;
        public List<ExpressionNode> inicializacion;
        public override void ValidSemantic()
        {
            var arreglo = new ArrayTipo();
            arreglo.tipo = obtenerTipo(identificador.tipo);
            if (unidimesionarioArray != null)
            {
                arreglo.unidimensional = true;
                unidimesionarioArray.ValidSemantic();
            } if (BidimesionarioArray != null)
            {
                arreglo.bidimensional = true;
                BidimesionarioArray.ValidSemantic();
            } if (inicializacion != null)
                foreach (var expresion in inicializacion)
                {
                   var tipo = expresion.ValidateSemantic();
                   var tipoExpresion =obtenerTipo(identificador.tipo);
                   if (tipo.GetType() != tipoExpresion.GetType())
                       throw new SintanticoException("no se puede asignar" + tipo + " con " + tipoExpresion);
                }
            
            if (ContenidoStack.InstanceStack.Stack == null)
                ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador.identificador, arreglo);

            else
            {
                bool existe = false;
                foreach (var stack in ContenidoStack.InstanceStack.Stack)
                {
                    if (stack.VariableExist(identificador.identificador))
                        existe = true;
                };
                if (existe == true)
                    throw new SintanticoException("variable " + identificador + " ya existe");
                if (existe == false)
                    ContenidoStack.InstanceStack.Stack.Peek().DeclareVariable(identificador.identificador , arreglo);
            }
            
            
        }
        private TiposBases obtenerTipo(String tipo)
        {
            if (tipo == "bool")
                return new BooleanTipo();
            else if (tipo == "char")
                return new CharTipo();
            else if (tipo == "date")
                return new DateTipo();
            else if (tipo == "enum")
                return new EnumTipo();
            else if (tipo == "float")
                return new FloatTipo();
            else if (tipo == "int")
                return new IntTipo();
            else if (tipo == "string")
                return new StringTipo();
            else if (tipo == "struct")
                return new StructTipo();
            else if (tipo == "void")
                return new VoidTipo();
            else if (tipo == "const")
                return new VoidTipo();
            return null;

        }
        public override void Interpret()
        {
            foreach (var Stack in ContenidoStack.InstanceStack.Stack)
            {
                if (Stack.VariableExist(identificador.identificador))
                {
                    if (Stack.VariableExist(identificador.identificador))
                    {
                       var tipo = Stack.GetVariableValue(identificador.identificador);
                        if (unidimesionarioArray != null)
                            Array.Resize(ref (Stack.GetVariableValue(identificador.identificador) as ArrayValue).Value, ((unidimesionarioArray as ArrayAsesorNode).tamaño.Interpret() as IntValue).Value+1);
                    }
                }
                
                
             }
             foreach (var Stack in ContenidoStack.InstanceStack.Stack)
            {
                if (Stack.VariableExist(identificador.identificador))
                {
                    if (Stack.VariableExist(identificador.identificador))
                    {
                       
                        if (inicializacion != null)
                        {
                            int i = 0;
                            foreach (var expresiones in inicializacion)
                            {
                                var valor = expresiones.Interpret();
                               (Stack._values[identificador.identificador] as ArrayValue).Value[i] = valor;
                                i++;  
                            }

                        }
                    }
                }
             }

            }
        public override string GenerarCodigo()
        {
            string codigo = "";
            if (identificador != null)
                codigo += identificador.GenerarCodigo();
            if (unidimesionarioArray != null)
                codigo += unidimesionarioArray.GenerarCodigo();
            if (BidimesionarioArray != null)
                codigo += BidimesionarioArray.GenerarCodigo();
            codigo += "=";
            if(inicializacion != null)
            foreach (var inicio in inicializacion)
                codigo += inicio.GenerarCodigo();
            codigo += ";";
            return codigo;
        }
        }
    }

