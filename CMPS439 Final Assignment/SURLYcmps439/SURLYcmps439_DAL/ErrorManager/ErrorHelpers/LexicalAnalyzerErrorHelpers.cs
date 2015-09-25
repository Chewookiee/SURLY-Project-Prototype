using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class LexicalAnalyzerErrorHelpers
    {
        public void WasNotFollowedByAnEqualSign(string curSymbol)
        {
            string errorMessage = "The operation was not followed by an \"=\" sign.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
   
        public void SlashWasNotFollowedByAnAxtrix(string curSymbol)
        {
            string errorMessage = "The operation was not followed by an \"*\" sign to form a comment.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
