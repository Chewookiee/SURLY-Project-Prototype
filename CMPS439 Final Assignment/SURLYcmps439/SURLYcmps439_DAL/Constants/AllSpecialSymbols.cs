using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Constants
{
    static class AllSpecialSymbols
    {
        internal const string RELATION = "RELATION";
        internal const string INDEX = "INDEX";
        internal const string ON = "ON";
        internal const string ORDER = "ORDER";
        internal const string STORAGE = "STORAGE";
        internal const string STRUCTURE = "STRUCTURE";
        internal const string DESTORY = "DESTORY";
        internal const string INSERT = "INSERT";
        internal const string DELETE = "DELETE";
        internal const string WHERE = "WHERE";
        internal const string INPUT = "INPUT";
        internal const string END_INPUT = "END_INPUT";
        internal const string PRINT = "PRINT";
        internal const string EXPORT = "EXPORT";
        internal const string IMPORT = "IMPORT";
        internal const string JOIN = "JOIN";
        internal const string AND = "AND";
        internal const string OR = "OR";
        internal const string OVER = "OVER";
        internal const string PROJECT = "PROJECT";
        internal const string SELECT = "SELECT";
        internal const string UNION = "UNION";
        internal const string SET_DIFFERENCE = "SET_DIFFERENCE";
        internal const string COPY = "COPY";
        internal const string ASSIGN = "ASSIGN";
        internal const string HEAP = "HEAP";
        internal const string HASH = "HASH";
        internal const string TREE = "TREE";
        internal const string CHAR = "CHAR";
        internal const string NUM = "NUM"; 
        internal const string OpentParenthases = "(";
        internal const string CloseParenthases = ")";
        internal const string SimiColorn = ";";
        internal const string Astrix = "*";

        
        internal static IList<string> GetListOfAllSymbols()
        {
            IList<string> listOfAllSymbols = new List<string>();
            
            listOfAllSymbols.Add(RELATION);
            listOfAllSymbols.Add(INDEX);
            listOfAllSymbols.Add(ON);
            listOfAllSymbols.Add(ORDER);
            listOfAllSymbols.Add(STORAGE);
            listOfAllSymbols.Add(STRUCTURE);
            listOfAllSymbols.Add(DESTORY);
            listOfAllSymbols.Add(INSERT);
            listOfAllSymbols.Add(DELETE);
            listOfAllSymbols.Add(WHERE);
            listOfAllSymbols.Add(INPUT);
            listOfAllSymbols.Add(END_INPUT);
            listOfAllSymbols.Add(PRINT);
            listOfAllSymbols.Add(EXPORT);
            listOfAllSymbols.Add(IMPORT);
            listOfAllSymbols.Add(JOIN);
            listOfAllSymbols.Add(AND);
            listOfAllSymbols.Add(OVER );
            listOfAllSymbols.Add(PROJECT);
            listOfAllSymbols.Add(SELECT);
            listOfAllSymbols.Add(UNION);
            listOfAllSymbols.Add(SET_DIFFERENCE);
            listOfAllSymbols.Add(COPY);
            listOfAllSymbols.Add(ASSIGN);
            listOfAllSymbols.Add(HEAP);
            listOfAllSymbols.Add(HASH);
            listOfAllSymbols.Add(TREE);
            listOfAllSymbols.Add(CHAR);
            listOfAllSymbols.Add(NUM);
            listOfAllSymbols.Add(OpentParenthases);
            listOfAllSymbols.Add(CloseParenthases);
            listOfAllSymbols.Add(SimiColorn);
            listOfAllSymbols.Add(Astrix);

            return listOfAllSymbols;
        }
    }
}
