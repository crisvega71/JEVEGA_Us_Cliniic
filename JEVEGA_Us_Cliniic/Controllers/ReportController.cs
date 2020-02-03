using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        
        private JEVEGA_UsDbEntities dbWizEnt = new JEVEGA_UsDbEntities();

        // GET: Report
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                ViewBag.ReportTemplateCategories = new SelectList(dbWizEnt.DiagnosticExams, "Id", "ExamName");
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {


            return RedirectToAction("Index");
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
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

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
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
