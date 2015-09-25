using SURLYcmps439_DAL.Constants;
using SURLYcmps439_DAL.CRUD.Services.Attribute;
using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.CRUD
{
    internal class AttributeCRUD
    {
        AttributeServices AttributeService = new AttributeServices();

        internal void Create()
        {

        }

        internal AttributeObject Create(string NameOfAttribute, string TypeOfAttribute, string SizeOfAttribute)
        {
            AttributeObject currentAttribute = new AttributeObject();

            switch(TypeOfAttribute)
            {
                case AllSpecialSymbols.CHAR:
                    currentAttribute = AttributeService.CreateCharAttribute(NameOfAttribute, SizeOfAttribute);
                    break;
                case AllSpecialSymbols.NUM:
                    currentAttribute = AttributeService.CreateNumAttribute(NameOfAttribute, SizeOfAttribute);
                    break;
                default:
                    ErrorHandlerSURLY.AttributeErrors.UnrecoginizedType(TypeOfAttribute);
                    break;
            }

            return currentAttribute;
        }

        internal void Read()
        {
            throw new NotImplementedException();
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }

        internal void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
