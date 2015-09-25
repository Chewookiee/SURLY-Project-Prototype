using SURLYcmps439.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Tuples
{
    public class TupleCellModel
    {
        /*
        public int idOfCell { get; set; }
        public int idOfTuple { get; set; }
        public object TupleCellData { get; set; }
        //public AttributeType AttributeType { get; set; }
        public AttributesModel MyAttribute { get; set; }
         * */

        public virtual string Value { get; set; }
        public virtual AttributesModel AttributeAssociatedWithCell { get; set; }

    }
}