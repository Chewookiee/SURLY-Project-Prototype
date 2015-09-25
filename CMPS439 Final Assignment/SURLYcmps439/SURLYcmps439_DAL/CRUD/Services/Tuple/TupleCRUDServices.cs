using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD.Services.Query;
using SURLYcmps439_DAL.CRUD.Services.Relation;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD.Services.Tuple
{
    internal class TupleCRUDServices
    {
        internal TupleCRUD Parent { get; set; }
        internal RelationServices relationService { get; set; }
        internal QueryServices queryService { get; set; }

        internal TupleCRUDServices(TupleCRUD parent)
        {
            Parent = parent;
            relationService = new RelationServices();
            queryService = new QueryServices();
        }

        internal void ConfirmRelationNameExists()
        {
            string nameOfRelation = QueryParser.FindNextSymbol();

            if(relationService.DoesRelationExist(nameOfRelation))
            {
                Parent.RelationshipNameForTuple = nameOfRelation;
            }
            else
            {
                ErrorHandlerSURLY.TupleErrors.TableDoesNotExist(nameOfRelation);
            }
        }

        internal bool ConfirmRelationNameExistsAndThatItDoesNotEqualEndOfInput()
        {
            string nameOfRelation = QueryParser.FindNextSymbol();

            if (relationService.DoesRelationExist(nameOfRelation))
            {
                Parent.RelationshipNameForTuple = nameOfRelation;
            }
            else if(nameOfRelation.Equals(AllSpecialSymbols.END_INPUT))
            {
                return false;
            }
            else
            {
                ErrorHandlerSURLY.TupleErrors.TableDoesNotExist(nameOfRelation);
            }

            return true;
        }

        internal void AddTuplesToRelation()
        {
            RelationObject relationObjectToAddTupleTo = relationService.GetRelation(Parent.RelationshipNameForTuple);
            IList<TupleCellObject> tupleCellsToBeAddedToTuple = new List<TupleCellObject>();
            TupleObject tupleToBeAdded = new TupleObject();
            string curSymbol = string.Empty;

            
            foreach (AttributeObject attr in relationObjectToAddTupleTo.RelationAttributes)
            {
                curSymbol = QueryParser.FindNextSymbol();

                queryService.EnsureCurrentSymbolIsNotAKeyWord(curSymbol);

                string convertedSizeOfCurSymbol = string.Empty;
                TupleCellObject currentTupleCellToAdd = new TupleCellObject();

                ConfirmTupleValueMatchesAttributeType(attr, curSymbol);
                convertedSizeOfCurSymbol = CropValueToSize(attr, curSymbol);

                currentTupleCellToAdd.AttributeAssociatedWithCell = attr;
                currentTupleCellToAdd.Value = convertedSizeOfCurSymbol;

                tupleCellsToBeAddedToTuple.Add(currentTupleCellToAdd);
            }
            
            if(tupleCellsToBeAddedToTuple.Count != relationObjectToAddTupleTo.RelationAttributes.Count)
            {
                ErrorHandlerSURLY.TupleErrors.NotAllAttributesSatisfied(curSymbol);
            }

            tupleToBeAdded.TupleCells = tupleCellsToBeAddedToTuple;

            relationObjectToAddTupleTo.RelationsTuples.Add(tupleToBeAdded);
        }

        private string CropValueToSize(AttributeObject attr, string curSymbol)
        {
            string convertedSizeOfCurSymbol = string.Empty;

            switch (attr.Type)
            {
                case AttributeConstants.CHAR:
                    convertedSizeOfCurSymbol = CropRightmostCharacters(attr, curSymbol);
                    break;
                case AttributeConstants.NUM:
                    convertedSizeOfCurSymbol = CropLeftmostCharacters(attr, curSymbol);
                    break;
                default:
                    ErrorHandlerSURLY.TupleErrors.InvalidType(attr.Type);
                    break;
            }

            return convertedSizeOfCurSymbol;
        }

        private string CropLeftmostCharacters(AttributeObject attr, string curSymbol)
        {
            string convertedSizeOfCurSymbol = curSymbol;

            if (curSymbol.Length > attr.Size)
            {
                convertedSizeOfCurSymbol = curSymbol.Remove(0, curSymbol.Length - attr.Size);
            }

            return convertedSizeOfCurSymbol;
        }

        private string CropRightmostCharacters(AttributeObject attr, string curSymbol)
        {
            string convertedSizeOfCurSymbol = curSymbol;

            if (curSymbol.Length > attr.Size)
            {
                convertedSizeOfCurSymbol = curSymbol.Remove(attr.Size);
            }

            return convertedSizeOfCurSymbol;
        }

        private void ConfirmTupleValueMatchesAttributeType(AttributeObject attr, string curSymbol)
        {
            switch(attr.Type)
            {
                case AttributeConstants.CHAR:
                    // Already string
                    break;
                case AttributeConstants.NUM:
                    ConfirmTypleIsANumber(curSymbol);
                    break;
                default:
                    ErrorHandlerSURLY.TupleErrors.InvalidType(attr.Type);
                    break;
            }
        }

        private void ConfirmTypleIsANumber(string curSymbol)
        {
            int numericalValue;

            bool result = Int32.TryParse(curSymbol, out numericalValue);

            if(result)
            {
                // Converted to int properly
            }
            else
            {
                ErrorHandlerSURLY.TupleErrors.TupleCellCouldNotBeTurnedIntoAnInt(curSymbol);
            }
        }

    }
}
