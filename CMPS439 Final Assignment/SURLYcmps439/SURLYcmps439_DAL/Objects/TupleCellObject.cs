using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Objects
{
    public class TupleCellObject
    {
        public string Value { get; set; }
        public AttributeObject AttributeAssociatedWithCell { get; set; }

        public TupleCellObject()
        {
            Value = string.Empty;
            AttributeAssociatedWithCell = new AttributeObject();
        }
    }
}
