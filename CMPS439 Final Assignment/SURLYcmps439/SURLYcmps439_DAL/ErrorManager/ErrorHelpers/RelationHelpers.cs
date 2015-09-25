using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class RelationErrorHelpers
    {
        public void MissingAndStatementForJoin(string curSymbol)
        {
            string errorMessage = "Missing 'AND' in JOIN statement.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void MissingClosingParenthasesStatementForJoin(string curSymbol)
        {
            string errorMessage = "Missing ')' in JOIN statement.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void AttributeDoesNotExistInTheRelation(string curSymbol)
        {
            string errorMessage = "Attribute does not exist inside the relation.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void RelationDoesNotExist()
        {
            string errorMessage = "Relation does not exist.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void RelationDoesNotContainAttributes(string relationName)
        {
            string errorMessage = "Relation \"" + relationName + "\" does not contain any attributes.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void DeleteRequiresTableIsEntered(string curSymbol)
        {
            string errorMessage = "DELETE command requires a table is entered.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void DestroyRequiresTableIsEntered(string curSymbol)
        {
            string errorMessage = "DESTROY command requires one or more tables to be entered for destruction.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
