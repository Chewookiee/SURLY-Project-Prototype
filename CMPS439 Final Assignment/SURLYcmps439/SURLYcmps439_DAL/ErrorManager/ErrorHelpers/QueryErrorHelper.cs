using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager.ErrorHelpers
{
    public class QueryErrorHelper
    {
        public void SpecificSymbolWasNotFound(string curSymbol, string symbolsLookedFor)
        {
            string errorMessage = "Invalid symbol \"" + curSymbol +"\".  Statement is expecting one of the following:" + symbolsLookedFor + ".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void UniqueNamesCannotBeASpecialSymbol(string curSymbol)
        {
            string errorMessage = "Unique names cannot be a special symbol.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void JoinProducedNullTable(RelationObject joinRelation1, RelationObject joinRelation2)
        {
            string errorMessage = "The generated table for the JOIN between \"" + joinRelation1 + "\" and \"" + joinRelation2.NameOfRelation + "\" has failed to produce a table.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation();
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void JoinMissingClosingParentheses(RelationObject joinRelation1, RelationObject joinRelation2, string curSymbol)
        {
            string errorMessage = "Missing ')' for the JOIN between \"" + joinRelation1 + "\" and \"" + joinRelation2.NameOfRelation + "\".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void JoinMissingOver(RelationObject joinRelation1, RelationObject joinRelation2, string curSymbol)
        {
            string errorMessage = "Missing 'OVER' for the JOIN between \"" + joinRelation1 + "\" and \"" + joinRelation2.NameOfRelation + "\".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void JoinMissingAnd(RelationObject joinRelation1, RelationObject joinRelation2, string curSymbol)
        {
            string errorMessage = "Missing 'AND' for the JOIN between \"" + joinRelation1 + "\" and \"" + joinRelation2.NameOfRelation + "\".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void SelectMissingOpenParenthases(RelationObject selectRelation, string curSymbol)
        {
            string errorMessage = "Missing '(' for the SELECT of \"" + selectRelation.NameOfRelation + "\".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void SelectMissingWhere(RelationObject selectRelation, string curSymbol)
        {
            string errorMessage = "Missing 'WHERE' statement for the SELECT of \"" + selectRelation.NameOfRelation + "\".";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void NoRelationCommandOrRelationNameFound(string curSymbol)
        {
            string errorMessage = "Exprected a RELATIONAL command or a relation name.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }

        public void ContainedIllegalAnCharacter(string curSymbol)
        {
            string errorMessage = "Input contained an illegal chracter.";
            errorMessage += ErrorHandlerSURLY.AdditionalErrorInformation(curSymbol);
            ErrorHandlerSURLY.ThrowError(errorMessage);
        }
    }
}
