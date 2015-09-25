using SURLYcmps439.DatabaseAccess;
using SURLYcmps439.Models.Relations;
using SURLYcmps439_DAL;
using SURLYcmps439_DAL.DataBase;
using SURLYcmps439_DAL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SURLYcmps439.Controllers
{
    public class RelationController : Controller
    {
        // GET: Relation
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Landing");
        }

        // GET: Relation/Details/5
        public ActionResult Details(string name)
        {
            DataHydrator dataHydrator = new DataHydrator();

            RelationsModel baseModel = dataHydrator.GetRelationModelFromDatabaseLazyLoaded(name);

            RelationToDisplayAllInformationViewModel relationToDisplay = new RelationToDisplayAllInformationViewModel(baseModel.RelationAttributes,
                                                                                                                        baseModel.RelationsTuples,
                                                                                                                        baseModel.NameOfRelation);

            return View(relationToDisplay);
        }

        public ActionResult DetailsForTempRelation(string name)
        {
            DataHydrator dataHydrator = new DataHydrator();

            RelationObject tempTable = InternalDatabase.TempRelatinos;

            RelationsModel baseModel = dataHydrator.GetRelationModelFromTempRelatinoLazyLoaded(tempTable);

            RelationToDisplayAllInformationViewModel relationToDisplay = new RelationToDisplayAllInformationViewModel(baseModel.RelationAttributes,
                                                                                                                        baseModel.RelationsTuples,
                                                                                                                        baseModel.NameOfRelation);

            relationToDisplay.NameOfRelation = "*TEMPORARY";

            return View(relationToDisplay);
        }

        // GET: Relation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Relation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Relation/Delete/5
        public ActionResult Delete(string name)
        {
            DeleteRelationViewModel relationNameToDelete = new DeleteRelationViewModel();
            relationNameToDelete.name = name;
            return View(relationNameToDelete);
        }

        // POST: Relation/Delete/5
        [HttpPost]
        public ActionResult Delete(DeleteRelationViewModel relationToDelete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResolveQuery resolveQuery = new ResolveQuery();

                    RelationObject isAnExpression = resolveQuery.Load("DELETE " + relationToDelete.name + ";");

                    if (isAnExpression == null)
                    {
                        return RedirectToAction("Index", "Landing");
                    }
                    else
                    {
                        InternalDatabase.TempRelatinos = isAnExpression;

                        return RedirectToAction("DetailsForTempRelation", "Relation", isAnExpression.NameOfRelation);
                    }
                }
                catch (Exception e)
                {
                    InternalDatabase.RevertDatabaseToBackup();

                    string[] errorSplitByPipe = e.Message.Split('|');

                    foreach (string errorMessage in errorSplitByPipe)
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                }

            }

            return View(relationToDelete);
        }

        // GET: Relation/Delete/5
        public ActionResult Destroy(string name)
        {
            return View();
        }

        // POST: Relation/Delete/5
        [HttpPost]
        public ActionResult Destroy(DestroyRelationViewModel relationToDestroy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResolveQuery resolveQuery = new ResolveQuery();

                    RelationObject isAnExpression = resolveQuery.Load("DESTROY " + relationToDestroy.name + ";");

                    if (isAnExpression == null)
                    {
                        return RedirectToAction("Index", "Landing");
                    }
                    else
                    {
                        InternalDatabase.TempRelatinos = isAnExpression;

                        return RedirectToAction("DetailsForTempRelation", "Relation", isAnExpression.NameOfRelation);
                    }
                }
                catch (Exception e)
                {
                    InternalDatabase.RevertDatabaseToBackup();

                    string[] errorSplitByPipe = e.Message.Split('|');

                    foreach (string errorMessage in errorSplitByPipe)
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                }

            }

            return View(relationToDestroy);
        }
    }
}
