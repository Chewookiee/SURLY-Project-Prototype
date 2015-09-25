using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerForQuery
{
    internal class StringReturn
    {
        internal static string RecordStringToReturn()
        {
            string StringToReturn = string.Empty;

            bool reading = true;

            while(reading)
            {
                QueryParser.IncrementCharIndexAndCheck();

                switch(QueryParser.Query[QueryParser.CharIndex])
                {
                    case '\'':
                        reading = false;
                        break;
                    default:
                        StringToReturn += (QueryParser.Query[QueryParser.CharIndex]).ToString();
                        break;
                }
            }

            return StringToReturn;
        }
    }
}
