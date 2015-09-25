using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Objects
{
    public class RelationObject
    {
        public IList<AttributeObject> RelationAttributes { get; set; }
        public IList<TupleObject> RelationsTuples { get; set; }

        public string NameOfRelation { get; set; }

        public RelationObject()
        {
            RelationAttributes = new List<AttributeObject>();
            RelationsTuples = new List<TupleObject>();
            NameOfRelation = string.Empty;
        }
    }
}
