using SURLYcmps439.Models.Attributes;
using SURLYcmps439.Models.Tuples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Relations
{
    public class RelationsModel
    {
        public IList<AttributesModel> RelationAttributes { get; set; }
        public IList<TuplesModel> RelationsTuples { get; set; }

        [DisplayName("Relations Name")]
        public string NameOfRelation { get; set; }

        public RelationsModel()
        {
            RelationAttributes = new List<AttributesModel>();
            RelationsTuples = new List<TuplesModel>();
            NameOfRelation = string.Empty;
        }
    }
}