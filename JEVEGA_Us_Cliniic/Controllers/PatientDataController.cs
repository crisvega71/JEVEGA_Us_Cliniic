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
    public class PatientDataController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();
        public static string patient_id_number = "";

        public string[] nameIndexChar = {"All", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y"};

        [Authorize]
        [OutputCache (Duration = 60)]
        // GET: PatientData
        public ActionResult Index()
        {
            string usertype;
            try
            {   usertype = Session["USER_TYPE"].ToString();     }
            catch (NullReferenceException ex)
            {
                ViewBag.NullUser = ex.Message.ToString();
                usertype = "Empty Username";
            }
            
            if (usertype == "1")   //-- Admin user ... 
            {
                //return View(db.PatientDatas.OrderBy(pd => pd.Lastname).ToList());
                //IEnumerable<PatientData> listPatients = db.PatientDatas.Where(pd => pd.Lastname.StartsWith("A")).OrderBy(l => l.Lastname).ToList();

                //.... return by default names where surname starts with "A" ....
                IEnumerable<PatientData> listPatients = db.PatientDatas.Where(p => p.Lastname.StartsWith("A")).OrderBy(l => l.Lastname).ToList();

                //IQueryable<PatientData> listPatients = db.PatientDatas.AsQueryable().OrderBy(l => l.Lastname);

                int countPatients = listPatients.Count();
                int oldestAge = (int)listPatients.Max(p => p.Age);
                PatientData oldestPatient = listPatients.FirstOrDefault(p => p.Age == 60);

                ViewBag.IndexNameChar = nameIndexChar;

                return View(listPatients);
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        [OutputCache (Duration = 60, VaryByParam = "id")]
        public ActionResult IndexSurname(string id)
        {
            IEnumerable<PatientData> listPatientsBySNameIndex;

            if (id == "All")
            {
                listPatientsBySNameIndex = db.PatientDatas.OrderBy(p => p.Lastname).ToList();
            }
            else {
                listPatientsBySNameIndex = db.PatientDatas.Where(p => p.Lastname.StartsWith(id)).OrderBy(p => p.Lastname).ToList();
            }

            ViewBag.IndexNameChar = nameIndexChar;
            ViewBag.IndexAlpha = id.ToString();

            return View(listPatientsBySNameIndex);
        }

        public ActionResult ListPatient()
        {
            if (Session["USER_TYPE"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            string usertype = Session["USER_TYPE"].ToString();
            if (usertype != "1")   //-- Admin user ... 
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            ViewBag.IndexNameChar = nameIndexChar;
            return View();

        } //--

        // GET: PatientData/Details/5
        public ActionResult Details(int? id)
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
                string patientId = patientData.Patient_Id;
                bool patientIdExist = checkPatientExamIdExist(patientId);

                if (patientIdExist)
                {
                    ViewBag.ErrorDuplicatePatientId = "Patient  Id No. " + patientId + " already existed!  Please enter a different Id number.";
                }
                else {
                    db.PatientDatas.Add(patientData);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
         
        // GET: PatientData/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToAction("Details/" + patientData.Id.ToString());
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


        public bool checkPatientExamIdExist(string patient_id)
        {
            bool exist = false;

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "select * from PatientData Where Patient_Id = @patient_id";
                sqlCmd.Parameters.AddWithValue("@patient_id", patient_id);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();

                SqlDataReader rdrPdata = sqlCmd.ExecuteReader();
                if (rdrPdata.HasRows)
                {   exist = true;   }
                else { exist = false; }

            }

            return exist;

        } //--
    }
}
