using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Tuples
{
    public class TuplesModel
    {
        public TuplesModel()
        {
            TupleCells = new List<TupleCellModel>();
        }

        public virtual IList<TupleCellModel> TupleCells { get; set; }
    }
}