using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JEVEGA_Us_Cliniic;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class DiagnosticExamCategoriesController : Controller
    {
        private JEVEGA_USDB_Entities db = new JEVEGA_USDB_Entities();

        // GET: DiagnosticExamCategories
        public ActionResult Index()
        {
            Session["USER_TYPE"] = 1;
            
            return View(db.DiagnosticExamCategories.ToList());
        }

        // GET: DiagnosticExamCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosticExamCategory diagnosticExamCategory = db.DiagnosticExamCategories.Find(id);
            if (diagnosticExamCategory == null)
            {
                return HttpNotFound();
            }
            return View(diagnosticExamCategory);
        }

        // GET: DiagnosticExamCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiagnosticExamCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName")] DiagnosticExamCategory diagnosticExamCategory)
        {
            if (ModelState.IsValid)
            {
                db.DiagnosticExamCategories.Add(diagnosticExamCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnosticExamCategory);
        }

        // GET: DiagnosticExamCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosticExamCategory diagnosticExamCategory = db.DiagnosticExamCategories.Find(id);
            if (diagnosticExamCategory == null)
            {
                return HttpNotFound();
            }
            return View(diagnosticExamCategory);
        }

        // POST: DiagnosticExamCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName")] DiagnosticExamCategory diagnosticExamCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosticExamCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnosticExamCategory);
        }

        // GET: DiagnosticExamCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosticExamCategory diagnosticExamCategory = db.DiagnosticExamCategories.Find(id);
            if (diagnosticExamCategory == null)
            {
                return HttpNotFound();
            }
            return View(diagnosticExamCategory);
        }

        // POST: DiagnosticExamCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiagnosticExamCategory diagnosticExamCategory = db.DiagnosticExamCategories.Find(id);
            db.DiagnosticExamCategories.Remove(diagnosticExamCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
