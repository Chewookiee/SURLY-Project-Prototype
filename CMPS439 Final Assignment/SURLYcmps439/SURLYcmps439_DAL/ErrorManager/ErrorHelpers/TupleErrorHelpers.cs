using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class TupleErrorHelpers
    {
        public void TableDoesNotExist(string relationRequested)
        {
            string errorMessage = "Relation \"" + relationRequested + "\" does not exist.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void NotAllAttributesSatisfied(string curSymbol)
        {
            string errorMessage = "Not all attributes were satisfied, too few tuple cells sent in.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void InvalidType(string type)
        {
            string errorMessage = "Invalid TYPE.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(type);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void TupleCellCouldNotBeTurnedIntoAnInt(string curSymbol)
        {
            string errorMessage = "Tuple cell could not be convered into an int.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
