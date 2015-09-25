using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class JoinErrorHelper
    {
        public void JoinAttributesDoNotMatch(RelationObject joinRelation1, RelationObject joinRelation2)
        {
            string errorMessage = "SyntaxError: The number of attributes in the lists sent in for the joining of \"" + joinRelation1.NameOfRelation + "\" and \"" + joinRelation2.NameOfRelation + "\" did not match.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void JoinAttributesAndTupleAttributesDidNotMatch(RelationObject joinRelation1, RelationObject joinRelation2, RelationObject joinedRelation)
        {
            string errorMessage = "Critical Error: For the joinging of \"" + joinRelation1.NameOfRelation + "\" and \"" + joinRelation2.NameOfRelation + "\" the attributes in joined relation \"" + joinedRelation.NameOfRelation + "\" did not match the expected tuple attributes.  Inform webmaster.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
