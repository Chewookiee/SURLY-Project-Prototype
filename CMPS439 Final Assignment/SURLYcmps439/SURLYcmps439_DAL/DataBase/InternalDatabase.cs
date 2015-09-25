using SURLYcmps439_DAL.ErrorManager;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.DataBase
{
    // TODO
    // Make a hydrator for everything

    static public class InternalDatabase
    {
        public static IList<RelationObject> Relations { get; internal set; }
        public static RelationObject TempRelatinos { get; set; }

        public static IList<RelationObject> BackupRelations { get; set; }

        public static IList<RelationObject> GetAllRelations()
        {
            return Relations;
        }

        public static RelationObject GetRelationByName(string nameOfRelation)
        {
            RelationObject relationToReturn = Relations.FirstOrDefault(r => r.NameOfRelation.Equals(nameOfRelation));

            return relationToReturn;
        }

        public static IList<string> GetAllRelationNames()
        {
            IList<string> AllRelationNames = new List<string>();

            if (Relations != null)
            {
                foreach (RelationObject relation in Relations)
                {
                    AllRelationNames.Add(relation.NameOfRelation);
                }
            }

            return AllRelationNames;
        }

        public static void DeleteRelation(RelationObject relationToDelete)
        {
            relationToDelete = Relations.FirstOrDefault(r => r == relationToDelete);

            relationToDelete.RelationsTuples = new List<TupleObject>();
        }

        public static void DestroyRelation(RelationObject relationToDestroy)
        {
            Relations.Remove(relationToDestroy);
        }

        public static void AddRelationToDatabase(RelationObject relationObjectToAddToDatabase)
        {
            if(Relations == null)
            {
                Relations = new List<RelationObject>();
            }

            Relations.Add(relationObjectToAddToDatabase);

            Console.Write("Relation " + relationObjectToAddToDatabase.NameOfRelation + " added to database");
        }

        public static void JUSTTOCHECK()
        {
            int temp = 0;
        }

        public static void BackupDatabase()
        {
            InternalDatabase.BackupRelations = new List<RelationObject>();

            if (InternalDatabase.Relations != null)
            {
                foreach (RelationObject relation in InternalDatabase.Relations)
                {
                    InternalDatabase.BackupRelations.Add(relation);
                }
            }
        }

        public static void RevertDatabaseToBackup()
        {
            InternalDatabase.Relations = new List<RelationObject>();

            if (InternalDatabase.Relations != null)
            {
                foreach (RelationObject relation in InternalDatabase.BackupRelations)
                {
                    InternalDatabase.Relations.Add(relation);
                }
            }
        }
    }
}
