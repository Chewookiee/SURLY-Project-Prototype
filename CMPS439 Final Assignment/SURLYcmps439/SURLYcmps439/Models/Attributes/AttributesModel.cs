using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Models.Attributes
{
    public class AttributesModel
    {
        /*
        public int id { get; set; }
        public Keys? KeyOfAttribute { get; set; }
        public string NameOfAttribute { get; set; }
        public AttributeType IsTypeOf { get; set; }
        public int value1 { get; set; }
         * */

        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }

        public AttributesModel()
        {
            Name = string.Empty;
            Type = string.Empty;
            Size = 0;
        }
    }
}