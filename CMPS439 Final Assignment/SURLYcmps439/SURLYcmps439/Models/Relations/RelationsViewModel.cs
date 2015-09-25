﻿using SURLYcmps439.Models.Attributes;
using SURLYcmps439.Models.Tuples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Relations
{
    public class RelationToDisplayAllInformationViewModel
    {
        public IList<AttributesModel> RelationAttributes { get; set; }
        public IList<TuplesModel> RelationsTuples { get; set; }

        [DisplayName("Relations Name")]
        public string NameOfRelation { get; set; }

        public RelationToDisplayAllInformationViewModel(IList<AttributesModel> relationAttributes, IList<TuplesModel> relationsTuples, string nameOfRelation)
        {
            this.RelationAttributes = relationAttributes;
            this.RelationsTuples = relationsTuples;
            this.NameOfRelation = nameOfRelation;
        }
    }
}