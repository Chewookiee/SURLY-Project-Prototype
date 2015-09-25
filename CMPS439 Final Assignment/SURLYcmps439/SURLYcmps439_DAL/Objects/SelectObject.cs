using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Objects
{
    public class SelectObject
    {
        public SelectObject()
        {
            ListOfOperationsSeparateByOrs = new List<OperationsObject>();
        }

        public IList<OperationsObject> ListOfOperationsSeparateByOrs { get; set; }
    }

    public class OperationsObject
    {
        public AttributeObject comparisonAttribute;
        public string comparisonOperation;
        public string comparisonValue;
    }
}
