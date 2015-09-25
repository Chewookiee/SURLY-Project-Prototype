using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class AttributeErrorHelper
    {
        public void UnrecoginizedType(string curSymbol)
        {
            string errorMessage = "Unrecoginized TYPE while creating an attribute.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
