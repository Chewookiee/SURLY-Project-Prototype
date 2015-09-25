using SURLYcmps439.Models.Attributes;
using SURLYcmps439.Models.Relations;
using SURLYcmps439.Models.Tuples;
using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.DatabaseAccess
{
    public class DataHydrator
    {

        public IList<RelationsModel> GetAllRelationModelFromDatabaseWithoutTuplesForView()
        {
            IList<RelationObject> ALL_DATABASE_RELATIONS = InternalDatabase.GetAllRelations();

            IList<RelationsModel> listOfRelationsModels = new List<RelationsModel>();

            if (ALL_DATABASE_RELATIONS != null)
            {
                foreach (RelationObject relationObject in ALL_DATABASE_RELATIONS)
                {
                    RelationsModel relationModel = new RelationsModel();
                    LoadAllRelationsWithAttributesAndName(relationObject, relationModel);
                    listOfRelationsModels.Add(relationModel);
                }
            }

            return listOfRelationsModels;
        }

        private void LoadAllRelationsWithAttributesAndName(RelationObject relationObject, RelationsModel relationModel)
        {
            LazyLoadAttributesFromDatabaseToModel(relationObject, relationModel);
            LazyLoadRelationName(relationObject, relationModel);
        }

        public IList<RelationsModel> GetAllRelationsFromDatabaseLazyLoad()
        {
            IList<RelationObject> ALL_DATABASE_RELATIONS = InternalDatabase.GetAllRelations();

            IList<RelationsModel> listOfRelationsModels = new List<RelationsModel>();

            if(ALL_DATABASE_RELATIONS != null)
            {
                foreach (RelationObject relationObject in ALL_DATABASE_RELATIONS)
                {
                    RelationsModel relationsModel = new RelationsModel();
                    LazyLoadRelationsModel(relationObject, relationsModel);
                    listOfRelationsModels.Add(relationsModel);
                }
            }

            return listOfRelationsModels;
        }

        public RelationsModel GetRelationModelFromDatabaseLazyLoaded(string relationObjectName)
        {
            RelationsModel relationModelToReturn = new RelationsModel();

            RelationObject relationObject = InternalDatabase.GetRelationByName(relationObjectName);

            relationModelToReturn = GetRelationModelFromDatabaseLazyLoaded(relationObject);

            return relationModelToReturn;
        }

        public RelationsModel GetRelationModelFromTempRelatinoLazyLoaded(RelationObject relationObject)
        {
            RelationsModel relationModelToReturn = new RelationsModel();

            relationModelToReturn = GetRelationModelFromDatabaseLazyLoaded(relationObject);

            return relationModelToReturn;
        }

        public RelationsModel GetRelationModelFromDatabaseLazyLoaded(RelationObject relationObject)
        {
            RelationsModel relationModelToReturn = new RelationsModel();

            LazyLoadRelationsModel(relationObject, relationModelToReturn);

            return relationModelToReturn;
        }

        private void LazyLoadRelationsModel(RelationObject relationObject, RelationsModel relationModel)
        {
            LazyLoadAttributesFromDatabaseToModel(relationObject, relationModel);
            LazyLoadTuplesFromDatabaseToModel(relationObject, relationModel);
            LazyLoadRelationName(relationObject, relationModel);
        }

        private void LazyLoadAttributesFromDatabaseToModel(RelationObject relationObject, RelationsModel relationModel)
        {
            IList<AttributesModel> attributesModelList = new List<AttributesModel>();

            foreach (AttributeObject attributeObject in relationObject.RelationAttributes)
            {
                AttributesModel attributeModelToAddToList = new AttributesModel();

                attributeModelToAddToList.Name = attributeObject.Name;
                attributeModelToAddToList.Type = attributeObject.Type;
                attributeModelToAddToList.Size = attributeObject.Size;

                attributesModelList.Add(attributeModelToAddToList);
            }

            relationModel.RelationAttributes = attributesModelList;
        }

        private void LazyLoadTuplesFromDatabaseToModel(RelationObject relationObject, RelationsModel relationModel)
        {
            IList<TuplesModel> tulesModelList = new List<TuplesModel>();

            if (relationObject.RelationsTuples != null)
            {
                foreach (TupleObject tupleObject in relationObject.RelationsTuples)
                {
                    TuplesModel tulesModel = new TuplesModel();

                    if (tupleObject.TupleCells != null)
                    {
                        foreach (TupleCellObject tupleCellObject in tupleObject.TupleCells)
                        {
                            tulesModel.TupleCells.Add(LazyTupleCellFromDatabaseForView(tupleCellObject));
                        }
                    }

                    tulesModelList.Add(tulesModel);
                }
            }

            relationModel.RelationsTuples = tulesModelList;
        }

        private void LazyLoadRelationName(RelationObject relationObject, RelationsModel relationModel)
        {
            relationModel.NameOfRelation = relationObject.NameOfRelation;
        }

        private TupleCellModel LazyTupleCellFromDatabaseForView(TupleCellObject tupleCellObject)
        {
            TupleCellModel tupleCellModel = new TupleCellModel();

            tupleCellModel.AttributeAssociatedWithCell = LoadAttributeModelOfTupleCellObjectFromDatabaseForView(tupleCellObject);
            tupleCellModel.Value = tupleCellObject.Value;

            return tupleCellModel;
        }

        private AttributesModel LoadAttributeModelOfTupleCellObjectFromDatabaseForView(TupleCellObject tupleCellObject)
        {
            AttributesModel attrModel = new AttributesModel();

            attrModel.Name = tupleCellObject.AttributeAssociatedWithCell.Name;
            attrModel.Size = tupleCellObject.AttributeAssociatedWithCell.Size;
            attrModel.Type = tupleCellObject.AttributeAssociatedWithCell.Type;

            return attrModel;
        }
    }
}