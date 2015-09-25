using LexicalAnalyzerForQuery;
using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD.Services.Query
{
    internal class QueryServices
    {
        internal void ReadUntilSymbol(string symbol)
        {
            bool reading = true;
            string currentSymbol = string.Empty;

            while(reading)
            {
                currentSymbol = QueryParser.FindNextSymbol();

                if(currentSymbol.Equals(symbol))
                {
                    reading = false;
                }
            }
        }

        /// <summary>
        /// Reads the next symbol.  If it does not match the symbol sent in then a syntax error will be thrown.
        /// </summary>
        /// <param name="symbolExpected"> Symbol expected to be next. </param>
        internal void EnsureNextSymbolIsThere(string symbolExpected)
        {
            IList<string> symbolConveredToList = new List<string>();

            symbolConveredToList.Add(symbolExpected);

            EnsureNextSymbolIsThere(symbolConveredToList);
        }

        /// <summary>
        /// Reads the next symbol.  If it does not match one of the symbols sent in then a syntax error will be thrown.
        /// </summary>
        /// <param name="symbolsExpected"> Symbols expected to be next. </param>
        internal void EnsureNextSymbolIsThere(IList<string> symbolsExpected)
        {
            bool theSymbolThatIsToBeLookedForHasBeenFound = false;
            string currentSymbol = QueryParser.FindNextSymbol();
            string allSymbolsLookedForAsString = string.Empty;

            foreach (string item in symbolsExpected)
            {
                if(item.Equals(currentSymbol))
                {
                    theSymbolThatIsToBeLookedForHasBeenFound = true;
                    break;
                }

                allSymbolsLookedForAsString += " " + item + "";
            }

            if(theSymbolThatIsToBeLookedForHasBeenFound == false)
            {
                ErrorHandlerSURLY.QueryErrors.SpecificSymbolWasNotFound(currentSymbol, allSymbolsLookedForAsString);
            }
        }

        internal void ConfirmEndOfStatement()
        {
            EnsureNextSymbolIsThere(SymbolConstants.SimiColorn);
        }


        internal void EnsureCurrentSymbolIsNotAKeyWord(string curSymbol)
        {
            IList<string> allSymbols = AllSpecialSymbols.GetListOfAllSymbols();

            if (allSymbols.Contains(curSymbol))
            {
                ErrorHandlerSURLY.QueryErrors.UniqueNamesCannotBeASpecialSymbol(curSymbol);
            }
        }

        public void EnsureCurrentSymboldContainIllegalCharacters(string curSymbol)
        {
            foreach (char curChar in curSymbol.ToCharArray())
            {
                if((curChar >= 'a' && curChar <= 'z') ||
                        (curChar >= 'A' && curChar <= 'Z') ||
                        (curChar >= '0' && curChar <= '9') ||
                        (curChar == ':') || (curChar == '-') || (curChar == '_'))
                {

                }
                else
                {
                    ErrorHandlerSURLY.QueryErrors.ContainedIllegalAnCharacter(curSymbol);
                }
            }
        }

        public void EnsureCurrentSymbolIsEitherNumOrChar(string curSymbol)
        {
            switch(curSymbol)
            {
                case AllSpecialSymbols.CHAR:
                    break;
                case AllSpecialSymbols.NUM:
                    break;
                default:
                    ErrorHandlerSURLY.AttributeErrors.UnrecoginizedType(curSymbol);
                    break;
            }
        }

        public void ConfirmTypleIsANumber(string curSymbol)
        {
            int numericalValue;

            bool result = Int32.TryParse(curSymbol, out numericalValue);

            if (result)
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
