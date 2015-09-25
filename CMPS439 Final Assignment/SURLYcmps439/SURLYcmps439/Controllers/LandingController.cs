using SURLYcmps439_DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SURLYcmps439_DAL.Objects;
using SURLYcmps439.Models.Relations;
using SURLYcmps439;
using SURLYcmps439.DatabaseAccess;


namespace CMPS439ProjectSURLY.Controllers
{
    public class LandingController : Controller
    {
        // GET: CreateTable
        public ActionResult Index()
        {
            DataHydrator dataHydrator = new DataHydrator();

            IList<RelationsModel> AllRelationsWithNoTuples = dataHydrator.GetAllRelationModelFromDatabaseWithoutTuplesForView();

            return View(AllRelationsWithNoTuples);
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Seed()
        {
            return View();
        }

        public ActionResult Examples()
        {
            return View();
        }

        public ActionResult ErrorHandling()
        {
            return View();
        }
        /*
        public ActionResult DisplayTable(string name)
        {
            Singleton singleton = Singleton.getInstance();

            RelationsModel TableToDisplayTuples = singleton.Database.FirstOrDefault(x => x.NameOfRelation.Equals(name));

            DisplayTablesColsAndRowsViewModel dispTable = new DisplayTablesColsAndRowsViewModel();

            dispTable.NameOfTable = TableToDisplayTuples.NameOfRelation;
            dispTable.Attributes = TableToDisplayTuples.RelationAttributes;
            dispTable.Touples = TableToDisplayTuples.RelationsTuples;
            
            
            singleton.TuplesForDisplay = dispTable.Touples;
            singleton.SetTupleCellListForView1(id);
            

            return View(dispTable);
        }

        [HttpGet]
        public ActionResult RemoveTable(int name)
        {
            Singleton singleton = Singleton.getInstance();

            RelationsModel TableToDelete = singleton.Database.FirstOrDefault(x => x.NameOfRelation.Equals(name));

            return View(TableToDelete);
        }

        [HttpPost]
        public ActionResult RemoveTable(RelationsModel TableToDelete)
        {
            throw new NotImplementedException("NotImplemented: CMPS439ProjectSURLY.Controllers.DatabaseControllers.RemoveTable");

            TableRemovalService service = new TableRemovalService();

            service.RemoveTable(TableToDelete);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemoveTableTuples(int name)
        {
            Singleton singleton = Singleton.getInstance();

            RelationsModel TableToDelete = singleton.Database.FirstOrDefault(x => x.NameOfRelation.Equals(name));

            return View(TableToDelete);
        }

        [HttpPost]
        public ActionResult RemoveTableTuples(RelationsModel TableToDelete)
        {
            TableRemovalService service = new TableRemovalService();

            service.RemoveAllTuplesFromTable(TableToDelete);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateTable()
        {
            throw new NotImplementedException("NotImplemented: CMPS439ProjectSURLY.Controllers.DatabaseControllers.CreateTable");

            return View();
        }

        [HttpPost]
        public ActionResult CreateTable(CreateRelationViewModel newTable)
        {
            throw new NotImplementedException("NotImplemented: CMPS439ProjectSURLY.Controllers.DatabaseControllers.CreateTable");

            
            RelationsModel realTable = new RelationsModel();
            Singleton singleton = Singleton.getInstance();

            realTable.NameOfRelation = newTable.NameOfTable;

            
            RelationsModel temp = singleton.Database.FirstOrDefault(x => x.NameOfRelation.Equals(realTable.NameOfRelation));

            if (temp == null)
            {
                singleton.Database.Add(realTable);
            }
            else
            {
                singleton.Database.Remove(temp);
                singleton.Database.Add(realTable);
            }

            return RedirectToAction("Index");
        }
             
        [HttpGet]
        public ActionResult AddAttributeToTable(string name)
        {
            AddNewAttributeToTableViewModel newAttribute = new AddNewAttributeToTableViewModel();

            newAttribute.id = DatabaseStatic.attributeCount++;
            newAttribute.idOfTableToAddThisToo = id;

            ViewBag.items = new[]
            {
                new SelectListItem { Value = "1", Text = "Int" },
                new SelectListItem { Value = "2", Text = "Char" },
                new SelectListItem { Value = "3", Text = "Varchar" }
            }; 

            return View(newAttribute);
        }

        [HttpPost]
        public ActionResult AddAttributeToTable(AddNewAttributeToTableViewModel newAttribute)
        {
            Singleton singleton = Singleton.getInstance();
            RelationsModel TableToAddAttributeTo = singleton.Database.FirstOrDefault(x => x.id == newAttribute.idOfTableToAddThisToo);

            AttributesModel realAttribute = new AttributesModel();

            realAttribute.id = DatabaseStatic.attributeCount++;
            realAttribute.NameOfAttribute = newAttribute.nameOfAttribute;

            if(newAttribute.typeOfAttribute == 1)
            {
                realAttribute.IsTypeOf = AttributeType.INTEGER;
                realAttribute.value1 = newAttribute.value1;
            }
            else if (newAttribute.typeOfAttribute == 2)
            {
                realAttribute.IsTypeOf = AttributeType.CHAR;
                realAttribute.value1 = newAttribute.value1;
            }
            else if (newAttribute.typeOfAttribute == 3)
            {
                realAttribute.IsTypeOf = AttributeType.VARCHAR;
            }

            if(singleton.Database.FirstOrDefault(x => x.id == newAttribute.idOfTableToAddThisToo).Attributes.FirstOrDefault(a => a.NameOfAttribute == realAttribute.NameOfAttribute) == null)
            {
                TableToAddAttributeTo.Attributes.Add(realAttribute);
            }
            else
            {
                // THORW ERROR
            }
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddTupleToTable(string name)
        {
            AddNewTupleViewModel newTuple = new AddNewTupleViewModel();
            Singleton singleton = Singleton.getInstance();

            RelationsModel CurrentTable = singleton.Database.FirstOrDefault(x => x.id == id);

            foreach(AttributesModel item in CurrentTable.Attributes)
            {
                AddNewTupleCellViewModel t = new AddNewTupleCellViewModel();

                t.AttributeName = item.NameOfAttribute;
                t.AttributeTypeName = TypeConverstionStatic.ConvertTypeToString(item.IsTypeOf);
                t.MyAttribute = item;
                
                newTuple.listOfCells.Add(t);
                StorageStatic.ListOfAttributesStorage.Add(item);
            }

            DatabaseStatic.TableToSave = CurrentTable;
                
            return View(newTuple.listOfCells);
        }

        [HttpPost]
        public ActionResult AddTupleToTable(IEnumerable<CMPS439ProjectSURLY.ViewModels.AddNewTupleCellViewModel> Whatever)
        {
           
        }

        public ActionResult TryToHitDB()
        {
            throw new NotImplementedException("NotImplemented: CMPS439ProjectSURLY.Controllers.DatabaseControllers.createtable");

            
            IList<CMPS439ProjectSURLY.Models.Tuples.TuplesModel> rows = new List<CMPS439ProjectSURLY.Models.Tuples.TuplesModel>();
            Singleton singleton = Singleton.getInstance();
            rows = singleton.GetRowsFromTable(1);


            return View();
        }
         */
    }
}