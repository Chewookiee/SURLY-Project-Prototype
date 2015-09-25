using SURLYcmps439_DAL.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    internal class ErrorReporting
    {
        internal static void UnexpectedEndOfQuery()
        {
            throw new ArgumentOutOfRangeException("query", QueryParser.Query, "The query exited improperly. Remove all whitespace, returns, tabs, etc. from the end of the query."); 
        }

        internal static void WasNotFollowedByAnEqualSign(string curSymbol)
        {
            ErrorHandlerSURLY.LexicalAnalyzerErrors.WasNotFollowedByAnEqualSign(curSymbol);
        }

        internal static void SlashWasNotFollowedByAnAxtrix(string curSymbol)
        {
            ErrorHandlerSURLY.LexicalAnalyzerErrors.SlashWasNotFollowedByAnAxtrix(curSymbol);
        }


        internal static void EmptyQuery()
        {
            throw new ArgumentOutOfRangeException("query", QueryParser.Query, "The query contains no characters.");
        }
    }
}
