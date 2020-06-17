using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JEVEGA_Us_Cliniic;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class DiagnosticExamsController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();

        // GET: DiagnosticExams
        public ActionResult Index()
        {
            return View(db.DiagnosticExams.ToList());
        }

        // GET: DiagnosticExams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosticExam diagnosticExam = db.DiagnosticExams.Find(id);
            if (diagnosticExam == null)
            {
                return HttpNotFound();
            }
            return View(diagnosticExam);
        }

        // GET: DiagnosticExams/Create
        public ActionResult Create()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                ViewBag.ExamCategoryTypes = new SelectList(db.DiagnosticExamCategories, "Id", "CategoryName");
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

            return View();
        } //--

        // POST: DiagnosticExams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamCategory,ExamName,ClinicBasePrice,DoctorBasePrice")] DiagnosticExam diagnosticExam)
        {
            if (ModelState.IsValid)
            {
                db.DiagnosticExams.Add(diagnosticExam);
                db.SaveChanges();
                CreateExamPriceHistory(diagnosticExam);
                return RedirectToAction("Index");
            }
            else
            {
                //*** if model state is not valid ... re-create list 
                ViewBag.ExamCategoryTypes = new SelectList(db.DiagnosticExamCategories, "Id", "CategoryName");
            }

            return View(diagnosticExam);
        }

        // GET: DiagnosticExams/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ExamCategoryTypes = new SelectList(db.DiagnosticExamCategories, "Id", "CategoryName");

            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            DiagnosticExam diagnosticExam = db.DiagnosticExams.Find(id);
            if (diagnosticExam == null)
            {   return HttpNotFound();      }

            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(diagnosticExam);
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        // POST: DiagnosticExams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamCategory,ExamName,ClinicBasePrice,DoctorBasePrice")] DiagnosticExam diagnosticExam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosticExam).State = EntityState.Modified;
                db.SaveChanges();
                CreateExamPriceHistory(diagnosticExam);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ExamCategoryTypes = new SelectList(db.DiagnosticExamCategories, "Id", "CategoryName");
            }

            return View(diagnosticExam);
        } //--

        // GET: DiagnosticExams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiagnosticExam diagnosticExam = db.DiagnosticExams.Find(id);
            if (diagnosticExam == null)
            {
                return HttpNotFound();
            }
            return View(diagnosticExam);
        }

        // POST: DiagnosticExams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiagnosticExam diagnosticExam = db.DiagnosticExams.Find(id);
            db.DiagnosticExams.Remove(diagnosticExam);
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

        public void CreateExamPriceHistory(DiagnosticExam diagnosticExam)
        {
            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Insert ExamPriceHistory(ExamCategory, ExamName, ClinicBasePrice, DoctorBasePrice, PriceDate, ExamNameId) Values(@examcat, @examname, @clprice, @docprice, @date, @examname_id)";

                sqlCmd.Parameters.AddWithValue("@examcat", diagnosticExam.ExamCategory);
                sqlCmd.Parameters.AddWithValue("@examname", diagnosticExam.ExamName);
                sqlCmd.Parameters.AddWithValue("@clprice", diagnosticExam.ClinicBasePrice);
                sqlCmd.Parameters.AddWithValue("@docprice", diagnosticExam.DoctorBasePrice);
                sqlCmd.Parameters.AddWithValue("@date", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@examname_id", diagnosticExam.Id);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
        } //--
    }
}
