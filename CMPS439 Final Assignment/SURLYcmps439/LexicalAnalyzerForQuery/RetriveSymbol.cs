using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    internal static class RetriveSymbol
    {
        internal static string ReturnSymbol()
        {
            bool reading = true;

            string SymbolToReturn = string.Empty;
            SymbolToReturn = QueryParser.Query[QueryParser.CharIndex].ToString();

            while(reading)
            {
                QueryParser.IncrementCharIndexAndCheck();
                char curChar = QueryParser.Query[QueryParser.CharIndex];

                if((curChar >= 'a' && curChar <= 'z') ||
                    (curChar >= 'A' && curChar <= 'Z') ||
                    (curChar >= '0' && curChar <= '9') ||
                    (curChar == ':') || (curChar == '-') || (curChar == '_'))
                {
                    SymbolToReturn += curChar.ToString();
                }
                else
                {
                    QueryParser.CharIndex--;
                    reading = false;
                }
            }

            return SymbolToReturn;
        }

        private static void DecrementCharIndex()
        {
            QueryParser.CharIndex--;
        }
    }
}
