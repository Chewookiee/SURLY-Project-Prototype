using SURLYcmps439.Models;
using SURLYcmps439.Services;
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
    public class QueryController : Controller
    {
        // GET: Query
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Landing", null);
        }

        // GET: Query/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Query/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Query/Create
        [HttpPost]
        public ActionResult Create(QueryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResolveQuery resolveQuery = new ResolveQuery();

                    RelationObject isAnExpression = resolveQuery.Load(model.QueryInput);

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
                catch(Exception e)
                {
                    InternalDatabase.RevertDatabaseToBackup();

                    string[] errorSplitByPipe = e.Message.Split('|');

                    foreach(string errorMessage in errorSplitByPipe)
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                }
                
            }

            return View(model);
        }

        // GET: Query/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Query/Edit/5
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

        // GET: Query/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Query/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
