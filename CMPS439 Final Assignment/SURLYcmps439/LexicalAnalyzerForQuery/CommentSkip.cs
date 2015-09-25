using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    internal static class CommentSkip
    {
        internal static void ConfirmSymbolIsOpenComment()
        {
            QueryParser.IncrementCharIndexAndCheck();

            switch (QueryParser.Query[QueryParser.CharIndex])
            {
                case '*':
                    break;
                default:
                    ErrorReporting.SlashWasNotFollowedByAnAxtrix(QueryParser.Query[QueryParser.CharIndex].ToString());
                    break;
            }
        }

        private static void SkipAllSymbolsUntilCloseComment()
        {
            bool skipping = true;

            while(skipping)
            {
                QueryParser.IncrementCharIndexAndCheck();

                switch(QueryParser.Query[QueryParser.CharIndex])
                {
                    case '*':
                        skipping = ConfirmCloseComment();
                        break;
                    default:
                        break;
                }
            }
        }

        private static bool ConfirmCloseComment()
        {
            bool IsCommentComplete = false;

            QueryParser.IncrementCharIndexAndCheck();

            switch(QueryParser.Query[QueryParser.CharIndex])
            {
                case '/':
                    IsCommentComplete = true;
                    break;
                default:
                    break;
            }

            return !IsCommentComplete;
        }
    }
}
