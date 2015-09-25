using SURLYcmps439_DAL.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    public static class QueryParser
    {
        public static string Query { get; private set; }
        public static string[] QuerySplitBySlashNRlashN { get; private set; }

        internal static int CharIndex { get; set; }
        internal static int PeakIndex { get; set; }

        public static void LoadQuery(string query)
        {
            Query = string.Empty;


            Query = query.Replace("\t", " ");

            QuerySplitBySlashNRlashN = Query.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for(int i = 0; i < QuerySplitBySlashNRlashN.Length; i++)
            {
                QuerySplitBySlashNRlashN[i] += "  ";
            }

            Query = Query.Replace("\n", " ");
            Query = Query.Replace("\r", " ");

            Query = Query.Trim();

            CharIndex = 0;

            ErrorChecking.IsQueryEmpty();
        }

        public static string FindNextSymbol()
        {
            string NextSymbol = string.Empty;
            bool Scanning = true;

            while(Scanning)
            {
                switch(Query[CharIndex])
                {
                    case '/':
                        CommentSkip.ConfirmSymbolIsOpenComment();
                        break;
                    case ',':
                        break;
                    case '\'':
                        Scanning = false;
                        NextSymbol = StringReturn.RecordStringToReturn();
                        break;
                    case '(':
                        Scanning = false;
                        NextSymbol = "(";
                        break;
                    case ')':
                        Scanning = false;
                        NextSymbol = ")";
                        break;
                    case '=':
                        Scanning = false;
                        NextSymbol = "=";
                        break;
                    case '*':
                        Scanning = false;
                        NextSymbol = "*";
                        break;
                    case '>':
                        Scanning = false;
                        NextSymbol = ">";
                        if (FollowedByEqualSign.IsFollowedByEqualSign())
                        {
                            NextSymbol += "=";
                        }
                        NextSymbol = ">=";
                        break;
                    case '<':
                        Scanning = false;
                        NextSymbol = "<";
                        if (FollowedByEqualSign.IsFollowedByEqualSign())
                        {
                            NextSymbol += "=";
                        }
                        break;
                    case '~':
                        if (FollowedByEqualSign.IsFollowedByEqualSign())
                        {
                            NextSymbol += "~=";
                            Scanning = false;
                        }
                        else
                        {
                            ErrorReporting.WasNotFollowedByAnEqualSign("~");
                        }
                        break;
                    case ';':
                        Scanning = false;
                        NextSymbol = ";";
                        break;
                    case ' ':
                        break;
                    case '\\':
                        // to skip \t for tabs
                        IncrementCharIndexAndCheck();
                        break;
                    default:
                        Scanning = false;
                        NextSymbol = RetriveSymbol.ReturnSymbol();
                        break;
                }

                if(Scanning)
                {
                    IncrementCharIndexAndCheck();
                }
                else
                {
                    CharIndex++;
                }
            }

            return NextSymbol;
        }

        public static bool AreSymbolsRemaining()
        {
            PeakIndex = CharIndex;
            bool isAtEndOfQuery = true;

            try
            {
                ErrorChecking.NotAtEndOfString(PeakIndex);
            }
            catch(Exception e)
            {
                isAtEndOfQuery = false;
            }

            return isAtEndOfQuery;
        }

        public static char PeakNextChar()
        {
            char characterFound;

            PeakIndex = CharIndex;
            ErrorChecking.NotAtEndOfString(PeakIndex);

            characterFound = Query[PeakIndex];

            return Query[PeakIndex];
        }

        public static void IncrementCharIndexAndCheck()
        {
            CharIndex++;
            ErrorChecking.NotAtEndOfString(CharIndex);
        }

        public static int CurrentLineNumberOfQueryThatIsExecuting()
        {
            int index = IndexOfCurrentLine();

            int lineNumber = index + 1;

            return lineNumber;
        }

        public static string CurrentLineOfQueryThatIsExecuting()
        {
            int index = IndexOfCurrentLine();

            return QuerySplitBySlashNRlashN[index];
        }

        private static int IndexOfCurrentLine()
        {
            int TempIndex = 0;
            int currentIndexOfStringArray = -1;

            do
            {
                currentIndexOfStringArray++;
                TempIndex += QuerySplitBySlashNRlashN[currentIndexOfStringArray].Length;
            }
            while (CharIndex > TempIndex);

            return currentIndexOfStringArray;
        }
    }
}
