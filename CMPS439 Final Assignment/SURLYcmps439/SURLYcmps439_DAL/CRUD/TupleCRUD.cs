using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD.Services.Query;
using SURLYcmps439_DAL.CRUD.Services.Tuple;
using SURLYcmps439_DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD
{
    internal class TupleCRUD
    {
        internal string RelationshipNameForTuple { get; set; }
        internal TupleCRUDServices tupleCRUDService { get; set; }
        internal QueryServices queryService { get; set; }

        internal TupleCRUD()
        {
            RenewObjects();
        }

        internal void Create()
        {
            tupleCRUDService.ConfirmRelationNameExists();
            tupleCRUDService.AddTuplesToRelation();
            queryService.ConfirmEndOfStatement();
        }

        internal void Read()
        {
            throw new NotImplementedException();
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }

        internal void Destroy()
        {
            throw new NotImplementedException();
        }

        internal void Input()
        {
            bool HasNotReachedEndOfInput = tupleCRUDService.ConfirmRelationNameExistsAndThatItDoesNotEqualEndOfInput();

            while (HasNotReachedEndOfInput)
            {
                do
                {
                    tupleCRUDService.AddTuplesToRelation();
                    queryService.ConfirmEndOfStatement();
                } while (!QueryParser.PeakNextChar().ToString().Equals(SymbolConstants.Astrix));

                QueryParser.IncrementCharIndexAndCheck();

                HasNotReachedEndOfInput = tupleCRUDService.ConfirmRelationNameExistsAndThatItDoesNotEqualEndOfInput();
            }

            queryService.ConfirmEndOfStatement();
        }

        internal void RenewObjects()
        {
            RelationshipNameForTuple = string.Empty;
            tupleCRUDService = new TupleCRUDServices(this);
            queryService = new QueryServices();
        }
    }
}
