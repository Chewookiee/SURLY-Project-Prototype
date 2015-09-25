using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SURLYcmps439.Services
{
    public class XMLActions
    {
    }

    public static class XMLWrite
    {
        public static void WriteXML()
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(IList<RelationObject>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//DATABASE.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, InternalDatabase.Relations);
            file.Close();
        }
    }
}