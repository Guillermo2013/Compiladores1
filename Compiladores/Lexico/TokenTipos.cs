﻿using System;
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
        LogicosY,
        Comentario,
        ComentarioBloque,
        PalabraReservadaAuto,
        PalabraReservadaBreak,
        PalabraReservadaCase,
        PalabraReservadaChar,
        PalabraReservadaConst,
        PalabraReservadaContinue,
        PalabraReservadaDefault,
        PalabraReservadaDo,
        PalabraReservadaDouble,
        PalabraReservadaElse,
        PalabraReservadaEnum,
        PalabraReservadaExtern,
        PalabraReservadaFor,
        PalabraReservadaFloat,
        PalabraReservadaGoto,
        PalabraReservadaIf,
        PalabraReservadaInt,
        PalabraReservadaLong,
        PalabraReservadaRegister,
        PalabraReservadaShort,
        PalabraReservadaSigned,
        PalabraReservadaSizeof,
        PalabraReservadaStatic,
        PalabraReservadaStruct,
        PalabraReservadaSwitch,
        PalabraReservadaTypedef,
        PalabraReservadaUnsigned,
        PalabraReservadaVoid,
        PalabraReservadaVolatile,
        PalabraReservadaWhile,
        Directiva,
        Punto,
        PalabraReservadaBool,
        PalabraReservadaTrue,
        PalabraReservadaFalse,
        LogicaStruct,
        PalabraReservadaDate,
        LiteralChar,
        NumeroHexagecimal,
        NumeroOctal,
        NumeroFloat,
        LiteralDate,
        DosPuntos,
        PalabraReservadaPrintF,
        PalabraReservadaString,
        PalabraReservadaScanf,
        PalabraReservadaInclude,
        PalabraReservadaReturn,
        CorrimientoDerecha,
        CorrimientoIzquierda,
        AutoOperacionResiduo,
        AutoOperacionYlogico,
        AutoOperacionXor,
        AutoOperacionOlogico,
        AutoOperacionCorrimientoDerecha,
        AutoOperacionCorrimientoIzquierda,
        HtmlToken,
        CcodeClose
    }
}
