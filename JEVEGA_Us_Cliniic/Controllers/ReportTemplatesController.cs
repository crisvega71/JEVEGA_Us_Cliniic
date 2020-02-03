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
    public class ReportTemplatesController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();

        // GET: ReportTemplates
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.ExamReportTemplates.ToList());
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        // GET: ReportTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamReportTemplate examReportTemplate = db.ExamReportTemplates.Find(id);
            if (examReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(examReportTemplate);
        }

        // GET: ReportTemplates/Create
        public ActionResult Create()
        {
            ViewBag.ReportCategories = new SelectList(db.DiagnosticExams, "Id", "ExamName");

            return View();
        }

        // POST: ReportTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportCategory,ReportWriteUps,ReportName")] ExamReportTemplate examReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.ExamReportTemplates.Add(examReportTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ReportCategories = new SelectList(db.DiagnosticExams, "Id", "ExamName");
            }

            return View(examReportTemplate);
        }

        // GET: ReportTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamReportTemplate examReportTemplate = db.ExamReportTemplates.Find(id);
            if (examReportTemplate == null)
            {
                return HttpNotFound();
            }

            ViewBag.ReportCategories = new SelectList(db.DiagnosticExams, "Id", "ExamName");

            return View(examReportTemplate);
        } //--

        // POST: ReportTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportCategory,ReportWriteUps,ReportName")] ExamReportTemplate examReportTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examReportTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(examReportTemplate);
        }

        // GET: ReportTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamReportTemplate examReportTemplate = db.ExamReportTemplates.Find(id);
            if (examReportTemplate == null)
            {
                return HttpNotFound();
            }
            return View(examReportTemplate);
        }

        // POST: ReportTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamReportTemplate examReportTemplate = db.ExamReportTemplates.Find(id);
            db.ExamReportTemplates.Remove(examReportTemplate);
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
