using SURLYcmps439_DAL.AlgebraicExpressions;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD;
using SURLYcmps439_DAL.CRUD.Services.Query;
using SURLYcmps439_DAL.CRUD.Services.Relation;
using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL
{
    public class ResolveQuery
    {
        private RelationCRUD relationCRUD;
        private TupleCRUD tupleCRUD;
        private RelationServices relationService;
        private JoinServices joinServices;
        private QueryServices queryServices;
        private SelectServices selectService;

        public ResolveQuery()
        {
            RenewObjects();
        }

        public RelationObject Load(string query)
        {
            InternalDatabase.BackupDatabase();
            RenewObjects();
            LexicalAnalyzerForQuery.QueryParser.LoadQuery(query);
            RelationObject temp = DirectQuery();
            return temp;
        }

        internal RelationObject DirectQuery()
        {
            string currentSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
            RelationObject TempRelationForExpression = null;

            switch(currentSymbol)
            {
                case "JOIN":
                    TempRelationForExpression = ExecuteRelationalAlgebra(currentSymbol);
                    break;
                case "SELECT":
                    TempRelationForExpression = ExecuteRelationalAlgebra(currentSymbol);
                    break;
                default:
                    ExecuteCommands(currentSymbol);
                    break;
            }

            return TempRelationForExpression;
        }

        internal RelationObject ExecuteRelationalAlgebra(string curSymbol)
        {
            IList<string> allRelationNames = InternalDatabase.GetAllRelationNames();
            RelationObject TempRelationForExpression = null;

            if(allRelationNames == null)
            {
                ErrorHandlerSURLY.NoRelationsInMemoryToExecuteAnExpressionAgainst(curSymbol);
            }

            TempRelationForExpression = RecursiveExpressionManipulator(TempRelationForExpression, curSymbol, allRelationNames);

            return TempRelationForExpression;
        }

        internal RelationObject RecursiveExpressionManipulator(RelationObject state, string curSymbol, IList<string> allRelationNames)
        {
            RelationObject joinedObject = new RelationObject();
            RelationObject joinRelation1 = new RelationObject();
            RelationObject joinRelation2 = new RelationObject();

            RelationObject selectedObject = new RelationObject();
            RelationObject selectRelation = new RelationObject();

            RelationObject TrueRelationObject = new RelationObject();

            string nextSymbol = string.Empty;

            switch (curSymbol)
            {
                case "JOIN":
                    IList<AttributeObject> attributeList1 = new List<AttributeObject>();
                    IList<AttributeObject> attributeList2 = new List<AttributeObject>();

                    curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();

                    if (curSymbol.Equals((AllSpecialSymbols.OpentParenthases).ToString()))
                    {
                        joinRelation1 = RecursiveExpressionManipulator(joinRelation1, LexicalAnalyzerForQuery.QueryParser.FindNextSymbol(), allRelationNames);
                        curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();

                        if(curSymbol.Equals(AllSpecialSymbols.AND))
                        {
                            joinRelation2 = RecursiveExpressionManipulator(joinRelation2, LexicalAnalyzerForQuery.QueryParser.FindNextSymbol(), allRelationNames);
                            curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();

                            if(curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
                            {
                                curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
                                if (curSymbol.Equals(AllSpecialSymbols.OVER))
                                {
                                    curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
                                    if (curSymbol.Equals((AllSpecialSymbols.OpentParenthases).ToString()))
                                    {
                                        attributeList1 = relationService.GetAllAttributesOfFirstRelationThatTheUserRequestedAndCheckForNextSymbolToBeAND(joinRelation1);
                                        attributeList2 = relationService.GetAllAttributesOfSecondRelationThatTheUserRequestedAndCheckForNextSymbolToBeClosingParentases(joinRelation2);

                                        joinedObject = joinServices.JoinRelations(joinRelation1,
                                                        joinRelation2,
                                                        attributeList1,
                                                        attributeList2); // Write method for generating a table based off of what is sent in

                                        if(joinedObject == null)
                                        {
                                            ErrorHandlerSURLY.QueryErrors.JoinProducedNullTable(joinRelation1, joinRelation2);
                                        }

                                        return joinedObject;
                                    }
                                    else
                                    {
                                        ErrorHandlerSURLY.QueryErrors.JoinMissingClosingParentheses(joinRelation1, joinRelation2, curSymbol);
                                    }
                                }
                                else
                                {
                                    ErrorHandlerSURLY.QueryErrors.JoinMissingOver(joinRelation1, joinRelation2, curSymbol);
                                }
                            }
                            else
                            {
                                ErrorHandlerSURLY.QueryErrors.JoinMissingClosingParentheses(joinRelation1, joinRelation2, curSymbol);
                            }
                         
                        }
                        else
                        {
                            ErrorHandlerSURLY.QueryErrors.JoinMissingAnd(joinRelation1, joinRelation2, curSymbol);
                        }
                    }
                    else
                    {
                        ErrorHandlerSURLY.QueryErrors.JoinMissingClosingParentheses(joinRelation1, joinRelation2, curSymbol);
                    }

                    return null;
                    break;

                case "SELECT":
                    curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();

                    if (curSymbol.Equals((AllSpecialSymbols.OpentParenthases).ToString()))
                    {
                        selectRelation = RecursiveExpressionManipulator(selectRelation, LexicalAnalyzerForQuery.QueryParser.FindNextSymbol(), allRelationNames);
                        curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();

                        if (curSymbol.Equals((AllSpecialSymbols.CloseParenthases).ToString()))
                        {
                            curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
                            if (curSymbol.Equals(AllSpecialSymbols.WHERE))
                            {
                                curSymbol = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
                                if (curSymbol.Equals((AllSpecialSymbols.OpentParenthases).ToString()))
                                {
                                    selectedObject = selectService.GenerateRelationBasedOnSelect(selectRelation); //(selectRelation1);  // Make new method that takes in the select relation, then reads user input until ')' and does the operations
                                }
                                else
                                {
                                    ErrorHandlerSURLY.QueryErrors.SelectMissingOpenParenthases(selectRelation, curSymbol);
                                }
                            }
                            else
                            {
                                ErrorHandlerSURLY.QueryErrors.SelectMissingWhere(selectRelation, curSymbol);
                            }
                        }
                    }
                    
                    return selectedObject;
                    break;

                default:
                    if (allRelationNames.Contains(curSymbol))
                    {
                        TrueRelationObject = relationService.GetRelation(curSymbol);
                        return TrueRelationObject;
                    }

                    ErrorHandlerSURLY.QueryErrors.NoRelationCommandOrRelationNameFound(curSymbol);
                    return null;
                    break;
            }
        }

        internal void ExecuteCommands(string Command)
        {
            bool processingQuery = true;

            while (processingQuery)
            {
                switch (Command)
                {
                    case "RELATION":
                        relationCRUD.CreateRelation();
                        break;
                    case "INDEX":
                        throw new NotImplementedException();
                        break;
                    case "INPUT":
                        tupleCRUD.Input();
                        break;
                    case "INSERT":
                        tupleCRUD.Create();
                        break;
                    case "DELETE":
                        relationCRUD.DeleteRelation();
                        break;
                    case "DESTROY":
                        relationCRUD.DestroyRelation();
                        break;
                    case "PRINT":
                        throw new NotImplementedException();
                        break;
                    default:
                        processingQuery = false;
                        break;
                }

                if (LexicalAnalyzerForQuery.QueryParser.AreSymbolsRemaining())
                {
                    Command = LexicalAnalyzerForQuery.QueryParser.FindNextSymbol();
                }
                else
                {
                    processingQuery = false;
                }
            }
        }


        private void RenewObjects()
        {
            relationCRUD = new RelationCRUD();
            tupleCRUD = new TupleCRUD();
            relationService = new RelationServices();
            joinServices = new JoinServices();
            queryServices = new QueryServices();
            selectService = new SelectServices();
        }
    }
}
