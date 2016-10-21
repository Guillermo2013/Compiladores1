using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores
{
    public enum TokenTipos
    {
        EndOfFile,
        Identificador,
        Numero,
        LiteralString,
        Asignacion,
        FinalDeSentencia,
        Separador,
        Negacion,
        YPorBit,
        OPorBit,
        NegacionPorBit,
        OExclusivoPorBit,
        ParentesisIzquierdo,
        ParentesisDerecho,
        CorcheteIzquierdo,
        CorcheteDerecho,
        LlaveIzquierdo,
        LlaveDerecho,
        OperacionMultiplicacion,
        OperacionSuma,
        OperacionResta,
        OperacionDivision,
        OperacionDivisionResiduo,
        AutoOperacionSuma,
        AutoOperacionResta,
        AutoOperacionMultiplicacion,
        AutoOperacionDivision,
        AutoOperacionDecremento,
        AutoOperacionIncremento,
        RelacionalMayor,
        RelacionalMayorOIgual,
        RelacionalMenor,
        RelacionalMenorOIgual,
        RelacionalIgual,
        RelacionalNoIgual,
        LogicosO,
        LogicosY
        }
}
