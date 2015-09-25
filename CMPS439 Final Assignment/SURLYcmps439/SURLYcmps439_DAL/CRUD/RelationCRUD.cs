using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD.Services;
using SURLYcmps439_DAL.CRUD.Services.Relation;
using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.CRUD.Services.Query;

namespace SURLYcmps439_DAL.CRUD
{
    internal class RelationCRUD
    {
        internal RelationObject relationToCreate;
        internal IList<AttributeObject> attributesForRelationToCreate;
        internal AttributeObject currentAttribute;
        internal string currentSymbol;
        internal IList<string> RelationNamesToDestroy;
        internal string RelationNameToDelete;
        private bool reading;
        private RelationCRUDServices RelationCRUDService;
        private RelationServices RelationService;
        private QueryServices QueryService;

        internal RelationCRUD()
        {
            RenewObjects();
        }

        internal void CreateRelation()
        {
            RenewObjects();
            RelationCRUDService.AssignRelationToEitherDestroyedObjectOrNewObjectBasedOnName();
            QueryService.ReadUntilSymbol(SymbolConstants.OpentParenthases);
            RelationCRUDService.AssignAttributesFromQueryToListOfAttributes();
            QueryService.ConfirmEndOfStatement();
            RelationCRUDService.AddListOfAttributesToRelationToCreate();
            InternalDatabase.AddRelationToDatabase(relationToCreate);
            // Add or update relation
            // LOG THAT NEW RELATION WAS CREATED
        }

        internal void ReadRelation()
        {
            throw new NotImplementedException();
        }

        internal void UpdateRelation()
        {
            throw new NotImplementedException();
        }

        internal void DeleteRelation()
        {
            RelationCRUDService.GetRelationNameFromQueryForDelete();
            RelationCRUDService.DeleteRelation();
            QueryService.ConfirmEndOfStatement();

            InternalDatabase.JUSTTOCHECK();
        }

        internal void DestroyRelation()
        {
            RelationCRUDService.GetRelationNamesFromQueryForDestroy();
            DestroyRelation(RelationNamesToDestroy);
        }

        internal void DestroyRelation(IList<string> RelationNames)
        {
            foreach(string relationName in RelationNames)
            {
                RelationObject relationToDestroy = RelationService.GetRelation(relationName);
                InternalDatabase.DestroyRelation(relationToDestroy);
            }
        }

        internal void RenewObjects()
        {
            relationToCreate = new RelationObject();
            attributesForRelationToCreate = new List<AttributeObject>();
            currentAttribute = new AttributeObject();
            currentSymbol = string.Empty;
            RelationNamesToDestroy = new List<String>();
            RelationNameToDelete = string.Empty;
            reading = false;
            RelationCRUDService = new RelationCRUDServices(this);
            RelationService = new RelationServices();
            QueryService = new QueryServices();
        }
    }
}
