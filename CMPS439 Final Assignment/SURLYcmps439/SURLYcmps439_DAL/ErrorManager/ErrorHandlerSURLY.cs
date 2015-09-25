using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.ErrorManager.ErrorHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager
{
    public static class ErrorHandlerSURLY
    {
        #region Error Helpers

        public static JoinErrorHelper JoinErrors = new JoinErrorHelper();
        public static SelectErrorHelper SelectErrors = new SelectErrorHelper();
        public static AttributeErrorHelper AttributeErrors = new AttributeErrorHelper();
        public static QueryErrorHelper QueryErrors = new QueryErrorHelper();
        public static RelationErrorHelpers RelationErrors = new RelationErrorHelpers();
        public static TupleErrorHelpers TupleErrors = new TupleErrorHelpers();
        public static LexicalAnalyzerErrorHelpers LexicalAnalyzerErrors = new LexicalAnalyzerErrorHelpers();

        #endregion

        public static string CurrentLineValue()
        {
            return QueryParser.CurrentLineOfQueryThatIsExecuting();
        }

        public static int CurrentLineNumber()
        {
            return QueryParser.CurrentLineNumberOfQueryThatIsExecuting();
        }

        public static string AdditionalErrorInformation()
        {
            return AdditionalErrorInformation("(Not Available)");
        }

        public static void ThrowError(string message)
        {
            throw new SyntaxError(message);
        }
        public static string AdditionalErrorInformation(string curSymbol)
        {
            string errorMessage = string.Empty;

            errorMessage += "|";
            errorMessage += "Line " + CurrentLineNumber().ToString() + ": " + CurrentLineValue();
            errorMessage += "|";
            errorMessage += "Offending Symbol: \"" + curSymbol + "\"";

            return errorMessage;
        }

        public static void NoRelationsInMemoryToExecuteAnExpressionAgainst(string curSymbol)
        {
            string errorMessage = "No relations in memory to execute an expression against.";
            errorMessage += AdditionalErrorInformation(curSymbol);
            ThrowError(errorMessage);
        }

    }
}