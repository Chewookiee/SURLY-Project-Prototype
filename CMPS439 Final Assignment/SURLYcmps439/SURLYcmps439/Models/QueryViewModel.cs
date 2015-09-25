using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SURLYcmps439.Models
{
    public class QueryViewModel
    {
        [DataType(DataType.MultilineText)]
        [DisplayName("Query Input")]
        public string QueryInput { get; set; }
    }
}