using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD.Services.Attribute
{
    internal class AttributeServices
    {
        internal AttributeObject CreateCharAttribute(string NameOfAttribute, string SizeOfAttribute)
        {
            AttributeObject curObject = new AttributeObject();

            curObject.Type = AttributeConstants.CHAR;

            curObject = LoadAttribute(curObject, NameOfAttribute, SizeOfAttribute);

            return curObject;
        }

        internal AttributeObject CreateNumAttribute(string NameOfAttribute, string SizeOfAttribute)
        {
            AttributeObject curObject = new AttributeObject();

            curObject.Type = AttributeConstants.NUM;

            curObject = LoadAttribute(curObject, NameOfAttribute, SizeOfAttribute);

            return curObject;
        }

        private AttributeObject LoadAttribute(AttributeObject curObject, string NameOFAttribute, string SizeOFAttribute)
        {
            curObject.Name = NameOFAttribute;
            curObject.Size = Int32.Parse(SizeOFAttribute);

            return curObject;
        }
    }
}
