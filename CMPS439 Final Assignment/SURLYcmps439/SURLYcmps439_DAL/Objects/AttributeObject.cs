using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.Objects
{
    public class AttributeObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }

        public AttributeObject()
        {
            Name = string.Empty;
            Type = string.Empty;
            Size = 0;
        }
    }
}
