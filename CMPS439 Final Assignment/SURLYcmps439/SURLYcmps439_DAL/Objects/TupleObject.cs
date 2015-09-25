using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Objects
{
    public class TupleObject
    {
        public IList<TupleCellObject> TupleCells { get; set; }

        public TupleObject()
        {
            TupleCells = new List<TupleCellObject>();
        }
    }
}
