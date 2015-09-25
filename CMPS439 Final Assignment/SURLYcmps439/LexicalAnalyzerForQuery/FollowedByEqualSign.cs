using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    internal static class FollowedByEqualSign
    {
        internal static bool IsFollowedByEqualSign()
        {
            bool isFollowedByEqualSign = false;

            QueryParser.IncrementCharIndexAndCheck();

            switch (QueryParser.Query[QueryParser.CharIndex])
            {
                case '=':
                    isFollowedByEqualSign = true;
                    break;
                default:
                    isFollowedByEqualSign = false;
                    QueryParser.CharIndex--;
                    break;
            }

            return isFollowedByEqualSign;
        }
    }
}
