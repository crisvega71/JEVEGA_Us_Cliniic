using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JEVEGA_Us_Cliniic;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class PatientExamController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();
        UtilityHelper utHelp = new UtilityHelper();

        // GET: PatientExam
        public ActionResult Index()
        {
            if (Session["USER_TYPE"] == null)
            {
                return RedirectToAction("Login", "Users", new { @ReturnUrl = "/PatientExam/Index" });
            }

            string datelist = "";
            bool nyear = false;
            bool nmonth = false;

            string query_month = Request.QueryString["mo"];
            string query_year = Request.QueryString["yr"];

            int this_month;
            int this_year;
            if (query_month == null)
            {
                DateTime today = DateTime.Now;
                this_month = today.Month;
                this_year = today.Year;
            }
            else
            {   this_month = Convert.ToInt32(query_month);
                this_year = Convert.ToInt32(query_year);
            }

            List<SelectListItem> listOfYears = new List<SelectListItem>();
            if (this_year == 2019) { nyear = true; } else { nyear = false; }
            SelectListItem item1 = new SelectListItem { Text = "2019", Value = "2019", Selected=nyear };
            listOfYears.Add(item1);

            if (this_year == 2020) { nyear = true; } else { nyear = false; }
            SelectListItem item2 = new SelectListItem { Text = "2020", Value = "2020", Selected = nyear };
            listOfYears.Add(item2);

            if (this_year == 2021) { nyear = true; } else { nyear = false; }
            SelectListItem item3 = new SelectListItem { Text = "2021", Value = "2021", Selected = nyear };
            listOfYears.Add(item3);

            if (this_year == 2022) { nyear = true; } else { nyear = false; }
            SelectListItem item4 = new SelectListItem { Text = "2022", Value = "2022", Selected = nyear };
            listOfYears.Add(item4);

            if (this_year == 2023) { nyear = true; } else { nyear = false; }
            SelectListItem item5 = new SelectListItem { Text = "2023", Value = "2023", Selected = nyear };
            listOfYears.Add(item5);

            ViewBag.YearsList = listOfYears.ToList();
            //...............................................................................................

            List<SelectListItem> listOfMonths = new List<SelectListItem>();

            if (this_month == 1) { nmonth = true; datelist = "January"; } else { nmonth = false; }
            SelectListItem mo1 = new SelectListItem { Text = "January", Value = "1", Selected = nmonth };
            listOfMonths.Add(mo1);

            if (this_month == 2) { nmonth = true; datelist = "February"; } else { nmonth = false; }
            SelectListItem mo2 = new SelectListItem { Text = "February", Value = "2", Selected = nmonth };
            listOfMonths.Add(mo2);

            if (this_month == 3) { nmonth = true; datelist = "March"; } else { nmonth = false; }
            SelectListItem mo3 = new SelectListItem { Text = "March", Value = "3", Selected = nmonth };
            listOfMonths.Add(mo3);

            if (this_month == 4) { nmonth = true; datelist = "April"; } else { nmonth = false; }
            SelectListItem mo4 = new SelectListItem { Text = "April", Value = "4", Selected = nmonth };
            listOfMonths.Add(mo4);

            if (this_month == 5) { nmonth = true; datelist = "May"; } else { nmonth = false; }
            SelectListItem mo5 = new SelectListItem { Text = "May", Value = "5", Selected = nmonth };
            listOfMonths.Add(mo5);

            if (this_month == 6) { nmonth = true; datelist = "June"; } else { nmonth = false; }
            SelectListItem mo6 = new SelectListItem { Text = "June", Value = "6", Selected = nmonth };
            listOfMonths.Add(mo6);

            if (this_month == 7) { nmonth = true; datelist = "July"; } else { nmonth = false; }
            SelectListItem mo7 = new SelectListItem { Text = "July", Value = "7", Selected = nmonth };
            listOfMonths.Add(mo7);

            if (this_month == 8) { nmonth = true; datelist = "August"; } else { nmonth = false; }
            SelectListItem mo8 = new SelectListItem { Text = "August", Value = "8", Selected = nmonth };
            listOfMonths.Add(mo8);

            if (this_month == 9) { nmonth = true; datelist = "September"; } else { nmonth = false; }
            SelectListItem mo9 = new SelectListItem { Text = "September", Value = "9", Selected = nmonth };
            listOfMonths.Add(mo9);

            if (this_month == 10) { nmonth = true; datelist = "October"; } else { nmonth = false; }
            SelectListItem mo10 = new SelectListItem { Text = "October", Value = "10", Selected = nmonth };
            listOfMonths.Add(mo10);

            if (this_month == 11) { nmonth = true; datelist = "November"; } else { nmonth = false; }
            SelectListItem mo11 = new SelectListItem { Text = "November", Value = "11", Selected = nmonth };
            listOfMonths.Add(mo11);

            if (this_month == 12) { nmonth = true; datelist = "December"; } else { nmonth = false; }
            SelectListItem mo12 = new SelectListItem { Text = "December", Value = "12", Selected = nmonth };
            listOfMonths.Add(mo12);

            ViewBag.MonthsList = listOfMonths.ToList();
            ViewBag.DateList = datelist + " " + this_year;
            //...............................................................................................

            List<PatientExam> patientExamList = db.PatientExams.Where(p => p.ExamDate.Value.Month == this_month).ToList();

            //return View(db.PatientExams.ToList());
            return View(patientExamList);

        } //-- 

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string str_year = formCollection["ListOfYears"];
            int this_year = Convert.ToInt32(str_year);
            bool nyear = false;
            string datelist = "";

            List<SelectListItem> listOfYears = new List<SelectListItem>();
            if (this_year == 2019) { nyear = true; } else { nyear = false; }
            SelectListItem item1 = new SelectListItem { Text = "2019", Value = "2019", Selected = nyear };
            listOfYears.Add(item1);

            if (this_year == 2020) { nyear = true; } else { nyear = false; }
            SelectListItem item2 = new SelectListItem { Text = "2020", Value = "2020", Selected = nyear };
            listOfYears.Add(item2);

            if (this_year == 2021) { nyear = true; } else { nyear = false; }
            SelectListItem item3 = new SelectListItem { Text = "2021", Value = "2021", Selected = nyear };
            listOfYears.Add(item3);

            if (this_year == 2022) { nyear = true; } else { nyear = false; }
            SelectListItem item4 = new SelectListItem { Text = "2022", Value = "2022", Selected = nyear };
            listOfYears.Add(item4);

            if (this_year == 2023) { nyear = true; } else { nyear = false; }
            SelectListItem item5 = new SelectListItem { Text = "2023", Value = "2023", Selected = nyear };
            listOfYears.Add(item5);

            ViewBag.YearsList = listOfYears.ToList();
            //...............................................................................................
            string str_month = formCollection["ListOfMonths"];
            int this_month = Convert.ToInt32(str_month);
            bool nmonth = false;

            List<SelectListItem> listOfMonths = new List<SelectListItem>();

            if (this_month == 1) { nmonth = true; datelist = "January"; } else { nmonth = false; }
            SelectListItem mo1 = new SelectListItem { Text = "January", Value = "1", Selected = nmonth };
            listOfMonths.Add(mo1);

            if (this_month == 2) { nmonth = true; datelist = "February"; } else { nmonth = false; }
            SelectListItem mo2 = new SelectListItem { Text = "February", Value = "2", Selected = nmonth };
            listOfMonths.Add(mo2);

            if (this_month == 3) { nmonth = true; datelist = "March"; } else { nmonth = false; }
            SelectListItem mo3 = new SelectListItem { Text = "March", Value = "3", Selected = nmonth };
            listOfMonths.Add(mo3);

            if (this_month == 4) { nmonth = true; datelist = "April"; } else { nmonth = false; }
            SelectListItem mo4 = new SelectListItem { Text = "April", Value = "4", Selected = nmonth };
            listOfMonths.Add(mo4);

            if (this_month == 5) { nmonth = true; datelist = "May"; } else { nmonth = false; }
            SelectListItem mo5 = new SelectListItem { Text = "May", Value = "5", Selected = nmonth };
            listOfMonths.Add(mo5);

            if (this_month == 6) { nmonth = true; datelist = "June"; } else { nmonth = false; }
            SelectListItem mo6 = new SelectListItem { Text = "June", Value = "6", Selected = nmonth };
            listOfMonths.Add(mo6);

            if (this_month == 7) { nmonth = true; datelist = "July"; } else { nmonth = false; }
            SelectListItem mo7 = new SelectListItem { Text = "July", Value = "7", Selected = nmonth };
            listOfMonths.Add(mo7);

            if (this_month == 8) { nmonth = true; datelist = "August"; } else { nmonth = false; }
            SelectListItem mo8 = new SelectListItem { Text = "August", Value = "8", Selected = nmonth };
            listOfMonths.Add(mo8);

            if (this_month == 9) { nmonth = true; datelist = "September"; } else { nmonth = false; }
            SelectListItem mo9 = new SelectListItem { Text = "September", Value = "9", Selected = nmonth };
            listOfMonths.Add(mo9);

            if (this_month == 10) { nmonth = true; datelist = "October"; } else { nmonth = false; }
            SelectListItem mo10 = new SelectListItem { Text = "October", Value = "10", Selected = nmonth };
            listOfMonths.Add(mo10);

            if (this_month == 11) { nmonth = true; datelist = "November"; } else { nmonth = false; }
            SelectListItem mo11 = new SelectListItem { Text = "November", Value = "11", Selected = nmonth };
            listOfMonths.Add(mo11);

            if (this_month == 12) { nmonth = true; datelist = "December"; } else { nmonth = false; }
            SelectListItem mo12 = new SelectListItem { Text = "December", Value = "12", Selected = nmonth };
            listOfMonths.Add(mo12);

            ViewBag.MonthsList = listOfMonths.ToList();
            ViewBag.DateList = datelist + " " + this_year;
            //...............................................................................................

            List<PatientExam> patientExamList = db.PatientExams.Where(p => p.ExamDate.Value.Month == this_month && p.ExamDate.Value.Year == this_year).ToList();

            return View(patientExamList);
        } //-- 

        // GET: PatientExam/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {   return HttpNotFound();  }

            string patientDataID = patientExam.PatientID;
            int id_key = utHelp.getPatientIdKey(patientDataID);

            PatientData patientData = db.PatientDatas.Find(id_key);

            SetViewBagFileUpStatus(patientExam);

            bool examImagesExist = checkExamImagesExist(id);
            if (examImagesExist)
            {   ViewBag.ExamImagesExist = true; }
            else { ViewBag.ExamImagesExist = false;   }

            string patiendIDNo = patientExam.PatientID.ToString();
            string examIdNo = patientExam.ExamId.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            ViewBag.imageFile01 = getImageFileUploaded(ViewBag.imageFile01_Up, examIdNo, "-01.jpg");
            ViewBag.imageFile02 = getImageFileUploaded(ViewBag.imageFile02_Up, examIdNo, "-02.jpg");
            ViewBag.imageFile03 = getImageFileUploaded(ViewBag.imageFile03_Up, examIdNo, "-03.jpg");
            ViewBag.imageFile04 = getImageFileUploaded(ViewBag.imageFile04_Up, examIdNo, "-04.jpg");
            ViewBag.imageFile05 = getImageFileUploaded(ViewBag.imageFile05_Up, examIdNo, "-05.jpg");
            ViewBag.imageFile06 = getImageFileUploaded(ViewBag.imageFile06_Up, examIdNo, "-06.jpg");
            ViewBag.imageFile07 = getImageFileUploaded(ViewBag.imageFile07_Up, examIdNo, "-07.jpg");
            ViewBag.imageFile08 = getImageFileUploaded(ViewBag.imageFile08_Up, examIdNo, "-08.jpg");
            ViewBag.imageFile09 = getImageFileUploaded(ViewBag.imageFile09_Up, examIdNo, "-09.jpg");
            ViewBag.imageFile10 = getImageFileUploaded(ViewBag.imageFile10_Up, examIdNo, "-10.jpg");

            ViewBag.imageFile11 = getImageFileUploaded(ViewBag.imageFile11_Up, examIdNo, "-11.jpg");
            ViewBag.imageFile12 = getImageFileUploaded(ViewBag.imageFile12_Up, examIdNo, "-12.jpg");
            ViewBag.imageFile13 = getImageFileUploaded(ViewBag.imageFile13_Up, examIdNo, "-13.jpg");
            ViewBag.imageFile14 = getImageFileUploaded(ViewBag.imageFile14_Up, examIdNo, "-14.jpg");
            ViewBag.imageFile15 = getImageFileUploaded(ViewBag.imageFile15_Up, examIdNo, "-15.jpg");
            ViewBag.imageFile16 = getImageFileUploaded(ViewBag.imageFile16_Up, examIdNo, "-16.jpg");
            ViewBag.imageFile17 = getImageFileUploaded(ViewBag.imageFile17_Up, examIdNo, "-17.jpg");
            ViewBag.imageFile18 = getImageFileUploaded(ViewBag.imageFile18_Up, examIdNo, "-18.jpg");
            ViewBag.imageFile19 = getImageFileUploaded(ViewBag.imageFile19_Up, examIdNo, "-19.jpg");
            ViewBag.imageFile20 = getImageFileUploaded(ViewBag.imageFile20_Up, examIdNo, "-20.jpg");

            ViewBag.imageFile21 = getImageFileUploaded(ViewBag.imageFile21_Up, examIdNo, "-21.jpg");
            ViewBag.imageFile22 = getImageFileUploaded(ViewBag.imageFile22_Up, examIdNo, "-22.jpg");
            ViewBag.imageFile23 = getImageFileUploaded(ViewBag.imageFile23_Up, examIdNo, "-23.jpg");
            ViewBag.imageFile24 = getImageFileUploaded(ViewBag.imageFile24_Up, examIdNo, "-24.jpg");
            ViewBag.imageFile25 = getImageFileUploaded(ViewBag.imageFile25_Up, examIdNo, "-25.jpg");
            ViewBag.imageFile26 = getImageFileUploaded(ViewBag.imageFile26_Up, examIdNo, "-26.jpg");
            ViewBag.imageFile27 = getImageFileUploaded(ViewBag.imageFile27_Up, examIdNo, "-27.jpg");
            ViewBag.imageFile28 = getImageFileUploaded(ViewBag.imageFile28_Up, examIdNo, "-28.jpg");
            ViewBag.imageFile29 = getImageFileUploaded(ViewBag.imageFile29_Up, examIdNo, "-29.jpg");
            ViewBag.imageFile30 = getImageFileUploaded(ViewBag.imageFile30_Up, examIdNo, "-30.jpg");
            ViewBag.imageFile31 = getImageFileUploaded(ViewBag.imageFile31_Up, examIdNo, "-31.jpg");
            ViewBag.imageFile32 = getImageFileUploaded(ViewBag.imageFile32_Up, examIdNo, "-32.jpg");

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();

            ViewBag.PatientAge = patientData.Age;
            ViewBag.PatientStatus = patientData.getStatusDesc;

            return View(patientExam);
        } //--
        
        // GET: PatientExam/Create
        public ActionResult Create()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            ViewBag.PatientList = new SelectList(db.PatientDatas.OrderBy(p => p.Lastname), "Patient_Id", "GetFullname");
            ViewBag.ExamtypeList = new SelectList(db.DiagnosticExams, "Id", "GetfullExamName");
            ViewBag.SonographerList = new SelectList(db.Sonographers, "Id", "GetFullname");
            ViewBag.DoctorList = new SelectList(db.RadiologistDoctors, "Id", "GetFullname");

            return View();
        }

        // POST: PatientExam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientID,ExamType,ExamDate,Sonographer,Radiologist,ExamReport,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,History,Image11,Image12,Image13,Image14,Image15,Image16,Image17,Image18,Image19,Image20,Image21,Image22,Image23,Image24,Image25,Image26,Image27,Image28,Image29,Image30,Image31,Image32,ExamId")] PatientExam patientExam)
        {
            if (ModelState.IsValid)
            {
                patientExam.Image1 = false;
                patientExam.Image2 = false;
                patientExam.Image3 = false;
                patientExam.Image4 = false;
                patientExam.Image5 = false;
                patientExam.Image6 = false;
                patientExam.Image7 = false;
                patientExam.Image8 = false;
                patientExam.Image9 = false;
                patientExam.Image10 = false;
                patientExam.Image11 = false;
                patientExam.Image12 = false;
                patientExam.Image13 = false;
                patientExam.Image14 = false;
                patientExam.Image15 = false;
                patientExam.Image16 = false;
                patientExam.Image17 = false;
                patientExam.Image18 = false;
                patientExam.Image19 = false;
                patientExam.Image20 = false;
                patientExam.Image21 = false;
                patientExam.Image22 = false;
                patientExam.Image23 = false;
                patientExam.Image24 = false;
                patientExam.Image25 = false;
                patientExam.Image26 = false;
                patientExam.Image27 = false;
                patientExam.Image28 = false;
                patientExam.Image29 = false;
                patientExam.Image30 = false;
                patientExam.Image31 = false;
                patientExam.Image32 = false;

                db.PatientExams.Add(patientExam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PatientList = new SelectList(db.PatientDatas, "Patient_Id", "GetIDFullname");
                ViewBag.ExamtypeList = new SelectList(db.DiagnosticExams, "Id", "GetfullExamName");
                ViewBag.SonographerList = new SelectList(db.Sonographers, "Id", "GetFullname");
                ViewBag.DoctorList = new SelectList(db.RadiologistDoctors, "Id", "GetFullname");
            }

            return View(patientExam);

        } //**

        // GET: PatientExam/Edit/5
        public ActionResult Edit(int? id)
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {   return HttpNotFound();}

            ViewBag.PatientList = new SelectList(db.PatientDatas, "Patient_Id", "GetIDFullname");
            ViewBag.ExamtypeList = new SelectList(db.DiagnosticExams, "Id", "GetfullExamName");
            ViewBag.SonographerList = new SelectList(db.Sonographers, "Id", "GetFullname");
            ViewBag.DoctorList = new SelectList(db.RadiologistDoctors, "Id", "GetFullname");

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();

            return View(patientExam);
        }

        // POST: PatientExam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientID,ExamType,ExamDate,Sonographer,Radiologist,ExamReport,SignByDoctor,DateSigned,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,History,Image21,Image22,Image23,Image24,Image25,Image26,Image27,Image28,Image29,Image30,Image31,Image32,ExamId")] PatientExam patientExam, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(patientExam).State = EntityState.Modified;
                //db.SaveChanges();

                string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;
                using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "Update PatientExam Set ExamDate = @exam_date, ExamType = @exam_type, History = @history, Radiologist = @doctor_id, Sonographer = @sono_id, ExamId = @exam_Id Where Id = @examId";

                    if (patientExam.History == null) { patientExam.History = ""; }

                    sqlCmd.Parameters.AddWithValue("@exam_date", patientExam.ExamDate);
                    sqlCmd.Parameters.AddWithValue("@exam_type", patientExam.ExamType);
                    sqlCmd.Parameters.AddWithValue("@history", patientExam.History);
                    sqlCmd.Parameters.AddWithValue("@doctor_id", patientExam.Radiologist);
                    sqlCmd.Parameters.AddWithValue("@sono_id", patientExam.Sonographer);
                    sqlCmd.Parameters.AddWithValue("@examId", patientExam.Id);
                    sqlCmd.Parameters.AddWithValue("@exam_Id", patientExam.ExamId);

                    sqlCmd.Connection = sqlCon;
                    sqlCon.Open();
                    int rowaffected = sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                string exam_month = patientExam.ExamDate.Value.Month.ToString();
                string exam_year = patientExam.ExamDate.Value.Year.ToString();

                return RedirectToAction("Index", new { mo = exam_month, yr = exam_year });
            }
            else {
                ViewBag.PatientList = new SelectList(db.PatientDatas, "Patient_Id", "GetIDFullname");
                ViewBag.ExamtypeList = new SelectList(db.DiagnosticExams, "Id", "GetfullExamName");
                ViewBag.SonographerList = new SelectList(db.Sonographers, "Id", "GetFullname");
                ViewBag.DoctorList = new SelectList(db.RadiologistDoctors, "Id", "GetFullname");
            }

            return View(patientExam);
        } //--

        // GET: PatientExam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {
                return HttpNotFound();
            }
            return View(patientExam);
        }

        // POST: PatientExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientExam patientExam = db.PatientExams.Find(id);
            db.PatientExams.Remove(patientExam);
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

        public bool InvalidFileExtension(string file_ext)
        {
            if (file_ext.ToLower() != ".jpg")
            {
                ViewBag.FileErrorMessage = "File Upload ERROR: Only JPG files are allowed to upload ... ";
                return true;
            }
            else
            { return false; }
        } //--

        [HttpGet]
        public ActionResult ScanImages(int? id)
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);   }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {
                return HttpNotFound();
            }

            ViewBag.patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.patientExamEntityKey = patientExam.Id;
            ViewBag.PatientName = patientExam.getPatientName.ToString();
            ViewBag.PatientExamName = patientExam.getExamName.ToString();
            ViewBag.PatientExamDate = patientExam.ExamDate.ToString();
            ViewBag.PatientExamId = patientExam.ExamId.ToString();

            SetViewBagFileUpStatus(patientExam);

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;
            string patientExamIdNo = patientExam.ExamId.ToString();

            ViewBag.imageFile01 = getImageFileUploaded(ViewBag.imageFile01_Up, patientExamIdNo, "-01.jpg");
            ViewBag.imageFile02 = getImageFileUploaded(ViewBag.imageFile02_Up, patientExamIdNo, "-02.jpg");
            ViewBag.imageFile03 = getImageFileUploaded(ViewBag.imageFile03_Up, patientExamIdNo, "-03.jpg");
            ViewBag.imageFile04 = getImageFileUploaded(ViewBag.imageFile04_Up, patientExamIdNo, "-04.jpg");
            ViewBag.imageFile05 = getImageFileUploaded(ViewBag.imageFile05_Up, patientExamIdNo, "-05.jpg");
            ViewBag.imageFile06 = getImageFileUploaded(ViewBag.imageFile06_Up, patientExamIdNo, "-06.jpg");
            ViewBag.imageFile07 = getImageFileUploaded(ViewBag.imageFile07_Up, patientExamIdNo, "-07.jpg");
            ViewBag.imageFile08 = getImageFileUploaded(ViewBag.imageFile08_Up, patientExamIdNo, "-08.jpg");
            ViewBag.imageFile09 = getImageFileUploaded(ViewBag.imageFile09_Up, patientExamIdNo, "-09.jpg");
            ViewBag.imageFile10 = getImageFileUploaded(ViewBag.imageFile10_Up, patientExamIdNo, "-10.jpg");

            ViewBag.imageFile11 = getImageFileUploaded(ViewBag.imageFile11_Up, patientExamIdNo, "-11.jpg");
            ViewBag.imageFile12 = getImageFileUploaded(ViewBag.imageFile12_Up, patientExamIdNo, "-12.jpg");
            ViewBag.imageFile13 = getImageFileUploaded(ViewBag.imageFile13_Up, patientExamIdNo, "-13.jpg");
            ViewBag.imageFile14 = getImageFileUploaded(ViewBag.imageFile14_Up, patientExamIdNo, "-14.jpg");
            ViewBag.imageFile15 = getImageFileUploaded(ViewBag.imageFile15_Up, patientExamIdNo, "-15.jpg");
            ViewBag.imageFile16 = getImageFileUploaded(ViewBag.imageFile16_Up, patientExamIdNo, "-16.jpg");
            ViewBag.imageFile17 = getImageFileUploaded(ViewBag.imageFile17_Up, patientExamIdNo, "-17.jpg");
            ViewBag.imageFile18 = getImageFileUploaded(ViewBag.imageFile18_Up, patientExamIdNo, "-18.jpg");
            ViewBag.imageFile19 = getImageFileUploaded(ViewBag.imageFile18_Up, patientExamIdNo, "-18.jpg");
            ViewBag.imageFile20 = getImageFileUploaded(ViewBag.imageFile20_Up, patientExamIdNo, "-20.jpg");

            ViewBag.imageFile21 = getImageFileUploaded(ViewBag.imageFile21_Up, patientExamIdNo, "-21.jpg");
            ViewBag.imageFile22 = getImageFileUploaded(ViewBag.imageFile22_Up, patientExamIdNo, "-22.jpg");
            ViewBag.imageFile23 = getImageFileUploaded(ViewBag.imageFile23_Up, patientExamIdNo, "-23.jpg");
            ViewBag.imageFile24 = getImageFileUploaded(ViewBag.imageFile24_Up, patientExamIdNo, "-24.jpg");
            ViewBag.imageFile25 = getImageFileUploaded(ViewBag.imageFile25_Up, patientExamIdNo, "-25.jpg");
            ViewBag.imageFile26 = getImageFileUploaded(ViewBag.imageFile26_Up, patientExamIdNo, "-26.jpg");
            ViewBag.imageFile27 = getImageFileUploaded(ViewBag.imageFile27_Up, patientExamIdNo, "-27.jpg");
            ViewBag.imageFile28 = getImageFileUploaded(ViewBag.imageFile28_Up, patientExamIdNo, "-28.jpg");
            ViewBag.imageFile29 = getImageFileUploaded(ViewBag.imageFile28_Up, patientExamIdNo, "-28.jpg");
            ViewBag.imageFile30 = getImageFileUploaded(ViewBag.imageFile30_Up, patientExamIdNo, "-30.jpg");
            ViewBag.imageFile31 = getImageFileUploaded(ViewBag.imageFile31_Up, patientExamIdNo, "-31.jpg");
            ViewBag.imageFile32 = getImageFileUploaded(ViewBag.imageFile32_Up, patientExamIdNo, "-32.jpg");

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();

            return View(patientExam);

        } //--

        [HttpPost]
        public ActionResult UploadScanImages(int patientExamId, string patiendIDNo, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5, HttpPostedFileBase file6, HttpPostedFileBase file7, HttpPostedFileBase file8, HttpPostedFileBase file9, HttpPostedFileBase file10, 
            HttpPostedFileBase file11, HttpPostedFileBase file12, HttpPostedFileBase file13, HttpPostedFileBase file14, HttpPostedFileBase file15, HttpPostedFileBase file16, HttpPostedFileBase file17, HttpPostedFileBase file18, HttpPostedFileBase file19, HttpPostedFileBase file20, HttpPostedFileBase file21, HttpPostedFileBase file22,
            HttpPostedFileBase file23, HttpPostedFileBase file24, HttpPostedFileBase file25, HttpPostedFileBase file26, HttpPostedFileBase file27, HttpPostedFileBase file28, HttpPostedFileBase file29, HttpPostedFileBase file30, HttpPostedFileBase file31, HttpPostedFileBase file32)
        {
            PatientExam patientExam = db.PatientExams.Find(patientExamId);

            if (patientExam == null)
            {   return HttpNotFound();  }

            string patient_ExamIdNo = patientExam.ExamId.ToString();
            string patient_id_number = patientExam.PatientID.ToString();
            bool upload01, upload02, upload03, upload04, upload05, upload06, upload07, upload08, upload09, upload10;
            bool upload11, upload12, upload13, upload14, upload15, upload16, upload17, upload18, upload19, upload20;
            bool upload21, upload22, upload23, upload24, upload25, upload26, upload27, upload28, upload29, upload30, upload31, upload32;

            ViewBag.patiendIDNo = patient_id_number;
            ViewBag.patientExamId = patientExam.Id;
            ViewBag.PatientUSexamID = patient_ExamIdNo;
            ViewBag.PatientName = patientExam.getPatientName.ToString();
            ViewBag.PatientExamName = patientExam.getExamName.ToString();
            ViewBag.PatientExamDate = patientExam.ExamDate.ToString();

            ViewBag.imageFile01_Up = upload01 = (bool)patientExam.Image1;
            ViewBag.imageFile02_Up = upload02 = (bool)patientExam.Image2;
            ViewBag.imageFile03_Up = upload03 = (bool)patientExam.Image3;
            ViewBag.imageFile04_Up = upload04 = (bool)patientExam.Image4;
            ViewBag.imageFile05_Up = upload05 = (bool)patientExam.Image5;
            ViewBag.imageFile06_Up = upload06 = (bool)patientExam.Image6;
            ViewBag.imageFile07_Up = upload07 = (bool)patientExam.Image7;
            ViewBag.imageFile08_Up = upload08 = (bool)patientExam.Image8;
            ViewBag.imageFile09_Up = upload09 = (bool)patientExam.Image9;
            ViewBag.imageFile10_Up = upload10 = (bool)patientExam.Image10;

            ViewBag.imageFile11_Up = upload11 = (bool)patientExam.Image11;
            ViewBag.imageFile12_Up = upload12 = (bool)patientExam.Image12;
            ViewBag.imageFile13_Up = upload13 = (bool)patientExam.Image13;
            ViewBag.imageFile14_Up = upload14 = (bool)patientExam.Image14;
            ViewBag.imageFile15_Up = upload15 = (bool)patientExam.Image15;
            ViewBag.imageFile16_Up = upload16 = (bool)patientExam.Image16;
            ViewBag.imageFile17_Up = upload17 = (bool)patientExam.Image17;
            ViewBag.imageFile18_Up = upload18 = (bool)patientExam.Image18;
            ViewBag.imageFile19_Up = upload19 = (bool)patientExam.Image19;
            ViewBag.imageFile20_Up = upload20 = (bool)patientExam.Image20;

            ViewBag.imageFile21_Up = upload21 = (bool)patientExam.Image21;
            ViewBag.imageFile22_Up = upload22 = (bool)patientExam.Image22;
            ViewBag.imageFile23_Up = upload23 = (bool)patientExam.Image23;
            ViewBag.imageFile24_Up = upload24 = (bool)patientExam.Image24;
            ViewBag.imageFile25_Up = upload25 = (bool)patientExam.Image25;
            ViewBag.imageFile26_Up = upload26 = (bool)patientExam.Image26;
            ViewBag.imageFile27_Up = upload27 = (bool)patientExam.Image27;
            ViewBag.imageFile28_Up = upload28 = (bool)patientExam.Image28;
            ViewBag.imageFile29_Up = upload29 = (bool)patientExam.Image29;
            ViewBag.imageFile30_Up = upload30 = (bool)patientExam.Image30;
            ViewBag.imageFile31_Up = upload31 = (bool)patientExam.Image31;
            ViewBag.imageFile32_Up = upload32 = (bool)patientExam.Image32;

            string examImageFilename = "";

            examImageFilename = patient_ExamIdNo + "-01.jpg";
            ViewBag.imageFile01 = examImageFilename;
            upload01 = UploadExamImagefile(examImageFilename, ViewBag.imageFile01_Up, file1, upload01);
            ViewBag.imageFile01_Up = upload01;

            examImageFilename = patient_ExamIdNo + "-02.jpg";
            ViewBag.imageFile02 = examImageFilename;
            upload02 = UploadExamImagefile(examImageFilename, ViewBag.imageFile02_Up, file2, upload02);
            ViewBag.imageFile02_Up = upload02;

            examImageFilename = patient_ExamIdNo + "-03.jpg";
            ViewBag.imageFile03 = examImageFilename;
            upload03 = UploadExamImagefile(examImageFilename, ViewBag.imageFile03_Up, file3, upload03);
            ViewBag.imageFile03_Up = upload03;

            examImageFilename = patient_ExamIdNo + "-04.jpg";
            ViewBag.imageFile04 = examImageFilename;
            upload04 = UploadExamImagefile(examImageFilename, ViewBag.imageFile04_Up, file4, upload04);
            ViewBag.imageFile04_Up = upload04;

            examImageFilename = patient_ExamIdNo + "-05.jpg";
            ViewBag.imageFile05 = examImageFilename;
            upload05 = UploadExamImagefile(examImageFilename, ViewBag.imageFile05_Up, file5, upload05);
            ViewBag.imageFile05_Up = upload05;

            examImageFilename = patient_ExamIdNo + "-06.jpg";
            ViewBag.imageFile06 = examImageFilename;
            upload06 = UploadExamImagefile(examImageFilename, ViewBag.imageFile06_Up, file6, upload06);
            ViewBag.imageFile06_Up = upload06;

            examImageFilename = patient_ExamIdNo + "-07.jpg";
            ViewBag.imageFile07 = examImageFilename;
            upload07 = UploadExamImagefile(examImageFilename, ViewBag.imageFile07_Up, file7, upload07);
            ViewBag.imageFile07_Up = upload07;

            examImageFilename = patient_ExamIdNo + "-08.jpg";
            ViewBag.imageFile08 = examImageFilename;
            upload08 = UploadExamImagefile(examImageFilename, ViewBag.imageFile08_Up, file5, upload08);
            ViewBag.imageFile08_Up = upload08;

            examImageFilename = patient_ExamIdNo + "-09.jpg";
            ViewBag.imageFile09 = examImageFilename;
            upload09 = UploadExamImagefile(examImageFilename, ViewBag.imageFile09_Up, file9, upload09);
            ViewBag.imageFile09_Up = upload09;

            examImageFilename = patient_ExamIdNo + "-10.jpg";
            ViewBag.imageFile10 = examImageFilename;
            upload10 = UploadExamImagefile(examImageFilename, ViewBag.imageFile10_Up, file10, upload10);
            ViewBag.imageFile10_Up = upload10;

            examImageFilename = patient_ExamIdNo + "-11.jpg";
            ViewBag.imageFile11 = examImageFilename;
            upload11 = UploadExamImagefile(examImageFilename, ViewBag.imageFile11_Up, file11, upload11);
            ViewBag.imageFile11_Up = upload11;

            examImageFilename = patient_ExamIdNo + "-12.jpg";
            ViewBag.imageFile12 = examImageFilename;
            upload12 = UploadExamImagefile(examImageFilename, ViewBag.imageFile12_Up, file12, upload12);
            ViewBag.imageFile12_Up = upload12;

            examImageFilename = patient_ExamIdNo + "-13.jpg";
            ViewBag.imageFile13 = examImageFilename;
            upload13 = UploadExamImagefile(examImageFilename, ViewBag.imageFile13_Up, file13, upload13);
            ViewBag.imageFile13_Up = upload13;

            examImageFilename = patient_ExamIdNo + "-14.jpg";
            ViewBag.imageFile14 = examImageFilename;
            upload14 = UploadExamImagefile(examImageFilename, ViewBag.imageFile14_Up, file14, upload14);
            ViewBag.imageFile14_Up = upload14;

            examImageFilename = patient_ExamIdNo + "-15.jpg";
            ViewBag.imageFile15 = examImageFilename;
            upload15 = UploadExamImagefile(examImageFilename, ViewBag.imageFile15_Up, file15, upload15);
            ViewBag.imageFile15_Up = upload15;

            examImageFilename = patient_ExamIdNo + "-16.jpg";
            ViewBag.imageFile16 = examImageFilename;
            upload16 = UploadExamImagefile(examImageFilename, ViewBag.imageFile16_Up, file16, upload16);
            ViewBag.imageFile16_Up = upload16;

            examImageFilename = patient_ExamIdNo + "-17.jpg";
            ViewBag.imageFile17 = examImageFilename;
            upload17 = UploadExamImagefile(examImageFilename, ViewBag.imageFile17_Up, file17, upload17);
            ViewBag.imageFile17_Up = upload17;

            examImageFilename = patient_ExamIdNo + "-18.jpg";
            ViewBag.imageFile18 = examImageFilename;
            upload18 = UploadExamImagefile(examImageFilename, ViewBag.imageFile18_Up, file18, upload18);
            ViewBag.imageFile18_Up = upload18;

            examImageFilename = patient_ExamIdNo + "-19.jpg";
            ViewBag.imageFile19 = examImageFilename;
            upload19 = UploadExamImagefile(examImageFilename, ViewBag.imageFile19_Up, file19, upload19);
            ViewBag.imageFile19_Up = upload19;

            examImageFilename = patient_ExamIdNo + "-20.jpg";
            ViewBag.imageFile20 = examImageFilename;
            upload20 = UploadExamImagefile(examImageFilename, ViewBag.imageFile20_Up, file20, upload20);
            ViewBag.imageFile20_Up = upload20;

            examImageFilename = patient_ExamIdNo + "-21.jpg";
            ViewBag.imageFile21 = examImageFilename;
            upload21 = UploadExamImagefile(examImageFilename, ViewBag.imageFile21_Up, file21, upload21);
            ViewBag.imageFile21_Up = upload21;

            examImageFilename = patient_ExamIdNo + "-22.jpg";
            ViewBag.imageFile22 = examImageFilename;
            upload22 = UploadExamImagefile(examImageFilename, ViewBag.imageFile22_Up, file22, upload22);
            ViewBag.imageFile22_Up = upload22;

            examImageFilename = patient_ExamIdNo + "-23.jpg";
            ViewBag.imageFile23 = examImageFilename;
            upload23 = UploadExamImagefile(examImageFilename, ViewBag.imageFile23_Up, file23, upload23);
            ViewBag.imageFile23_Up = upload23;

            examImageFilename = patient_ExamIdNo + "-24.jpg";
            ViewBag.imageFile24 = examImageFilename;
            upload24 = UploadExamImagefile(examImageFilename, ViewBag.imageFile24_Up, file24, upload24);
            ViewBag.imageFile24_Up = upload24;

            examImageFilename = patient_ExamIdNo + "-25.jpg";
            ViewBag.imageFile25 = examImageFilename;
            upload25 = UploadExamImagefile(examImageFilename, ViewBag.imageFile25_Up, file25, upload25);
            ViewBag.imageFile25_Up = upload25;

            examImageFilename = patient_ExamIdNo + "-26.jpg";
            ViewBag.imageFile26 = examImageFilename;
            upload26 = UploadExamImagefile(examImageFilename, ViewBag.imageFile26_Up, file26, upload26);
            ViewBag.imageFile26_Up = upload26;

            examImageFilename = patient_ExamIdNo + "-27.jpg";
            ViewBag.imageFile27 = examImageFilename;
            upload27 = UploadExamImagefile(examImageFilename, ViewBag.imageFile27_Up, file27, upload27);
            ViewBag.imageFile27_Up = upload27;

            examImageFilename = patient_ExamIdNo + "-28.jpg";
            ViewBag.imageFile28 = examImageFilename;
            upload28 = UploadExamImagefile(examImageFilename, ViewBag.imageFile28_Up, file28, upload28);
            ViewBag.imageFile28_Up = upload28;

            examImageFilename = patient_ExamIdNo + "-29.jpg";
            ViewBag.imageFile29 = examImageFilename;
            upload29 = UploadExamImagefile(examImageFilename, ViewBag.imageFile29_Up, file29, upload29);
            ViewBag.imageFile29_Up = upload29;

            examImageFilename = patient_ExamIdNo + "-30.jpg";
            ViewBag.imageFile30 = examImageFilename;
            upload30 = UploadExamImagefile(examImageFilename, ViewBag.imageFile30_Up, file30, upload30);
            ViewBag.imageFile30_Up = upload30;

            examImageFilename = patient_ExamIdNo + "-31.jpg";
            ViewBag.imageFile31 = examImageFilename;
            upload31 = UploadExamImagefile(examImageFilename, ViewBag.imageFile31_Up, file31, upload31);
            ViewBag.imageFile31_Up = upload31;

            examImageFilename = patient_ExamIdNo + "-32.jpg";
            ViewBag.imageFile32 = examImageFilename;
            upload32 = UploadExamImagefile(examImageFilename, ViewBag.imageFile32_Up, file32, upload32);
            ViewBag.imageFile32_Up = upload32;

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;

            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update PatientExam Set Image1 = @upload01, Image2 = @upload02, Image3 = @upload03, Image4 = @upload04, Image5 = @upload05, Image6 = @upload06, Image7 = @upload07, Image8 = @upload08, Image9 = @upload09, Image10 = @upload10, " +
                    "Image11 = @upload11, Image12 = @upload12, Image13 = @upload13, Image14 = @upload14, Image15 = @upload15, Image16 = @upload16, Image17 = @upload17, Image18 = @upload18, Image19 = @upload19, Image20 = @upload20, Image21 = @upload21, " +
                    "Image22 = @upload22, Image23 = @upload23,  Image24 = @upload24, Image25 = @upload25, Image26 = @upload26, Image27 = @upload27, Image28 = @upload28, Image29 = @upload29, Image30 = @upload30, Image31 = @upload31,  Image32 = @upload32 Where Id = @examId";

                sqlCmd.Parameters.AddWithValue("@upload01", upload01);
                sqlCmd.Parameters.AddWithValue("@upload02", upload02);
                sqlCmd.Parameters.AddWithValue("@upload03", upload03);
                sqlCmd.Parameters.AddWithValue("@upload04", upload04);
                sqlCmd.Parameters.AddWithValue("@upload05", upload05);
                sqlCmd.Parameters.AddWithValue("@upload06", upload06);
                sqlCmd.Parameters.AddWithValue("@upload07", upload07);
                sqlCmd.Parameters.AddWithValue("@upload08", upload08);
                sqlCmd.Parameters.AddWithValue("@upload09", upload09);
                sqlCmd.Parameters.AddWithValue("@upload10", upload10);

                sqlCmd.Parameters.AddWithValue("@upload11", upload11);
                sqlCmd.Parameters.AddWithValue("@upload12", upload12);
                sqlCmd.Parameters.AddWithValue("@upload13", upload13);
                sqlCmd.Parameters.AddWithValue("@upload14", upload14);
                sqlCmd.Parameters.AddWithValue("@upload15", upload15);
                sqlCmd.Parameters.AddWithValue("@upload16", upload16);
                sqlCmd.Parameters.AddWithValue("@upload17", upload17);
                sqlCmd.Parameters.AddWithValue("@upload18", upload18);
                sqlCmd.Parameters.AddWithValue("@upload19", upload19);
                sqlCmd.Parameters.AddWithValue("@upload20", upload20);

                sqlCmd.Parameters.AddWithValue("@upload21", upload21);
                sqlCmd.Parameters.AddWithValue("@upload22", upload22);
                sqlCmd.Parameters.AddWithValue("@upload23", upload23);
                sqlCmd.Parameters.AddWithValue("@upload24", upload24);
                sqlCmd.Parameters.AddWithValue("@upload25", upload25);
                sqlCmd.Parameters.AddWithValue("@upload26", upload26);
                sqlCmd.Parameters.AddWithValue("@upload27", upload27);
                sqlCmd.Parameters.AddWithValue("@upload28", upload28);
                sqlCmd.Parameters.AddWithValue("@upload29", upload29);
                sqlCmd.Parameters.AddWithValue("@upload30", upload30);
                sqlCmd.Parameters.AddWithValue("@upload31", upload31);
                sqlCmd.Parameters.AddWithValue("@upload32", upload32);

                sqlCmd.Parameters.AddWithValue("@examId", patientExamId);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();

            return View();
        }

        [HttpGet]
        public ActionResult InitialReport(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patientDataID = patientExam.PatientID.ToString();
            int id_key = utHelp.getPatientIdKey(patientDataID);

            PatientData patientData = db.PatientDatas.Find(id_key);

            bool examImagesExist = false;
            SetViewBagFileUpStatus(patientExam);

            string patientExamIdNo = patientExam.ExamId.ToString();
            examImagesExist = CheckAndSetImageStatusFilename(patientExamIdNo);

            ViewBag.ExamImageExist = examImagesExist;
            if (!examImagesExist)
            {
                ViewBag.ReportImageError = "Cannot create a report without an exam images!";
            }

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();
            ViewBag.PatientAge = patientData.Age;
            ViewBag.PatientStatus = patientData.getStatusDesc.ToString();
            ViewBag.ReportTemplateList = new SelectList(db.ExamReportTemplates, "Id", "ReportName");

            return View(patientExam);
        } //-- 

        [HttpPost]
        public ActionResult InitialReport([Bind(Include = "Id,PatientID,ExamReport")] PatientExam patientExam)
        {
            int patientExamId = patientExam.Id;
            string examReport = patientExam.ExamReport.ToString();

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;

            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update PatientExam Set ExamReport = @exam_report Where Id = @examId";

                sqlCmd.Parameters.AddWithValue("@exam_report", examReport);
                sqlCmd.Parameters.AddWithValue("@examId", patientExamId);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return RedirectToAction("Details/" + patientExamId);
            ;
        } //**

        [HttpGet]
        public ActionResult SignMedicalReport(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {   return HttpNotFound();  }

            string patientDataID = patientExam.PatientID.ToString();
            PatientData patientData = db.PatientDatas.Find(patientDataID);

            bool examImagesExist = false;

            SetViewBagFileUpStatus(patientExam);


            string patiendIDNo = patientExam.PatientID.ToString();
            string patientExamIdNo = patientExam.ExamId.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            if (ViewBag.imageFile01_Up)
            {
                string filename01 = patientExamIdNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile02_Up)
            {
                string filename02 = patientExamIdNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile03_Up)
            {
                string filename03 = patientExamIdNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile04_Up)
            {
                string filename04 = patientExamIdNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patientExamIdNo + "-05.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patientExamIdNo + "-06.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patientExamIdNo + "-07.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patientExamIdNo + "-08.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patientExamIdNo + "-09.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patientExamIdNo + "-10.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile11_Up)
            {
                ViewBag.imageFile11 = patientExamIdNo + "-11.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile12_Up)
            {
                ViewBag.imageFile12 = patientExamIdNo + "-12.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile13_Up)
            {
                ViewBag.imageFile13 = patientExamIdNo + "-13.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile14_Up)
            {
                ViewBag.imageFile14 = patientExamIdNo + "-14.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile15_Up)
            {
                ViewBag.imageFile15 = patientExamIdNo + "-15.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile16_Up)
            {
                ViewBag.imageFile16 = patientExamIdNo + "-16.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile17_Up)
            {
                ViewBag.imageFile17 = patientExamIdNo + "-17.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile18_Up)
            {
                ViewBag.imageFile18 = patientExamIdNo + "-18.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile19_Up)
            {
                ViewBag.imageFile19 = patientExamIdNo + "-19.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile20_Up)
            {
                ViewBag.imageFile20 = patientExamIdNo + "-20.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile21_Up)
            {
                ViewBag.imageFile21 = patientExamIdNo + "-21.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile22_Up)
            {
                ViewBag.imageFile22 = patientExamIdNo + "-22.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile23_Up)
            {
                ViewBag.imageFile23 = patientExamIdNo + "-23.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile24_Up)
            {
                ViewBag.imageFile24 = patientExamIdNo + "-24.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile25_Up)
            {
                ViewBag.imageFile25 = patientExamIdNo + "-25.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile26_Up)
            {
                ViewBag.imageFile26 = patientExamIdNo + "-26.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile27_Up)
            {
                ViewBag.imageFile27 = patientExamIdNo + "-27.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile28_Up)
            {
                ViewBag.imageFile28 = patientExamIdNo + "-28.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile29_Up)
            {
                ViewBag.imageFile29 = patientExamIdNo + "-29.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile30_Up)
            {
                ViewBag.imageFile30 = patientExamIdNo + "-30.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile31_Up)
            {
                ViewBag.imageFile31 = patientExamIdNo + "-31.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile32_Up)
            {
                ViewBag.imageFile32 = patientExamIdNo + "-32.jpg";
                examImagesExist = true;
            } //--


            ViewBag.ExamImageExist = examImagesExist;
            if (!examImagesExist)
            {
                ViewBag.ReportImageError = "Cannot create a report without an exam images!";
            }

            ViewBag.EditMonth = patientExam.ExamDate.Value.Month.ToString();
            ViewBag.EditYear = patientExam.ExamDate.Value.Year.ToString();
            ViewBag.PatientAge = patientData.Age;
            ViewBag.PatientStatus = patientData.getStatusDesc.ToString();

            return View(patientExam);

        } //**

        [HttpPost]
        public ActionResult SignMedicalReport([Bind(Include = "Id,PatientID,ExamReport,SignByDoctor,DateSigned")] PatientExam patientExam)
        {
            int patientExamId = patientExam.Id;
            string examReport = patientExam.ExamReport.ToString();

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;

            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update PatientExam Set ExamReport = @exam_report, SignByDoctor = @sign_doctor, DateSigned = @date_signed Where Id = @examId";

                sqlCmd.Parameters.AddWithValue("@exam_report", examReport);
                sqlCmd.Parameters.AddWithValue("@sign_doctor", "Y");
                sqlCmd.Parameters.AddWithValue("@date_signed", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@examId", patientExamId);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return RedirectToAction("Details/" + patientExamId);
;
        } //**

        [AllowAnonymous]
        public ActionResult PrintExamReport(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;
            int id_key = utHelp.getPatientIdKey(patiendIDNo);

            PatientData patientData = db.PatientDatas.Find(id_key);

            ViewBag.PatientSex = utHelp.getGenderDefinition(patientData.Sex.ToString());
            ViewBag.Age = patientData.Age.ToString();
            ViewBag.Status = utHelp.getStatusDefinition(patientData.Status.ToString());

            string examReport = patientExam.ExamReport.ToString();
            //string examReportNewLine = examReport.Replace(".", "<br />");
            ViewBag.ExamReport = examReport;

            return View(patientExam);
        } //--

        [AllowAnonymous]
        public ActionResult PrintExamImages(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            SetViewBagFileUpStatus(patientExam);

            string patiendIDNo = patientExam.PatientID.ToString();
            string patientExamIdNo = patientExam.ExamId.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            ViewBag.imageFile01 = getImageFileUploaded(ViewBag.imageFile01_Up, patientExamIdNo, "-01.jpg");
            ViewBag.imageFile02 = getImageFileUploaded(ViewBag.imageFile02_Up, patientExamIdNo, "-02.jpg");
            ViewBag.imageFile03 = getImageFileUploaded(ViewBag.imageFile03_Up, patientExamIdNo, "-03.jpg");
            ViewBag.imageFile04 = getImageFileUploaded(ViewBag.imageFile04_Up, patientExamIdNo, "-04.jpg");
            ViewBag.imageFile05 = getImageFileUploaded(ViewBag.imageFile05_Up, patientExamIdNo, "-05.jpg");
            ViewBag.imageFile06 = getImageFileUploaded(ViewBag.imageFile06_Up, patientExamIdNo, "-06.jpg");
            ViewBag.imageFile07 = getImageFileUploaded(ViewBag.imageFile07_Up, patientExamIdNo, "-07.jpg");
            ViewBag.imageFile08 = getImageFileUploaded(ViewBag.imageFile08_Up, patientExamIdNo, "-08.jpg");
            ViewBag.imageFile09 = getImageFileUploaded(ViewBag.imageFile09_Up, patientExamIdNo, "-09.jpg");
            ViewBag.imageFile10 = getImageFileUploaded(ViewBag.imageFile10_Up, patientExamIdNo, "-10.jpg");

            ViewBag.imageFile11 = getImageFileUploaded(ViewBag.imageFile11_Up, patientExamIdNo, "-11.jpg");
            ViewBag.imageFile12 = getImageFileUploaded(ViewBag.imageFile12_Up, patientExamIdNo, "-12.jpg");
            ViewBag.imageFile13 = getImageFileUploaded(ViewBag.imageFile13_Up, patientExamIdNo, "-13.jpg");
            ViewBag.imageFile14 = getImageFileUploaded(ViewBag.imageFile14_Up, patientExamIdNo, "-14.jpg");
            ViewBag.imageFile15 = getImageFileUploaded(ViewBag.imageFile15_Up, patientExamIdNo, "-15.jpg");
            ViewBag.imageFile16 = getImageFileUploaded(ViewBag.imageFile16_Up, patientExamIdNo, "-16.jpg");
            ViewBag.imageFile17 = getImageFileUploaded(ViewBag.imageFile17_Up, patientExamIdNo, "-17.jpg");
            ViewBag.imageFile18 = getImageFileUploaded(ViewBag.imageFile18_Up, patientExamIdNo, "-18.jpg");
            ViewBag.imageFile19 = getImageFileUploaded(ViewBag.imageFile18_Up, patientExamIdNo, "-18.jpg");
            ViewBag.imageFile20 = getImageFileUploaded(ViewBag.imageFile20_Up, patientExamIdNo, "-20.jpg");

            ViewBag.imageFile21 = getImageFileUploaded(ViewBag.imageFile21_Up, patientExamIdNo, "-21.jpg");
            ViewBag.imageFile22 = getImageFileUploaded(ViewBag.imageFile22_Up, patientExamIdNo, "-22.jpg");
            ViewBag.imageFile23 = getImageFileUploaded(ViewBag.imageFile23_Up, patientExamIdNo, "-23.jpg");
            ViewBag.imageFile24 = getImageFileUploaded(ViewBag.imageFile24_Up, patientExamIdNo, "-24.jpg");
            ViewBag.imageFile25 = getImageFileUploaded(ViewBag.imageFile25_Up, patientExamIdNo, "-25.jpg");
            ViewBag.imageFile26 = getImageFileUploaded(ViewBag.imageFile26_Up, patientExamIdNo, "-26.jpg");
            ViewBag.imageFile27 = getImageFileUploaded(ViewBag.imageFile27_Up, patientExamIdNo, "-27.jpg");
            ViewBag.imageFile28 = getImageFileUploaded(ViewBag.imageFile28_Up, patientExamIdNo, "-28.jpg");
            ViewBag.imageFile29 = getImageFileUploaded(ViewBag.imageFile28_Up, patientExamIdNo, "-28.jpg");
            ViewBag.imageFile30 = getImageFileUploaded(ViewBag.imageFile30_Up, patientExamIdNo, "-30.jpg");
            ViewBag.imageFile31 = getImageFileUploaded(ViewBag.imageFile31_Up, patientExamIdNo, "-31.jpg");
            ViewBag.imageFile32 = getImageFileUploaded(ViewBag.imageFile32_Up, patientExamIdNo, "-32.jpg");

            return View(patientExam);
        } //-- 


        public ActionResult PrintSelectedExamImages(int? id, FormCollection checkBoxImages)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            SetViewBagFileUpStatus(patientExam);

            string patiendIDNo = patientExam.PatientID.ToString();
            string patientExamIdNo = patientExam.ExamId.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            string[] examImages = new string[32];
            int examImageCounter = 0;

            bool chkImage1 = checkImageSelected(checkBoxImages["chkImage1"]);
            if (ViewBag.imageFile01_Up && chkImage1)
            {
                string filename01 = patientExamIdNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
                examImages[examImageCounter] = filename01;
                ++examImageCounter;
            } //--

            bool chkImage2 = checkImageSelected(checkBoxImages["chkImage2"]);
            if (ViewBag.imageFile02_Up && chkImage2)
            {
                string filename02 = patientExamIdNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
                examImages[examImageCounter] = filename02;
                ++examImageCounter;
            } //--

            bool chkImage3 = checkImageSelected(checkBoxImages["chkImage3"]);
            if (ViewBag.imageFile03_Up && chkImage3)
            {
                string filename03 = patientExamIdNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
                examImages[examImageCounter] = filename03;
                ++examImageCounter;
            } //--

            bool chkImage4 = checkImageSelected(checkBoxImages["chkImage4"]);
            if (ViewBag.imageFile04_Up && chkImage4)
            {
                string filename04 = patientExamIdNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
                examImages[examImageCounter] = filename04;
                ++examImageCounter;
            } //--

            bool chkImage5 = checkImageSelected(checkBoxImages["chkImage5"]);
            if (ViewBag.imageFile05_Up && chkImage5)
            {
                string filename05 = patientExamIdNo + "-05.jpg";
                ViewBag.imageFile05 = filename05;
                examImages[examImageCounter] = filename05;
                ++examImageCounter;
            } //--

            bool chkImage6 = checkImageSelected(checkBoxImages["chkImage6"]);
            if (ViewBag.imageFile06_Up && chkImage6)
            {
                string filename06 = patientExamIdNo + "-06.jpg";
                ViewBag.imageFile06 = filename06;
                examImages[examImageCounter] = filename06;
                ++examImageCounter;
            } //--

            bool chkImage7 = checkImageSelected(checkBoxImages["chkImage7"]);
            if (ViewBag.imageFile07_Up && chkImage7)
            {
                string filename07 = patientExamIdNo + "-07.jpg";
                ViewBag.imageFile07 = filename07;
                examImages[examImageCounter] = filename07;
                ++examImageCounter;

            } //--

            bool chkImage8 = checkImageSelected(checkBoxImages["chkImage8"]);
            if (ViewBag.imageFile08_Up && chkImage8)
            {
                string filename08 = patientExamIdNo + "-08.jpg";
                ViewBag.imageFile08 = patientExamIdNo + "-08.jpg";
                examImages[examImageCounter] = filename08;
                ++examImageCounter;
            } //--

            bool chkImage9 = checkImageSelected(checkBoxImages["chkImage9"]);
            if (ViewBag.imageFile09_Up && chkImage9)
            {
                string filename09 = patientExamIdNo + "-09.jpg";
                ViewBag.imageFile09 = patientExamIdNo + "-09.jpg";
                examImages[examImageCounter] = filename09;
                ++examImageCounter;
            } //--

            bool chkImage10 = checkImageSelected(checkBoxImages["chkImage10"]);
            if (ViewBag.imageFile10_Up && chkImage10)
            {
                string filename10 = patientExamIdNo + "-10.jpg";
                ViewBag.imageFile10 = patientExamIdNo + "-10.jpg";
                examImages[examImageCounter] = filename10;
                ++examImageCounter;
            } //--

            bool chkImage11 = checkImageSelected(checkBoxImages["chkImage11"]);
            if (ViewBag.imageFile11_Up && chkImage11)
            {
                string filename11 = patientExamIdNo + "-11.jpg";
                ViewBag.imageFile11 = patientExamIdNo + "-11.jpg";
                examImages[examImageCounter] = filename11;
                ++examImageCounter;
            } //--

            bool chkImage12 = checkImageSelected(checkBoxImages["chkImage12"]);
            if (ViewBag.imageFile12_Up && chkImage12)
            {
                string filename12 = patientExamIdNo + "-12.jpg";
                ViewBag.imageFile12 = patientExamIdNo + "-12.jpg";
                examImages[examImageCounter] = filename12;
                ++examImageCounter;
            } //--

            bool chkImage13 = checkImageSelected(checkBoxImages["chkImage13"]);
            if (ViewBag.imageFile13_Up && chkImage13)
            {
                string filename13 = patientExamIdNo + "-13.jpg";
                ViewBag.imageFile13 = patientExamIdNo + "-13.jpg";
                examImages[examImageCounter] = filename13;
                ++examImageCounter;
            } //--

            bool chkImage14 = checkImageSelected(checkBoxImages["chkImage14"]);
            if (ViewBag.imageFile14_Up && chkImage14)
            {
                string filename14 = patientExamIdNo + "-14.jpg";
                ViewBag.imageFile14 = patientExamIdNo + "-14.jpg";
                examImages[examImageCounter] = filename14;
                ++examImageCounter;
            } //--

            bool chkImage15 = checkImageSelected(checkBoxImages["chkImage15"]);
            if (ViewBag.imageFile15_Up && chkImage15)
            {
                string filename15 = patientExamIdNo + "-15.jpg";
                ViewBag.imageFile15 = patientExamIdNo + "-15.jpg";
                examImages[examImageCounter] = filename15;
                ++examImageCounter;
            } //--

            bool chkImage16 = checkImageSelected(checkBoxImages["chkImage16"]);
            if (ViewBag.imageFile16_Up && chkImage16)
            {
                string filename16 = patientExamIdNo + "-16.jpg";
                ViewBag.imageFile16 = patientExamIdNo + "-16.jpg";
                examImages[examImageCounter] = filename16;
                ++examImageCounter;
            } //--

            bool chkImage17 = checkImageSelected(checkBoxImages["chkImage17"]);
            if (ViewBag.imageFile17_Up && chkImage17)
            {
                string filename17 = patientExamIdNo + "-17.jpg";
                ViewBag.imageFile17 = patientExamIdNo + "-17.jpg";
                examImages[examImageCounter] = filename17;
                ++examImageCounter;
            } //--

            bool chkImage18 = checkImageSelected(checkBoxImages["chkImage18"]);
            if (ViewBag.imageFile18_Up && chkImage18)
            {
                string filename18 = patientExamIdNo + "-18.jpg";
                ViewBag.imageFile18 = patientExamIdNo + "-18.jpg";
                examImages[examImageCounter] = filename18;
                ++examImageCounter;
            } //--

            bool chkImage19 = checkImageSelected(checkBoxImages["chkImage19"]);
            if (ViewBag.imageFile19_Up && chkImage19)
            {
                string filename19 = patientExamIdNo + "-19.jpg";
                ViewBag.imageFile19 = patientExamIdNo + "-19.jpg";
                examImages[examImageCounter] = filename19;
                ++examImageCounter;
            } //--

            bool chkImage20 = checkImageSelected(checkBoxImages["chkImage20"]);
            if (ViewBag.imageFile20_Up && chkImage20)
            {
                string filename20 = patientExamIdNo + "-20.jpg";
                ViewBag.imageFile20 = patientExamIdNo + "-20.jpg";
                examImages[examImageCounter] = filename20;
                ++examImageCounter;
            } //--

            bool chkImage21 = checkImageSelected(checkBoxImages["chkImage21"]);
            if (ViewBag.imageFile21_Up && chkImage21)
            {
                string filename21 = patientExamIdNo + "-21.jpg";
                ViewBag.imageFile21 = patientExamIdNo + "-21.jpg";
                examImages[examImageCounter] = filename21;
                ++examImageCounter;
            } //--

            bool chkImage22 = checkImageSelected(checkBoxImages["chkImage22"]);
            if (ViewBag.imageFile22_Up && chkImage22)
            {
                string filename22 = patientExamIdNo + "-22.jpg";
                ViewBag.imageFile22 = patientExamIdNo + "-22.jpg";
                examImages[examImageCounter] = filename22;
                ++examImageCounter;
            } //--

            bool chkImage23 = checkImageSelected(checkBoxImages["chkImage23"]);
            if (ViewBag.imageFile23_Up && chkImage23)
            {
                string filename23 = patientExamIdNo + "-23.jpg";
                ViewBag.imageFile23 = patientExamIdNo + "-23.jpg";
                examImages[examImageCounter] = filename23;
                ++examImageCounter;
            } //--

            bool chkImage24 = checkImageSelected(checkBoxImages["chkImage24"]);
            if (ViewBag.imageFile24_Up && chkImage24)
            {
                string filename24 = patientExamIdNo + "-24.jpg";
                ViewBag.imageFile24 = patientExamIdNo + "-24.jpg";
                examImages[examImageCounter] = filename24;
                ++examImageCounter;
            } //--

            bool chkImage25 = checkImageSelected(checkBoxImages["chkImage25"]);
            if (ViewBag.imageFile25_Up && chkImage25)
            {
                string filename25 = patientExamIdNo + "-25.jpg";
                ViewBag.imageFile25 = patientExamIdNo + "-25.jpg";
                examImages[examImageCounter] = filename25;
                ++examImageCounter;
            } //--

            bool chkImage26 = checkImageSelected(checkBoxImages["chkImage26"]);
            if (ViewBag.imageFile26_Up && chkImage26)
            {
                string filename26 = patientExamIdNo + "-26.jpg";
                ViewBag.imageFile26 = patientExamIdNo + "-26.jpg";
                examImages[examImageCounter] = filename26;
                ++examImageCounter;
            } //--

            bool chkImage27 = checkImageSelected(checkBoxImages["chkImage27"]);
            if (ViewBag.imageFile27_Up && chkImage27)
            {
                string filename27 = patientExamIdNo + "-27.jpg";
                ViewBag.imageFile27 = patientExamIdNo + "-27.jpg";
                examImages[examImageCounter] = filename27;
                ++examImageCounter;
            } //--

            bool chkImage28 = checkImageSelected(checkBoxImages["chkImage28"]);
            if (ViewBag.imageFile28_Up && chkImage28)
            {
                string filename28 = patientExamIdNo + "-28.jpg";
                ViewBag.imageFile28 = patientExamIdNo + "-28.jpg";
                examImages[examImageCounter] = filename28;
                ++examImageCounter;
            } //--

            bool chkImage29 = checkImageSelected(checkBoxImages["chkImage29"]);
            if (ViewBag.imageFile29_Up && chkImage29)
            {
                string filename29 = patientExamIdNo + "-29.jpg";
                ViewBag.imageFile29 = patientExamIdNo + "-29.jpg";
                examImages[examImageCounter] = filename29;
                ++examImageCounter;
            } //--

            bool chkImage30 = checkImageSelected(checkBoxImages["chkImage30"]);
            if (ViewBag.imageFile30_Up && chkImage30)
            {
                string filename30 = patientExamIdNo + "-30.jpg";
                ViewBag.imageFile30 = patientExamIdNo + "-30.jpg";
                examImages[examImageCounter] = filename30;
                ++examImageCounter;
            } //--

            bool chkImage31 = checkImageSelected(checkBoxImages["chkImage31"]);
            if (ViewBag.imageFile31_Up && chkImage31)
            {
                string filename31 = patientExamIdNo + "-31.jpg";
                ViewBag.imageFile31 = patientExamIdNo + "-31.jpg";
                examImages[examImageCounter] = filename31;
                ++examImageCounter;
            } //--

            bool chkImage32 = checkImageSelected(checkBoxImages["chkImage32"]);
            if (ViewBag.imageFile32_Up && chkImage32)
            {
                string filename32 = patientExamIdNo + "-32.jpg";
                ViewBag.imageFile32 = patientExamIdNo + "-32.jpg";
                examImages[examImageCounter] = filename32;
                ++examImageCounter;
            } //--


            ViewBag.ExamImageList = examImages;
            ViewBag.ExamImageCount = examImageCounter;
            return View(patientExam);
        }

        public bool checkImageSelected(string on_selected)
        {
            if (on_selected == null)
                return false;
            else
                return true;
        } //--

        [AllowAnonymous]
        public ActionResult PrintReportWithLogo(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;
            int id_key = utHelp.getPatientIdKey(patiendIDNo);

            PatientData patientData = db.PatientDatas.Find(id_key);

            ViewBag.PatientSex = utHelp.getGenderDefinition(patientData.Sex.ToString());
            ViewBag.Age = patientData.Age.ToString();
            ViewBag.Status = utHelp.getStatusDefinition(patientData.Status.ToString());

            string examReport = patientExam.ExamReport.ToString();
            ViewBag.ExamReport = examReport;

            return View(patientExam);

        } //--

        [AllowAnonymous]
        public ActionResult PrintPatientReport(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            PatientData patientData = db.PatientDatas.Find(patientExam.PatientID);

            ViewBag.PatientSex = utHelp.getGenderDefinition(patientData.Sex.ToString());
            ViewBag.Age = patientData.Age.ToString();
            ViewBag.Status = utHelp.getStatusDefinition(patientData.Status.ToString());

            string examReport = patientExam.ExamReport.ToString();
            ViewBag.ExamReport = examReport;

            return View(patientExam);

        } //--

        public string getImageFileUploaded(bool isUploaded, string patiendID, string filename_ext)
        {
            if (isUploaded)
            {
                return (patiendID + filename_ext);
            }
            else {
                return "";
            }

        } //--

        public bool UploadExamImagefile(string exam_image_filename, bool isfile_uploaded, HttpPostedFileBase file, bool up_status)
        {
            bool uploaded = up_status;

            if (file != null || isfile_uploaded)
            {
                if (file != null)
                {
                    string file_ext = Path.GetExtension(file.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + exam_image_filename);
                        file.SaveAs(path);
                        uploaded = true;
                        ViewBag.imageFile10_Up = true;
                    }
                }


            } //--

            return uploaded;
        } //--

        public ActionResult SendExamToRadiologist(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patientName = patientExam.getPatientName;
            string doctorName = patientExam.getDoctorName;
            string doctorEmail = patientExam.getRadDoctorEmail;
            string emailSubject, emailBody;
            
            emailSubject = "Ultrasound Exam - for Patient : " + patientName + " | Exam Id : " + patientExam.ExamId;
            emailBody = "Dear Dr. " + doctorName + ": \r\n\r\n";
            emailBody += "Find attached link below for exam images of the patient above ... " + "\r\n";
            emailBody += "https://www.jevegausdiagnostic.com/PatientExam/SignMedicalReport/" + id.ToString() + "\r\n\r\n";
            emailBody += "Kindly see and make your official report." + "\r\n";
            emailBody += "\r\n";
            emailBody += "Thanks and regards, " + "\r\n";
            emailBody += "JEVEGA Ultrasound Diagnostic ADMIN";

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.PatientExamId = patientExam.Id;
            ViewBag.RadDoctorEmail = doctorEmail;
            ViewBag.Posted = false;

            return View();

        } //--

        [HttpPost]
        public ActionResult SendExamToRadiologist(FormCollection formCollection)
        {
            string emailSubject = formCollection["mail_subject"].ToString();
            string emailBody = formCollection["mail_body"].ToString();
            string emailRadDoctor = formCollection["mail_rad_doctor"].ToString();

            //string emailAddressTo = "crisvega71@gmail.com";
            string emailAddressTo = emailRadDoctor;

            string emailAddressFrom = "JevegaUSadmin@jevegausdiagnostic.com";
            string emailAddFromPwd = "Crv@UE8903510";

            string responseMessage = "";

            responseMessage = utHelp.SendSMTPmail(emailSubject, emailBody, emailAddressTo, emailAddressFrom, emailAddFromPwd);
            ViewBag.ResponseMessage = responseMessage;

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.Posted = true;

            return View();
        } //--

        public bool checkExamImagesExist(int? exam_id)
        {
            bool exist = false;

            PatientExam patientExam = db.PatientExams.Find(exam_id);

            if (patientExam.Image1 == true) { exist = true;  return exist; }
            if (patientExam.Image2 == true) { exist = true; return exist; }
            if (patientExam.Image3 == true) { exist = true; return exist; }
            if (patientExam.Image4 == true) { exist = true; return exist; }
            if (patientExam.Image5 == true) { exist = true; return exist; }
            if (patientExam.Image6 == true) { exist = true; return exist; }
            if (patientExam.Image7 == true) { exist = true; return exist; }
            if (patientExam.Image8 == true) { exist = true; return exist; }
            if (patientExam.Image9 == true) { exist = true; return exist; }
            if (patientExam.Image10 == true) { exist = true; return exist; }

            if (patientExam.Image11 == true) { exist = true; return exist; }
            if (patientExam.Image12 == true) { exist = true; return exist; }
            if (patientExam.Image13 == true) { exist = true; return exist; }
            if (patientExam.Image14 == true) { exist = true; return exist; }
            if (patientExam.Image15 == true) { exist = true; return exist; }
            if (patientExam.Image16 == true) { exist = true; return exist; }
            if (patientExam.Image17 == true) { exist = true; return exist; }
            if (patientExam.Image18 == true) { exist = true; return exist; }
            if (patientExam.Image19 == true) { exist = true; return exist; }
            if (patientExam.Image20 == true) { exist = true; return exist; }

            if (patientExam.Image21 == true) { exist = true; return exist; }
            if (patientExam.Image22 == true) { exist = true; return exist; }
            if (patientExam.Image23 == true) { exist = true; return exist; }
            if (patientExam.Image24 == true) { exist = true; return exist; }
            if (patientExam.Image25 == true) { exist = true; return exist; }
            if (patientExam.Image26 == true) { exist = true; return exist; }
            if (patientExam.Image27 == true) { exist = true; return exist; }
            if (patientExam.Image28 == true) { exist = true; return exist; }
            if (patientExam.Image29 == true) { exist = true; return exist; }
            if (patientExam.Image30 == true) { exist = true; return exist; }
            if (patientExam.Image31 == true) { exist = true; return exist; }
            if (patientExam.Image32 == true) { exist = true; return exist; }

            return exist;

        } //--

        public ActionResult SendExamToOBDoctor(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patientName = patientExam.getPatientName;
            string OBdoctorName = "Teresitta VillaFuerte";
            
            string emailSubject, emailBody;

            emailSubject = "Ultrasound Exam - for Patient : " + patientName + " | Exam Id : " + patientExam.ExamId; ;
            emailBody = "Dear Dr. " + OBdoctorName + ": \r\n\r\n";
            emailBody += "Click on the link below to see exam images of the patient above ... " + "\r\n";
            emailBody += "https://www.jevegausdiagnostic.com/PatientExam/Details/" + id.ToString() + "\r\n\r\n";
            emailBody += "Thanks and regards, " + "\r\n";
            emailBody += "JEVEGA Ultrasound Diagnostic ADMIN";

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.PatientExamId = patientExam.Id;
            ViewBag.Posted = false;

            return View();

        } //--

        [HttpPost]
        public ActionResult SendExamToOBDoctor(FormCollection formCollection)
        {
            string emailSubject = formCollection["mail_subject"].ToString();
            string emailBody = formCollection["mail_body"].ToString();

            string emailAddressTo = "crisvega71@gmail.com";
            string emailAddressFrom = "JevegaUSadmin@jevegausdiagnostic.com";
            string emailAddFromPwd = "Crv@UE8903510";

            //string OBemailAddress = "crisvega71@gmail.com";
            string OBemailAddress = "teresitavillafuerte@yahoo.com";
            emailAddressTo = OBemailAddress;

            string responseMessage = "";
            responseMessage = utHelp.SendSMTPmail(emailSubject, emailBody, emailAddressTo, emailAddressFrom, emailAddFromPwd);
            ViewBag.ResponseMessage = responseMessage;
            ViewBag.Posted = true;

            return View();
        } //--

        public ActionResult SendExamReportToOBDoctor(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patientName = patientExam.getPatientName;
            string OBdoctorName = "Teresitta";
            string OBemailAddress = "crisvega71@gmail.com";

            string responseMessage = "";
            string emailSubject, emailBody, emailAddressTo, emailAddressFrom, emailAddFromPwd;

            emailSubject = "Official Report of Ultrasound Exam - for Patient : " + patientName;
            emailBody = "Dear Dr. " + OBdoctorName + " \r\n\r\n";
            emailBody += "Click on the link below to see the official report of diagnostic examination for the patient above ... " + "\r\n\r\n";
            
            emailBody += "https://jevegausdiagnostic.com/PatientExam/PrintReportWithLogo/" + id.ToString() + "\r\n\r\n";

            emailBody += "Thanks and regards, " + "\r\n";
            emailBody += "JEVEGA Ultrasound Diagnostic ADMIN";

            emailAddressTo = OBemailAddress;
            emailAddressFrom = "JevegaUSadmin@jevegausdiagnostic.com";
            emailAddFromPwd = "Crv@UE8903510";

            responseMessage = utHelp.SendSMTPmail(emailSubject, emailBody, emailAddressTo, emailAddressFrom, emailAddFromPwd);
            ViewBag.ResponseMessage = responseMessage;

            return View();
        } //--

        public ActionResult SendExamReportToPatient(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

            string patientName = patientExam.getPatientName;
            string patientEmailAddress = patientExam.getPatientEmail;

            string emailSubject, emailBody;

            emailSubject = "Official Report of Ultrasound Exam - for Patient : " + patientName;
            emailBody = "Dear " + patientName + " \r\n\r\n";
            emailBody += "Click on the link below to see the your official report of diagnostic examination ... " + "\r\n\r\n";

            emailBody += "https://jevegausdiagnostic.com/PatientExam/PrintPatientReport/" + id.ToString() + "\r\n\r\n";

            emailBody += "Thanks and regards, " + "\r\n";
            emailBody += "JEVEGA Ultrasound Diagnostic ADMIN";

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.PatientExamId = patientExam.Id;
            ViewBag.PatientEmail = patientEmailAddress;
            ViewBag.Posted = false;

            return View();
        } //--

        [HttpPost]
        public ActionResult SendExamReportToPatient(FormCollection formCollection)
        {
            string emailSubject = formCollection["mail_subject"].ToString();
            string emailBody = formCollection["mail_body"].ToString();
            string emailPatient = formCollection["mail_patient"].ToString();

            string emailAddressTo = emailPatient;

            string emailAddressFrom = "JevegaUSadmin@jevegausdiagnostic.com";
            string emailAddFromPwd = "Crv@UE8903510";

            string responseMessage = "";

            responseMessage = utHelp.SendSMTPmail(emailSubject, emailBody, emailAddressTo, emailAddressFrom, emailAddFromPwd);
            ViewBag.ResponseMessage = responseMessage;

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.Posted = true;

            return View();
        }

        public ActionResult SearchPatientExam()
        {
            string lastName = Request.QueryString["ln"];
            string firstName = Request.QueryString["fn"];
            string patientIDs = "";

            if (lastName.Length > 0 && firstName.Length == 0)
            {
                List<PatientData> patientData = db.PatientDatas.Where(p => p.Lastname.ToUpper().Contains(lastName.ToUpper())).ToList();

                int patient_count = patientData.Count;
                
                for (int i = 0; i < patient_count; i++)
                {
                    patientIDs = patientIDs + patientData[i].Patient_Id.ToString() + ",";
                }
            }
            return View(db.PatientExams.Where(p => p.PatientID.Contains(patientIDs)).ToList());

        } //--

        public ActionResult ShowPatientExam(string p_id)
        {
            PatientData patientData = db.PatientDatas.FirstOrDefault(p => p.Patient_Id == p_id);
            ViewBag.PatientName = patientData.Lastname + ", " + patientData.Firstname;

            List<PatientExam> patientExam = db.PatientExams.Where(p => p.PatientID == p_id).ToList();

            return View(patientExam);
        } //---

        public ActionResult UploadMultipleFiles(int? id)
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)  {  return HttpNotFound();     }

            ViewBag.patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.patientExamEntityKey = patientExam.Id;
            ViewBag.PatientName = patientExam.getPatientName.ToString();
            ViewBag.PatientExamName = patientExam.getExamName.ToString();
            ViewBag.PatientExamDate = patientExam.ExamDate.ToString();
            ViewBag.PatientExamId = patientExam.ExamId.ToString();

            return View();
        } //--


        [HttpPost]
        public ActionResult UploadMultipleExamImages(int patientExamId, string patientExamIdNo, HttpPostedFileBase[] file_exams)
        {
            string examImageFilename;
            int file_counter = 0;
            bool[] upload_status = new bool[32];

            foreach (HttpPostedFileBase file in file_exams)
            {
                if (file != null)
                {
                    file_counter++;
                    if (file_counter < 10)
                    {
                        examImageFilename = patientExamIdNo + "-0" + file_counter.ToString() + ".jpg";
                    }
                    else  {
                        examImageFilename = patientExamIdNo + "-" + file_counter.ToString() + ".jpg";
                    }

                    string file_ext = Path.GetExtension(file.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + examImageFilename);
                        file.SaveAs(path);
                        upload_status[file_counter - 1] = true;
                    }
                }
            }

            for (int n = file_counter-1; n < 32; n++)
            {
                upload_status[file_counter] = false;
            }
            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update PatientExam Set Image1 = @upload01, Image2 = @upload02, Image3 = @upload03, Image4 = @upload04, Image5 = @upload05, Image6 = @upload06, Image7 = @upload07, Image8 = @upload08, Image9 = @upload09, Image10 = @upload10, " +
                    "Image11 = @upload11, Image12 = @upload12, Image13 = @upload13, Image14 = @upload14, Image15 = @upload15, Image16 = @upload16, Image17 = @upload17, Image18 = @upload18, Image19 = @upload19, Image20 = @upload20, Image21 = @upload21, " +
                    "Image22 = @upload22, Image23 = @upload23,  Image24 = @upload24, Image25 = @upload25, Image26 = @upload26, Image27 = @upload27, Image28 = @upload28, Image29 = @upload29, Image30 = @upload30, Image31 = @upload31,  Image32 = @upload32 Where Id = @examId";

                sqlCmd.Parameters.AddWithValue("@upload01", upload_status[0]);
                sqlCmd.Parameters.AddWithValue("@upload02", upload_status[1]);
                sqlCmd.Parameters.AddWithValue("@upload03", upload_status[2]);
                sqlCmd.Parameters.AddWithValue("@upload04", upload_status[3]);
                sqlCmd.Parameters.AddWithValue("@upload05", upload_status[4]);
                sqlCmd.Parameters.AddWithValue("@upload06", upload_status[5]);
                sqlCmd.Parameters.AddWithValue("@upload07", upload_status[6]);
                sqlCmd.Parameters.AddWithValue("@upload08", upload_status[7]);
                sqlCmd.Parameters.AddWithValue("@upload09", upload_status[8]);
                sqlCmd.Parameters.AddWithValue("@upload10", upload_status[9]);

                sqlCmd.Parameters.AddWithValue("@upload11", upload_status[10]);
                sqlCmd.Parameters.AddWithValue("@upload12", upload_status[11]);
                sqlCmd.Parameters.AddWithValue("@upload13", upload_status[12]);
                sqlCmd.Parameters.AddWithValue("@upload14", upload_status[13]);
                sqlCmd.Parameters.AddWithValue("@upload15", upload_status[14]);
                sqlCmd.Parameters.AddWithValue("@upload16", upload_status[15]);
                sqlCmd.Parameters.AddWithValue("@upload17", upload_status[16]);
                sqlCmd.Parameters.AddWithValue("@upload18", upload_status[17]);
                sqlCmd.Parameters.AddWithValue("@upload19", upload_status[18]);
                sqlCmd.Parameters.AddWithValue("@upload20", upload_status[19]);

                sqlCmd.Parameters.AddWithValue("@upload21", upload_status[20]);
                sqlCmd.Parameters.AddWithValue("@upload22", upload_status[21]);
                sqlCmd.Parameters.AddWithValue("@upload23", upload_status[22]);
                sqlCmd.Parameters.AddWithValue("@upload24", upload_status[23]);
                sqlCmd.Parameters.AddWithValue("@upload25", upload_status[24]);
                sqlCmd.Parameters.AddWithValue("@upload26", upload_status[25]);
                sqlCmd.Parameters.AddWithValue("@upload27", upload_status[26]);
                sqlCmd.Parameters.AddWithValue("@upload28", upload_status[27]);
                sqlCmd.Parameters.AddWithValue("@upload29", upload_status[28]);
                sqlCmd.Parameters.AddWithValue("@upload30", upload_status[29]);
                sqlCmd.Parameters.AddWithValue("@upload31", upload_status[30]);
                sqlCmd.Parameters.AddWithValue("@upload32", upload_status[31]);

                sqlCmd.Parameters.AddWithValue("@examId", patientExamId);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return RedirectToAction("Details/" + patientExamId);

        } //--

        public void SetViewBagFileUpStatus(PatientExam patientExam)
        {

            ViewBag.imageFile01_Up = patientExam.Image1;
            ViewBag.imageFile02_Up = patientExam.Image2;
            ViewBag.imageFile03_Up = patientExam.Image3;
            ViewBag.imageFile04_Up = patientExam.Image4;
            ViewBag.imageFile05_Up = patientExam.Image5;
            ViewBag.imageFile06_Up = patientExam.Image6;
            ViewBag.imageFile07_Up = patientExam.Image7;
            ViewBag.imageFile08_Up = patientExam.Image8;
            ViewBag.imageFile09_Up = patientExam.Image9;
            ViewBag.imageFile10_Up = patientExam.Image10;

            ViewBag.imageFile11_Up = patientExam.Image11;
            ViewBag.imageFile12_Up = patientExam.Image12;
            ViewBag.imageFile13_Up = patientExam.Image13;
            ViewBag.imageFile14_Up = patientExam.Image14;
            ViewBag.imageFile15_Up = patientExam.Image15;
            ViewBag.imageFile16_Up = patientExam.Image16;
            ViewBag.imageFile17_Up = patientExam.Image17;
            ViewBag.imageFile18_Up = patientExam.Image18;
            ViewBag.imageFile19_Up = patientExam.Image19;
            ViewBag.imageFile20_Up = patientExam.Image20;

            ViewBag.imageFile21_Up = patientExam.Image21;
            ViewBag.imageFile22_Up = patientExam.Image22;
            ViewBag.imageFile23_Up = patientExam.Image23;
            ViewBag.imageFile24_Up = patientExam.Image24;
            ViewBag.imageFile25_Up = patientExam.Image25;
            ViewBag.imageFile26_Up = patientExam.Image26;
            ViewBag.imageFile27_Up = patientExam.Image27;
            ViewBag.imageFile28_Up = patientExam.Image28;
            ViewBag.imageFile29_Up = patientExam.Image29;
            ViewBag.imageFile30_Up = patientExam.Image30;
            ViewBag.imageFile31_Up = patientExam.Image31;
            ViewBag.imageFile32_Up = patientExam.Image32;

        } //-- 
        
        public string loadReportTemplate(int Id)
        {
            string report_template;

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Select * from ExamReportTemplate Where Id = @tempId";
                sqlCmd.Parameters.AddWithValue("@tempId", Id);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();

                SqlDataReader tempRdr = sqlCmd.ExecuteReader();
                tempRdr.Read();

                report_template = tempRdr["ReportWriteUps"].ToString();
            }

            ViewBag.ReportTemplate = report_template;

            //return View();

            return report_template;

        } //--

        public bool CheckAndSetImageStatusFilename(string patientExamIdNo)
        {
            bool examImagesExist = false;

            if (ViewBag.imageFile01_Up)
            {
                ViewBag.imageFile01 = patientExamIdNo + "-01.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile02_Up)
            {
                ViewBag.imageFile02 = patientExamIdNo + "-02.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile03_Up)
            {
                ViewBag.imageFile03 = patientExamIdNo + "-03.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile04_Up)
            {
                ViewBag.imageFile04 = patientExamIdNo + "-04.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patientExamIdNo + "-05.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patientExamIdNo + "-06.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patientExamIdNo + "-07.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patientExamIdNo + "-08.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patientExamIdNo + "-09.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patientExamIdNo + "-10.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile11_Up)
            {
                ViewBag.imageFile11 = patientExamIdNo + "-11.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile12_Up)
            {
                ViewBag.imageFile12 = patientExamIdNo + "-12.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile13_Up)
            {
                ViewBag.imageFile13 = patientExamIdNo + "-13.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile14_Up)
            {
                ViewBag.imageFile14 = patientExamIdNo + "-14.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile15_Up)
            {
                ViewBag.imageFile15 = patientExamIdNo + "-15.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile16_Up)
            {
                ViewBag.imageFile16 = patientExamIdNo + "-16.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile17_Up)
            {
                ViewBag.imageFile17 = patientExamIdNo + "-17.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile18_Up)
            {
                ViewBag.imageFile18 = patientExamIdNo + "-18.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile19_Up)
            {
                ViewBag.imageFile19 = patientExamIdNo + "-19.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile20_Up)
            {
                ViewBag.imageFile20 = patientExamIdNo + "-20.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile21_Up)
            {
                ViewBag.imageFile21 = patientExamIdNo + "-21.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile22_Up)
            {
                ViewBag.imageFile22 = patientExamIdNo + "-22.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile23_Up)
            {
                ViewBag.imageFile23 = patientExamIdNo + "-23.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile24_Up)
            {
                ViewBag.imageFile24 = patientExamIdNo + "-24.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile25_Up)
            {
                ViewBag.imageFile25 = patientExamIdNo + "-25.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile26_Up)
            {
                ViewBag.imageFile26 = patientExamIdNo + "-26.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile27_Up)
            {
                ViewBag.imageFile27 = patientExamIdNo + "-27.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile28_Up)
            {
                ViewBag.imageFile28 = patientExamIdNo + "-28.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile29_Up)
            {
                ViewBag.imageFile29 = patientExamIdNo + "-29.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile30_Up)
            {
                ViewBag.imageFile30 = patientExamIdNo + "-30.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile31_Up)
            {
                ViewBag.imageFile31 = patientExamIdNo + "-31.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile32_Up)
            {
                ViewBag.imageFile32 = patientExamIdNo + "-32.jpg";
                examImagesExist = true;
            } //--

            return examImagesExist;
        } //--

    }
}
