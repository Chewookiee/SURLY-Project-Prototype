using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD
{
    internal class IndexCRUD
    {
        internal void Create()
        {
            // IndexObject indexObjectToCreate = new IndexObject();
            // RelatinObject relationObjectToAddIndexTo = null;

            // string NameOfIndex = string.empty();
            // string NameOfPrimaryRelation = string.empty();
            // IList<string> NameOfAttributesToIndex = new List<string>();
            // string TypeOfStorageStructure = string.empty();
            // string nextSymbol = string.empty();
            // string KeywordOn = string.empty();
            // string KeywordOrder = string.empty();
            // string KeywordStorage = string.empty();
            // string KeyWordStructure = string.empty();

            // NameOfIndex = get next sybol from query

            /// MAKE NEW SERVICE FOR CHECKING IF IT IS A KEY WORD
            // CheckIfNameOfIndexIsNotKeyWord(NameOfIndex);
                // {
                        // If(symbol sent in = a key word)
                            // Throw syntax error (index name cannot be a key word, offending query, offending stage, offending symbol)
                // }
            /// END OF MAKE NEW SERVICE
            
            /// MAKE NEW SERVICE FOR CHECKING IF INDEX EXISTS
            // CheckIfIndexNameAlreadyExists(NameOfIndex);
                // {
                        // IList<string> name of indexs;
                        
                        // if (name of index sent in is the same as one in the list)
                            // Throw syntax error (index already exists, offending query, offending stage, offending symbol)
                // }
            /// END OF MAKE NEW SERVICE

            // nextSymbol = get next symbol from query;

            /// MAKE NEW SERVICE FOR CHECKING IF A SYMBOL IS A SPECIFIC KEYWORD
            // CheckIfSymbolIsAKeyWord(nextSymbol, string constant for "ON")
                // {
                        // if(the keyword sent in does not equal the expected keyword)
                            //  Throw error syntax error (Unexpected or missing keyword. (send in the expected keyword), offending query, offending stage, offending symbol)
                // }
            /// END MAKE NEW SERVICE

            // NameOfPrimaryRelation = get next symbol from query

            /// MAKE NEW SERVICE FOR CHECKING IF A RELATION EXISTS
            // EnsureRelationNameExists(NameOfPrimaryRelation);
                // {
                        // IList<string> name of all relations;
                        
                        // if(the list of relation names does not contain the name sent in)
                            // Throw syntax error (Relationship name does not exist, offending query, offending stage, offending symbol)
                // }
            /// END NEW SERVICE
            
            // relationshipObjectToAddIndexTo = get relationship object with NameOfPrimaryRelation

            // nextSymbol = get next symbol from query
            // CheckIfSymbolIsAKeyWord(nextSymbol, string constant for "ORDER");


            /*
             * This part ensures that IF IT HAS A ( then it will take all the arguments
             *  IF IT DOES NOT HAVE A ( then it will just read one
             *  Then it will store all of these strings into the list of strings
             *  Later this list of strings will be used to create an index
             */

            // nextSymbol = get next symbol from query
            // if(nextSymbol == '(')
                // nextSymbol = get next symbol

                // do
                    // EnsureIsNotKeyWord(nextSymbol);

                    /// NEW SERVICE ENSURE ATTRIBUTE NAME EXISTS IN THE RELATION
                    // EnsureAttributeNameExistsInRelation(relationObjectToAddIndexTo.attributes, nextSymbol)
                        // {
                                // if(relationObjectAttributesSent.names contains does not contain next symbol)
                                    // throw syntax error (The relation does not contain that attribute, offending query, offending stage, offending symbol);
                        // }
                    /// END NEW SERVICE
                    
                    // List of attribute names add next symbol
                    
                    // nextSymbol = get next symbol
                // while(nextSymbol != ')')
            // else
                // EnsureIsNotKeyWord(nextSymbol);
                // EnsureAttributeNameExistsInRelation(relationObjectToAddIndexTo.attributes, nextSymbol)
                // List of attribute names add nextSymbol

            /*
             * END OF SEGMENT
             * List of attribute names ensured to be true
             */

            /* TODO 
             * This is too much for one night.  Will do other parts first.
             * Remaining: By this point all parts should be confirmed valid entries,
             *  now it only remains that their tree and hash storage are done correctly.
             */

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
    }
}
