using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.Constants;
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
    class SelectServices
    {
        //TODO
        // Separate into individual methods
        private RelationServices relationService;

        internal SelectServices()
        {
            relationService = new RelationServices();
        }

        internal RelationObject GenerateRelationBasedOnSelect(RelationObject selectRelation)
        {
            IList<SelectObject> comparisonResultsBetweenAnds = new List<SelectObject>();
            string curSymbol = QueryParser.FindNextSymbol();

            if(curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
            {
                ErrorHandlerSURLY.SelectErrors.RequiresAtLeastOneComparison(curSymbol);
            }

            while(!curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
            {
                IList<OperationsObject> listOfOperationsBeforeAnOr = new List<OperationsObject>();
                SelectObject containerForOperationsBeforeAnOr = new SelectObject();

                bool firstPassEMERGENCYCHANGEOBJECT = true;

                while (!curSymbol.Equals((AllSpecialSymbols.OR).ToString()) && !curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
                {
                    if(firstPassEMERGENCYCHANGEOBJECT)
                    {
                        firstPassEMERGENCYCHANGEOBJECT = false;
                    }
                    else if(curSymbol.Equals(AllSpecialSymbols.AND))
                    {
                        curSymbol = QueryParser.FindNextSymbol();
                    }
                    else
                    {
                        ErrorHandlerSURLY.SelectErrors.UnrecognizeableBooleanOperator(curSymbol);
                    }

                    OperationsObject currentOperaitonObject = new OperationsObject();

                    AttributeObject attributeObjectOfCurrentParameter = relationService.GetAttributeObjectFromRelationObject(curSymbol, selectRelation);
                    string operatorForComparison = QueryParser.FindNextSymbol();
                    string rightsideOfComparison = QueryParser.FindNextSymbol();

                    currentOperaitonObject.comparisonAttribute = attributeObjectOfCurrentParameter;
                    currentOperaitonObject.comparisonOperation = operatorForComparison;
                    currentOperaitonObject.comparisonValue = rightsideOfComparison;

                    listOfOperationsBeforeAnOr.Add(currentOperaitonObject);

                    curSymbol = QueryParser.FindNextSymbol();
                }

                if(curSymbol.Equals(AllSpecialSymbols.OR))
                {
                    curSymbol = QueryParser.FindNextSymbol();
                }

                containerForOperationsBeforeAnOr.ListOfOperationsSeparateByOrs = listOfOperationsBeforeAnOr;
                comparisonResultsBetweenAnds.Add(containerForOperationsBeforeAnOr);
            }

            RelationObject PrunedRelation = PruneRelationObjectBasedOnSelections(selectRelation, comparisonResultsBetweenAnds);

            PrunedRelation.NameOfRelation = "*TEMPORARY*";

            return PrunedRelation;
        }

        private RelationObject PruneRelationObjectBasedOnSelections(RelationObject selectRelation, IList<SelectObject> comparisonResultsBetweenAnds)
        {
            RelationObject PrunedRelation = new RelationObject();

            PrunedRelation.RelationAttributes = selectRelation.RelationAttributes;

            foreach(TupleObject tupleObject in selectRelation.RelationsTuples)
            {
                IList<bool> boolsForOr = new List<bool>();
                foreach (SelectObject selectObject in comparisonResultsBetweenAnds)
                {
                    IList<bool> boolsForAnd = new List<bool>();
                    foreach (OperationsObject operationObject in selectObject.ListOfOperationsSeparateByOrs)
                    {
                        foreach (TupleCellObject tupleCell in tupleObject.TupleCells)
                        {
                            if (operationObject.comparisonAttribute.Equals(tupleCell.AttributeAssociatedWithCell))
                            {
                                boolsForAnd.Add(CompareItemsBasedOnType(tupleCell.Value,
                                                            operationObject.comparisonOperation,
                                                            operationObject.comparisonValue,
                                                            operationObject.comparisonAttribute.Type));
                            }
                        }
                    }

                    bool resultOfAnds = true;

                    foreach(bool item in boolsForAnd)
                    {
                        if(item == false)
                        {
                            resultOfAnds = false;
                            break;
                        }
                    }

                    boolsForOr.Add(resultOfAnds);
                }

                bool resultOfOrs = false;

                foreach (bool item in boolsForOr)
                {
                    if (item == true)
                    {
                        resultOfOrs = true;
                        break;
                    }
                }

                if(resultOfOrs == true)
                {
                    PrunedRelation.RelationsTuples.Add(tupleObject);
                }
            }

            return PrunedRelation;
        }

        private bool CompareItemsBasedOnType(string leftsideOfComparison, string operatorForComparison, string rightsideOfComparison, string objectType)
        {
            bool comparisonEvaluation = false;

            switch (operatorForComparison)
            {
                case OperatorConstants.EQUALTO:
                    comparisonEvaluation = Equal(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                case OperatorConstants.GREATER_THAN:
                    comparisonEvaluation = GreaterThan(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                case OperatorConstants.GREATER_THAN_OR_EQUAL_TO:
                    comparisonEvaluation = GreaterThanOrEqualTo(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                case OperatorConstants.LESS_THAN:
                    comparisonEvaluation = LessThan(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                case OperatorConstants.LESS_THAN_OR_EQUAL_TO:
                    comparisonEvaluation = LessThanOrEqualTo(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                case OperatorConstants.NOT_EQUAL_TO:
                    comparisonEvaluation = NotEqual(leftsideOfComparison, rightsideOfComparison, objectType);
                    break;
                default:
                    ErrorHandlerSURLY.SelectErrors.InvalidOperator(operatorForComparison);
                    break;
            }

            return comparisonEvaluation;
        }

        private bool Equal(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if(type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if(leftsideAsInt == rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                if(leftside.Equals(rightside))
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private bool GreaterThan(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if (type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if (leftsideAsInt > rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                int result = leftside.CompareTo(rightside);

                if (result > 0)
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private bool GreaterThanOrEqualTo(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if (type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if (leftsideAsInt >= rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                int result = leftside.CompareTo(rightside);

                if (result >= 0)
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private bool LessThan(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if (type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if (leftsideAsInt < rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                int result = leftside.CompareTo(rightside);

                if (result < 0)
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private bool LessThanOrEqualTo(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if (type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if (leftsideAsInt <= rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                int result = leftside.CompareTo(rightside);

                if (result <= 0)
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private bool NotEqual(string leftside, string rightside, string type)
        {
            bool resultOfComparison = false;

            if (type.Equals(AllSpecialSymbols.NUM))
            {
                int leftsideAsInt = ParseStringToInt(leftside);
                int rightsideAsInt = ParseStringToInt(rightside);

                if (leftsideAsInt != rightsideAsInt)
                {
                    resultOfComparison = true;
                }
            }
            else
            {
                if (!leftside.Equals(rightside))
                {
                    resultOfComparison = true;
                }
            }

            return resultOfComparison;
        }

        private int ParseStringToInt(string value)
        {
            int valueAsInt;

            bool DidItParse = Int32.TryParse(value, out valueAsInt);

            if (!DidItParse)
            {
                ErrorHandlerSURLY.SelectErrors.IsNotANumber(value);
            }

            return valueAsInt;
        }
    }
}
