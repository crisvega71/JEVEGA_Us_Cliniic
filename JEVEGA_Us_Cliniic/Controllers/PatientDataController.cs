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
    public class PatientDataController : Controller
    {
        private JEVEGA_USDB_Entities db = new JEVEGA_USDB_Entities();
        public static string patient_id_number = "";


        [Authorize]
        // GET: PatientData
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.PatientDatas.ToList());
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        // GET: PatientData/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientDatas.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaritalStatusList = new SelectList(db.MaritalStatus, "StatusCode", "Status");
            List<SelectListItem> genderList = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem { Text = "Male", Value = "M" };
            genderList.Add(item1);
            SelectListItem item2 = new SelectListItem { Text = "Female", Value = "F" };
            genderList.Add(item2);
            ViewBag.GenderList = genderList;

            return View(patientData);
        }

        // GET: PatientData/Create
        public ActionResult Create()
        {
            ViewBag.MaritalStatusList = new SelectList(db.MaritalStatus, "StatusCode", "Status");
            //ViewBag.GenderList = new SelectList(db.Genders, "GenderCode", "Gender1");

            List<SelectListItem> genderList = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem { Text = "Male", Value = "M" };
            genderList.Add(item1);
            SelectListItem item2 = new SelectListItem { Text = "Female", Value = "F" };
            genderList.Add(item2);
            ViewBag.GenderList = genderList;

            return View();
        }

        // POST: PatientData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Patient_Id,Lastname,Firstname,Age,Sex,Address,Email,Phone,Status,LMP")] PatientData patientData)
        {
            if (ModelState.IsValid)
            {
                db.PatientDatas.Add(patientData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.MaritalStatusList = new SelectList(db.MaritalStatus, "StatusCode", "Status");

                List<SelectListItem> genderList = new List<SelectListItem>();
                SelectListItem item1 = new SelectListItem { Text = "Male", Value = "M" };
                genderList.Add(item1);
                SelectListItem item2 = new SelectListItem { Text = "Female", Value = "F" };
                genderList.Add(item2);
                ViewBag.GenderList = genderList;
            }

            return View(patientData);
        }

        // GET: PatientData/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientDatas.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaritalStatusList = new SelectList(db.MaritalStatus, "StatusCode", "Status");
            List<SelectListItem> genderList = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem { Text = "Male", Value = "M" };
            genderList.Add(item1);
            SelectListItem item2 = new SelectListItem { Text = "Female", Value = "F" };
            genderList.Add(item2);
            ViewBag.GenderList = genderList;

            patient_id_number = patientData.Patient_Id.ToString();
            return View(patientData);
        }

        // POST: PatientData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Patient_Id,Lastname,Firstname,Age,Sex,Address,Email,Phone,LoginPassword,Status,LMP")] PatientData patientData)
        {
            patientData.Patient_Id = patient_id_number;

            if (ModelState.IsValid)
            {
                db.Entry(patientData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                ViewBag.MaritalStatusList = new SelectList(db.MaritalStatus, "StatusCode", "Status");
                List<SelectListItem> genderList = new List<SelectListItem>();
                SelectListItem item1 = new SelectListItem { Text = "Male", Value = "M" };
                genderList.Add(item1);
                SelectListItem item2 = new SelectListItem { Text = "Female", Value = "F" };
                genderList.Add(item2);
                ViewBag.GenderList = genderList;
            }
            return View(patientData);
        }

        // GET: PatientData/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientDatas.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }
            return View(patientData);
        }

        // POST: PatientData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PatientData patientData = db.PatientDatas.Find(id);
            db.PatientDatas.Remove(patientData);
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

        public ActionResult SearchPatientData()
        {
            string lastName = Request.QueryString["ln"];
            string firstName = Request.QueryString["fn"];

            if (lastName.Length > 0 && firstName.Length == 0)
            {
                return View(db.PatientDatas.Where(p => p.Lastname.ToUpper().Contains(lastName.ToUpper())).ToList());
            }

            if(lastName.Length == 0 && firstName.Length > 0)
            {
                return View(db.PatientDatas.Where(p => p.Firstname.ToUpper().Contains(firstName.ToUpper())).ToList());
            }


            return View(db.PatientDatas.Where(p => p.Lastname.ToUpper().Contains(lastName.ToUpper()) || p.Firstname.ToUpper().Contains(firstName.ToUpper())).ToList());

        } //--

    }
}
