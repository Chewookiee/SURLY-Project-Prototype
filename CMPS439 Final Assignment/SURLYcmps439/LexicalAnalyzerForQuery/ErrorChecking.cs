using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    class ErrorChecking
    {
        internal static void IsQueryEmpty()
        {
            if (QueryParser.Query.Equals(string.Empty) || QueryParser.Query.Equals(""))
            {
                ErrorReporting.EmptyQuery();
            }
        }

        internal static bool NotAtEndOfString(int index)
        {
            try
            {
                char temp = QueryParser.Query[index];
            }
            catch
            {
                ErrorReporting.UnexpectedEndOfQuery();
            }

            return true;
        }

        internal static bool DoesStringContainAnyMoreCharacters()
        {
            bool onlySpancesRemain = true;

            try
            {
                bool checking = true;
                int tempIndex = QueryParser.CharIndex;

                while (checking)
                {
                    switch (QueryParser.Query[tempIndex])
                    {
                        case ' ':
                            break;
                        default:
                            onlySpancesRemain = false;
                            break;
                    }
                }
            }
            finally
            {

            }

            return onlySpancesRemain;
        }
    }
}
