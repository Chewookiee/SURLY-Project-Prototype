using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD.Services.Query;
using SURLYcmps439_DAL.CRUD.Services.Tuple;
using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD.Services.Relation
{
    internal class RelationCRUDServices 
    {
        private RelationServices RelationService;
        private QueryServices queryService;
        private AttributeCRUD attributeCRUD;
        RelationCRUD Parent;

        public RelationCRUDServices(RelationCRUD parent)
        {
            Parent = parent;
            RelationService = new RelationServices();
            attributeCRUD = new AttributeCRUD();
            queryService = new QueryServices();
        }

        internal void AssignRelationToEitherDestroyedObjectOrNewObjectBasedOnName()
        {
            string curSymbol = QueryParser.FindNextSymbol();

            IList<string> RelationNameAsList = new List<string>();
            RelationNameAsList.Add(curSymbol);

            if(RelationService.DoesRelationExist(curSymbol))
            {
                Parent.DestroyRelation(RelationNameAsList);
            }

            Parent.relationToCreate = new RelationObject() { NameOfRelation = curSymbol };
        }

        internal void AssignAttributesFromQueryToListOfAttributes()
        {
            string symbolToCheck = QueryParser.FindNextSymbol();

            // TODO
            // ERRO CHECKING
            while (!symbolToCheck.Equals(Constants.SymbolConstants.CloseParenthases))
            {
                queryService.EnsureCurrentSymbolIsNotAKeyWord(symbolToCheck);
                queryService.EnsureCurrentSymboldContainIllegalCharacters(symbolToCheck);
                string NameOfAttribute = symbolToCheck;

                symbolToCheck = QueryParser.FindNextSymbol();
                queryService.EnsureCurrentSymbolIsEitherNumOrChar(symbolToCheck);
                string TypeOfAttribute = symbolToCheck;

                symbolToCheck = QueryParser.FindNextSymbol();
                queryService.ConfirmTypleIsANumber(symbolToCheck);
                string SizeOfAttribute = symbolToCheck;

                symbolToCheck = QueryParser.FindNextSymbol();

                Parent.attributesForRelationToCreate.Add(attributeCRUD.Create(NameOfAttribute, TypeOfAttribute, SizeOfAttribute));
            }
        }

        internal void AddListOfAttributesToRelationToCreate()
        {
            foreach(AttributeObject item in Parent.attributesForRelationToCreate)
            {
                Parent.relationToCreate.RelationAttributes.Add(item);
            }
        }

        internal void GetRelationNameFromQueryForDelete()
        {
            string currentSymbol = QueryParser.FindNextSymbol();

            if (currentSymbol == SymbolConstants.SimiColorn)
            {
                ErrorHandlerSURLY.RelationErrors.DeleteRequiresTableIsEntered(currentSymbol);
            }

            RelationService.DoesRelationExist(currentSymbol);
            Parent.RelationNameToDelete = currentSymbol;
        }

        internal void DeleteRelation()
        {
            RelationObject RelationToDelete = RelationService.GetRelation(Parent.RelationNameToDelete);
            InternalDatabase.DeleteRelation(RelationToDelete);
        }

        internal void GetRelationNamesFromQueryForDestroy()
        {
            string currentSymbol = QueryParser.FindNextSymbol();

            if(currentSymbol == SymbolConstants.SimiColorn)
            {
                ErrorHandlerSURLY.RelationErrors.DestroyRequiresTableIsEntered(currentSymbol);
            }

            while(currentSymbol != SymbolConstants.SimiColorn)
            {
                RelationService.DoesRelationExist(currentSymbol);
                Parent.RelationNamesToDestroy.Add(currentSymbol);
                currentSymbol = QueryParser.FindNextSymbol();
            }
        }
    }
}
