using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class SelectErrorHelper
    {
        public void RequiresAtLeastOneComparison(string curSymbol)
        {
            string errorMessage = "Select statement requires at least one comparison.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void UnrecognizeableBooleanOperator(string curSymbol)
        {
            string errorMessage = "Select statement used an unrecognizeable boolean operator.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void InvalidOperator(string curSymbol)
        {
            string errorMessage = "Invalid operator for SELECT statement.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void IsNotANumber(string curSymbol)
        {
            string errorMessage = "Value sent in to SELECT statement is not a number.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
