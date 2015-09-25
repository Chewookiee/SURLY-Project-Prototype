using SURLYcmps439.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Relations
{
    public class RelationshipsModel
    {
        public RelationsModel TableConnectedTo { get; set; }

        public IList<AttributesModel> ForeignKeys { get; set; }
    }
}