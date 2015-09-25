using SURLYcmps439_DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SURLYcmps439_DAL.Objects;
using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Constants;

namespace SURLYcmps439_DAL.CRUD.Services.Relation
{
    internal class RelationServices
    {
        internal bool DoesRelationExist(string relationName)
        {
            IList<string> allRelationNames = InternalDatabase.GetAllRelationNames();
            bool doesRelationExist = false;

            if(allRelationNames.Contains(relationName))
            {
                doesRelationExist = true;
            }

            return doesRelationExist;
        }

        internal RelationObject GetRelation(string relationName)
        {
            RelationObject relationToReturn = new RelationObject();

            foreach(RelationObject relation in InternalDatabase.Relations)
            {
                if(relation.NameOfRelation.Equals(relationName))
                {
                    relationToReturn = relation;
                    break;
                }
            }

            return relationToReturn;
        }

        internal IList<AttributeObject> GetAllAttributesOfFirstRelationThatTheUserRequestedAndCheckForNextSymbolToBeAND(RelationObject relationObjectToGrabAttributesFrom)
        {
            IList<AttributeObject> attributesRequestedByUser = new List<AttributeObject>();

            string curSymbol = GetAllAttributesOfARelationThatTheUserRequested(relationObjectToGrabAttributesFrom, attributesRequestedByUser);

            if (!curSymbol.Equals(AllSpecialSymbols.AND))
            {
                ErrorHandlerSURLY.RelationErrors.MissingAndStatementForJoin(curSymbol);
            }

            return attributesRequestedByUser;
        }

        internal IList<AttributeObject> GetAllAttributesOfSecondRelationThatTheUserRequestedAndCheckForNextSymbolToBeClosingParentases(RelationObject relationObjectToGrabAttributesFrom)
        {
            IList<AttributeObject> attributesRequestedByUser = new List<AttributeObject>();

            string curSymbol = GetAllAttributesOfARelationThatTheUserRequested(relationObjectToGrabAttributesFrom, attributesRequestedByUser);

            if (!curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
            {
                ErrorHandlerSURLY.RelationErrors.MissingClosingParenthasesStatementForJoin(curSymbol);
            }

            return attributesRequestedByUser;
        }

        private string GetAllAttributesOfARelationThatTheUserRequested(RelationObject relationObjectToGrabAttributesFrom, IList<AttributeObject> attributesRequestedByUser)
        {
            EnsureObjectContainsAttributes(relationObjectToGrabAttributesFrom);

            string curSymbol = QueryParser.FindNextSymbol();

            while(!AllSpecialSymbols.GetListOfAllSymbols().Contains(curSymbol))
            {
                AttributeObject attributeToAddToAttributesRequestedByUser = new AttributeObject();

                EnsureAttributeExistInObject(curSymbol, relationObjectToGrabAttributesFrom);
                attributeToAddToAttributesRequestedByUser = GetAttributeObjectFromRelationObject(curSymbol, relationObjectToGrabAttributesFrom);

                attributesRequestedByUser.Add(attributeToAddToAttributesRequestedByUser);

                curSymbol = QueryParser.FindNextSymbol();
            }

            return curSymbol;
        }

        internal AttributeObject GetAttributeObjectFromRelationObject(string curSymbol, RelationObject relationObjectToGrabAttributesFrom)
        {
            IList<string> allAttributeNames = GetAllAttributeNamesFromARelation(relationObjectToGrabAttributesFrom);
            IList<AttributeObject> allAttributesFromRelation = GetSelectedAttributesFromARelation(relationObjectToGrabAttributesFrom);

            foreach (AttributeObject item in allAttributesFromRelation)
            {
                if (curSymbol.Equals(item.Name))
                {
                    return item;
                }
            }

            ErrorHandlerSURLY.RelationErrors.AttributeDoesNotExistInTheRelation(curSymbol);
            return null;
        }

        internal void EnsureAttributeExistInObject(IList<AttributeObject> attrObjects, RelationObject relationToCheckAttributesAgainst)
        {
            foreach(AttributeObject attrName in attrObjects)
            {
                EnsureAttributeExistInObject(attrName.Name, relationToCheckAttributesAgainst);
            }
        }

        internal void EnsureAttributeExistInObject(string curSymbol, RelationObject relationObjectToGrabAttributesFrom)
        {
            IList<string> allAttributeNames = GetAllAttributeNamesFromARelation(relationObjectToGrabAttributesFrom);
            IList<AttributeObject> allAttributesFromRelation = GetSelectedAttributesFromARelation(relationObjectToGrabAttributesFrom);
            
            foreach(AttributeObject item in allAttributesFromRelation)
            {
                if(allAttributeNames.Contains(item.Name))
                {
                    return;
                }
            }

            ErrorHandlerSURLY.RelationErrors.AttributeDoesNotExistInTheRelation(curSymbol);
        }

        internal void EnsureObjectContainsAttributes(RelationObject objectToGetAttributeNamesFrom)
        {
            if (objectToGetAttributeNamesFrom == null)
            {
                ErrorHandlerSURLY.RelationErrors.RelationDoesNotExist();
            }
            else if (objectToGetAttributeNamesFrom.RelationAttributes == null)
            {
                ErrorHandlerSURLY.RelationErrors.RelationDoesNotContainAttributes(objectToGetAttributeNamesFrom.NameOfRelation);
            }
        }

        internal static IList<string> GetAllAttributeNamesFromARelation(RelationObject objectToGetAttributeNamesFrom)
        {
            IList<string> AllAttributeNames = new List<string>();

            if (objectToGetAttributeNamesFrom.RelationAttributes == null)
            {
                ErrorHandlerSURLY.RelationErrors.RelationDoesNotContainAttributes(objectToGetAttributeNamesFrom.NameOfRelation);
            }

            foreach (AttributeObject attr in objectToGetAttributeNamesFrom.RelationAttributes)
            {
                AllAttributeNames.Add(attr.Name);
            }

            return AllAttributeNames;
        }

        internal IList<AttributeObject> GetSelectedAttributesFromARelation(RelationObject objectToGet)
        {
            IList<AttributeObject> allAttributesFromRelation = objectToGet.RelationAttributes;

            return allAttributesFromRelation;
        }
    }
}
