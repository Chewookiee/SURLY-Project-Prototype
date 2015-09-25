using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.CRUD.Services.Relation;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.AlgebraicExpressions
{
    class JoinServices
    {
        internal RelationServices relationService { get; set; }

        private RelationObject JoinRelation1 { get; set; }
        private RelationObject JoinRelation2 { get; set; }
        private IList<AttributeObject> AttributeList1 { get; set; }
        private IList<AttributeObject> AttributeList2 { get; set; }
        private AttributeObject[] AttributeList1AsArray;
        private AttributeObject[] AttributeList2AsArray;

        internal JoinServices()
        {
            relationService = new RelationServices();
        }

        internal RelationObject JoinRelations(RelationObject joinRelation1, 
                                            RelationObject joinRelation2, 
                                            IList<AttributeObject> attributeList1, 
                                            IList<AttributeObject> attributeList2)
        {
            LoadParametersToAttributes(joinRelation1, 
                                            joinRelation2, 
                                            attributeList1, 
                                            attributeList2);

            relationService.EnsureAttributeExistInObject(this.AttributeList1, this.JoinRelation1);
            relationService.EnsureAttributeExistInObject(this.AttributeList2, this.JoinRelation2);

            EnsureAttributeListsAreTheSameSize();

            RelationObject joinedRelation = new RelationObject();

            joinedRelation = LoadJoinedRelationDeprojectsAttributeList2();
            joinedRelation = PruneTuplesToAddToJoinedRelation(joinedRelation);

            return joinedRelation;
        }

        private RelationObject PruneTuplesToAddToJoinedRelation(RelationObject joinedRelation)
        {
            for (int i = 0; i < this.AttributeList1AsArray.Length; i++)
            {
                foreach (TupleObject currentTuple1 in this.JoinRelation1.RelationsTuples)
                {
                    foreach (TupleCellObject currentCellOfTuple1 in currentTuple1.TupleCells)
                    {
                        if (currentCellOfTuple1.AttributeAssociatedWithCell == AttributeList1AsArray[i])
                        {

                    foreach (TupleObject currentTuple2 in this.JoinRelation2.RelationsTuples)
                    {
                                foreach(TupleCellObject currentCellOfTuple2 in currentTuple2.TupleCells)
                                {
                                    if(currentCellOfTuple2.AttributeAssociatedWithCell == AttributeList2AsArray[i])
                                    {
                                        
                                        if(currentCellOfTuple1.Value.Equals(currentCellOfTuple2.Value))
                                        {
                                            TupleObject tupleToAddToJoinedRelation = AddTupleExceptForAttributeList2Items(currentTuple1, currentTuple2);
                                            EnsureTupleMatchesJoinedRelationAttributes(joinedRelation, tupleToAddToJoinedRelation);
                                            joinedRelation.RelationsTuples.Add(tupleToAddToJoinedRelation);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            joinedRelation.NameOfRelation = "*TEMPORARY*";

            return joinedRelation;
        }

        private void EnsureTupleMatchesJoinedRelationAttributes(RelationObject joinedRelation, TupleObject tupleToAddToJoinedRelation)
        {
            AttributeObject[] attributesFromJoinedRelation = joinedRelation.RelationAttributes.ToArray();
            TupleCellObject[] tupleCellsFromTupleToAddToJoinedRelation = tupleToAddToJoinedRelation.TupleCells.ToArray();

            for(int i = 0; i < attributesFromJoinedRelation.Length; i++)
            {
                if(attributesFromJoinedRelation[i] != tupleCellsFromTupleToAddToJoinedRelation[i].AttributeAssociatedWithCell)
                {
                    ErrorHandlerSURLY.JoinErrors.JoinAttributesAndTupleAttributesDidNotMatch(this.JoinRelation1, this.JoinRelation2, joinedRelation);
                }
            }
        }

        private TupleObject AddTupleExceptForAttributeList2Items(TupleObject currentTuple1, TupleObject currentTuple2)
        {
            TupleObject tupleToReturn = new TupleObject();

            foreach (TupleCellObject tupleCell in currentTuple1.TupleCells)
            {
                tupleToReturn.TupleCells.Add(tupleCell);
            }

            foreach(TupleCellObject tupleCell in currentTuple2.TupleCells)
            {
                if(!this.AttributeList2.Contains(tupleCell.AttributeAssociatedWithCell))
                {
                    tupleToReturn.TupleCells.Add(tupleCell);
                }
            }

            return tupleToReturn;
        }

        private RelationObject LoadJoinedRelationDeprojectsAttributeList2()
        {
            RelationObject joinedRelation = new RelationObject();

            foreach(AttributeObject attrObject in this.JoinRelation1.RelationAttributes)
            {
                joinedRelation.RelationAttributes.Add(attrObject);
            }

            foreach (AttributeObject attrObject in this.JoinRelation2.RelationAttributes)
            {
                if (!this.AttributeList2.Contains(attrObject))
                {
                    joinedRelation.RelationAttributes.Add(attrObject);
                }
            }

            return joinedRelation;
        }

        private void EnsureAttributeListsAreTheSameSize()
        {
            if (this.AttributeList1.ToArray().Length != this.AttributeList2.ToArray().Length)
            {
                ErrorHandlerSURLY.JoinErrors.JoinAttributesDoNotMatch(this.JoinRelation1, this.JoinRelation2);
            }
        }

        private void LoadParametersToAttributes(RelationObject joinRelation1,
                                            RelationObject joinRelation2,
                                            IList<AttributeObject> attributeList1,
                                            IList<AttributeObject> attributeList2)
        {
            this.JoinRelation1 = joinRelation1;
            this.JoinRelation2 = joinRelation2;
            this.AttributeList1 = attributeList1;
            this.AttributeList2 = attributeList2;
            this.AttributeList1AsArray = attributeList1.ToArray();
            this.AttributeList2AsArray = attributeList2.ToArray();
        }

    }
}
