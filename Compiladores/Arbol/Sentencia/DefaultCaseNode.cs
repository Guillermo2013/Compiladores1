using Compiladores.Semantico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.Arbol.Sentencia
{
    public class DefaultCaseNode:CaseNode
    {
       public  List<StatementNode> BloqueCondicionalCase{get; set;}
       public override void ValidSemantic()
       {
           foreach (StatementNode sentencia in BloqueCondicionalCase)
               sentencia.ValidSemantic();
       }
    }
}
