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
        private JEVEGA_USDB_Entities db = new JEVEGA_USDB_Entities();
        UtilityHelper utHelp = new UtilityHelper();

        // GET: PatientExam
        public ActionResult Index()
        {
            return View(db.PatientExams.ToList());
        }

        // GET: PatientExam/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {   return HttpNotFound();  }

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

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            if (ViewBag.imageFile01_Up)
            {
                string filename01 = patiendIDNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
            } //--

            if (ViewBag.imageFile02_Up)
            {
                string filename02 = patiendIDNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
            } //--

            if (ViewBag.imageFile03_Up)
            {
                string filename03 = patiendIDNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
            } //--

            if (ViewBag.imageFile04_Up)
            {
                string filename04 = patiendIDNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
            } //--

            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patiendIDNo + "-05.jpg";
            } //--

            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patiendIDNo + "-06.jpg";
            } //--

            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patiendIDNo + "-07.jpg";
            } //--

            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patiendIDNo + "-08.jpg";
            } //--

            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patiendIDNo + "-09.jpg";
            } //--

            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patiendIDNo + "-10.jpg";
            } //--

            return View(patientExam);
        }
        
        // GET: PatientExam/Create
        public ActionResult Create()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            ViewBag.PatientList = new SelectList(db.PatientDatas, "Patient_Id", "GetIDFullname");
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
        public ActionResult Create([Bind(Include = "Id,PatientID,ExamType,ExamDate,Sonographer,Radiologist,ExamReport,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,History")] PatientExam patientExam)
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

            return View(patientExam);
        }

        // POST: PatientExam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientID,ExamType,ExamDate,Sonographer,Radiologist,ExamReport,SignByDoctor,DateSigned,Image1,Image2,Image3,Image4,Image5,Image6,Image7,Image8,Image9,Image10,History")] PatientExam patientExam)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(patientExam).State = EntityState.Modified;
                //db.SaveChanges();

                string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;
                using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "Update PatientExam Set ExamDate = @exam_date, ExamType = @exam_type, History = @history, Radiologist = @doctor_id, Sonographer = @sono_id Where Id = @examId";

                    sqlCmd.Parameters.AddWithValue("@exam_date", patientExam.ExamDate);
                    sqlCmd.Parameters.AddWithValue("@exam_type", patientExam.ExamType);
                    sqlCmd.Parameters.AddWithValue("@history", patientExam.History);
                    sqlCmd.Parameters.AddWithValue("@doctor_id", patientExam.Radiologist);
                    sqlCmd.Parameters.AddWithValue("@sono_id", patientExam.Sonographer);
                    sqlCmd.Parameters.AddWithValue("@examId", patientExam.Id);

                    sqlCmd.Connection = sqlCon;
                    sqlCon.Open();
                    int rowaffected = sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                return RedirectToAction("Index");
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
            ViewBag.patientExamId = patientExam.Id;
            ViewBag.PatientName = patientExam.getPatientIdName.ToString();
            ViewBag.PatientExamName = patientExam.getExamName.ToString();
            ViewBag.PatientExamDate = patientExam.ExamDate.ToString();

            ViewBag.imageFile01_Up = patientExam.Image1;
            if (ViewBag.imageFile01_Up)
            {
                ViewBag.imageFile01 = patientExam.PatientID + "-" + "01.jpg";
            }

            ViewBag.imageFile02_Up = patientExam.Image2;
            if (ViewBag.imageFile02_Up)
            {
                ViewBag.imageFile02 = patientExam.PatientID + "-" + "02.jpg";
            }

            ViewBag.imageFile03_Up = patientExam.Image3;
            if (ViewBag.imageFile03_Up)
            {
                ViewBag.imageFile03 = patientExam.PatientID + "-" + "03.jpg";
            }

            ViewBag.imageFile04_Up = patientExam.Image4;
            if (ViewBag.imageFile04_Up)
            {
                ViewBag.imageFile04 = patientExam.PatientID + "-" + "04.jpg";
            }

            ViewBag.imageFile05_Up = patientExam.Image5;
            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patientExam.PatientID + "-" + "05.jpg";
            }

            ViewBag.imageFile06_Up = patientExam.Image6;
            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patientExam.PatientID + "-" + "06.jpg";
            }

            ViewBag.imageFile07_Up = patientExam.Image7;
            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patientExam.PatientID + "-" + "07.jpg";
            }

            ViewBag.imageFile08_Up = patientExam.Image8;
            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patientExam.PatientID + "-" + "08.jpg";
            }

            ViewBag.imageFile09_Up = patientExam.Image9;
            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patientExam.PatientID + "-" + "09.jpg";
            }

            ViewBag.imageFile10_Up = patientExam.Image10;
            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patientExam.PatientID + "-" + "10.jpg";
            }

            return View(patientExam);

        } //--

        [HttpPost]
        public ActionResult UploadScanImages(int patientExamId, string patiendIDNo, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5, HttpPostedFileBase file6, HttpPostedFileBase file7, HttpPostedFileBase file8, HttpPostedFileBase file9, HttpPostedFileBase file10)
        {
            PatientExam patientExam = db.PatientExams.Find(patientExamId);

            if (patientExam == null)
            {   return HttpNotFound();  }

            string patient_id_number = patientExam.PatientID.ToString();
            bool upload01, upload02, upload03, upload04, upload05, upload06, upload07, upload08, upload09, upload10;

            ViewBag.patiendIDNo = patient_id_number;
            ViewBag.patientExamId = patientExam.Id;
            ViewBag.PatientName = patientExam.getPatientIdName.ToString();
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

            if (file1 != null || ViewBag.imageFile01_Up)
            {
                string filename01 = patiendIDNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;

                if (file1 != null)
                {   string file_ext = Path.GetExtension(file1.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename01);
                        file1.SaveAs(path);
                        upload01 = true;
                        ViewBag.imageFile01_Up = true;
                    }
                }
            } //-- 

            if (file2 != null || ViewBag.imageFile02_Up)
            {
                string filename02 = patiendIDNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;

                if (file2 != null)
                {
                    string file_ext = Path.GetExtension(file2.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename02);
                        file2.SaveAs(path);
                        upload02 = true;
                        ViewBag.imageFile02_Up = true;
                    }
                }
            } //--

            if (file3 != null || ViewBag.imageFile03_Up)
            {
                string filename03 = patiendIDNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;

                if (file3 != null)
                {
                    string file_ext = Path.GetExtension(file3.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename03);
                        file3.SaveAs(path);
                        upload03 = true;
                        ViewBag.imageFile03_Up = true;
                    }
                }
            } //--

            if (file4 != null || ViewBag.imageFile04_Up)
            {
                string filename04 = patiendIDNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;

                if (file4 != null)
                {
                    string file_ext = Path.GetExtension(file4.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename04);
                        file4.SaveAs(path);
                        upload04 = true;
                        ViewBag.imageFile04_Up = true;
                    }
                }
            } //--

            if (file5 != null || ViewBag.imageFile05_Up)
            {
                string filename05 = patiendIDNo + "-05.jpg";
                ViewBag.imageFile05 = filename05;

                if (file5 != null)
                {
                    string file_ext = Path.GetExtension(file5.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename05);
                        file5.SaveAs(path);
                        upload05 = true;
                        ViewBag.imageFile05_Up = true;
                    }
                }
            } //--

            if (file6 != null || ViewBag.imageFile06_Up)
            {
                string filename06 = patiendIDNo + "-06.jpg";
                ViewBag.imageFile06 = filename06;

                if (file6 != null)
                {
                    string file_ext = Path.GetExtension(file6.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename06);
                        file6.SaveAs(path);
                        upload06 = true;
                        ViewBag.imageFile06_Up = true;
                    }
                }
            } //--

            if (file7 != null || ViewBag.imageFile07_Up)
            {
                string filename07 = patiendIDNo + "-07.jpg";
                ViewBag.imageFile07 = filename07;

                if (file7 != null)
                {
                    string file_ext = Path.GetExtension(file6.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename07);
                        file7.SaveAs(path);
                        upload07 = true;
                        ViewBag.imageFile07_Up = true;
                    }
                }
            } //--

            if (file8 != null || ViewBag.imageFile08_Up)
            {
                string filename08 = patiendIDNo + "-08.jpg";
                ViewBag.imageFile08 = filename08;

                if (file8 != null)
                {   string file_ext = Path.GetExtension(file8.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename08);
                        file8.SaveAs(path);
                        upload08 = true;
                        ViewBag.imageFile08_Up = true;
                    }
                }
            } //--

            if (file9 != null || ViewBag.imageFile09_Up)
            {
                string filename09 = patiendIDNo + "-09.jpg";
                ViewBag.imageFile09 = filename09;

                if (file9 != null)
                {   string file_ext = Path.GetExtension(file9.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {   string path = Server.MapPath("~/ExamImages/" + filename09);
                        file9.SaveAs(path);
                        upload09 = true;
                        ViewBag.imageFile09_Up = true;
                    }
                }
            } //--

            if (file10 != null || ViewBag.imageFile10_Up)
            {
                string filename10 = patiendIDNo + "-10.jpg";
                ViewBag.imageFile10 = filename10;

                if (file10 != null)
                {   string file_ext = Path.GetExtension(file10.FileName);
                    if (!InvalidFileExtension(file_ext))
                    {
                        string path = Server.MapPath("~/ExamImages/" + filename10);
                        file10.SaveAs(path);
                        upload10 = true;
                        ViewBag.imageFile10_Up = true;
                    }
                }
            } //--

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString; ;

            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update PatientExam Set Image1 = @upload01, Image2 = @upload02, Image3 = @upload03, Image4 = @upload04, Image5 = @upload05, Image6 = @upload06, Image7 = @upload07, Image8 = @upload08, Image9 = @upload09, Image10 = @upload10 Where Id = @examId";

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
                sqlCmd.Parameters.AddWithValue("@examId", patientExamId);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return View();
        }


        [HttpGet]
        public ActionResult SignMedicalReport(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            {   return HttpNotFound();  }

            bool examImagesExist = false;
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

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            if (ViewBag.imageFile01_Up)
            {
                string filename01 = patiendIDNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile02_Up)
            {
                string filename02 = patiendIDNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile03_Up)
            {
                string filename03 = patiendIDNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile04_Up)
            {
                string filename04 = patiendIDNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patiendIDNo + "-05.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patiendIDNo + "-06.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patiendIDNo + "-07.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patiendIDNo + "-08.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patiendIDNo + "-09.jpg";
                examImagesExist = true;
            } //--

            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patiendIDNo + "-10.jpg";
                examImagesExist = true;
            } //--

            ViewBag.ExamImageExist = examImagesExist;
            if (!examImagesExist)
            {
                ViewBag.ReportImageError = "Cannot create a report without an exam images!";
            }

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

            PatientData patientData = db.PatientDatas.Find(patientExam.PatientID);

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

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            if (ViewBag.imageFile01_Up)
            {
                string filename01 = patiendIDNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
            } //--

            if (ViewBag.imageFile02_Up)
            {
                string filename02 = patiendIDNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
            } //--

            if (ViewBag.imageFile03_Up)
            {
                string filename03 = patiendIDNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
            } //--

            if (ViewBag.imageFile04_Up)
            {
                string filename04 = patiendIDNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
            } //--

            if (ViewBag.imageFile05_Up)
            {
                ViewBag.imageFile05 = patiendIDNo + "-05.jpg";
            } //--

            if (ViewBag.imageFile06_Up)
            {
                ViewBag.imageFile06 = patiendIDNo + "-06.jpg";
            } //--

            if (ViewBag.imageFile07_Up)
            {
                ViewBag.imageFile07 = patiendIDNo + "-07.jpg";
            } //--

            if (ViewBag.imageFile08_Up)
            {
                ViewBag.imageFile08 = patiendIDNo + "-08.jpg";
            } //--

            if (ViewBag.imageFile09_Up)
            {
                ViewBag.imageFile09 = patiendIDNo + "-09.jpg";
            } //--

            if (ViewBag.imageFile10_Up)
            {
                ViewBag.imageFile10 = patiendIDNo + "-10.jpg";
            } //--

            return View(patientExam);
        } //-- 


        public ActionResult PrintSelectedExamImages(int? id, FormCollection checkBoxImages)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            PatientExam patientExam = db.PatientExams.Find(id);
            if (patientExam == null)
            { return HttpNotFound(); }

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

            string patiendIDNo = patientExam.PatientID.ToString();
            ViewBag.PatientIdNo = patiendIDNo;

            string[] examImages = new string[10];
            int examImageCounter = 0;

            bool chkImage1 = checkImageSelected(checkBoxImages["chkImage1"]);
            if (ViewBag.imageFile01_Up && chkImage1)
            {
                string filename01 = patiendIDNo + "-01.jpg";
                ViewBag.imageFile01 = filename01;
                examImages[examImageCounter] = filename01;
                ++examImageCounter;
            } //--

            bool chkImage2 = checkImageSelected(checkBoxImages["chkImage2"]);
            if (ViewBag.imageFile02_Up && chkImage2)
            {
                string filename02 = patiendIDNo + "-02.jpg";
                ViewBag.imageFile02 = filename02;
                examImages[examImageCounter] = filename02;
                ++examImageCounter;
            } //--

            bool chkImage3 = checkImageSelected(checkBoxImages["chkImage3"]);
            if (ViewBag.imageFile03_Up && chkImage3)
            {
                string filename03 = patiendIDNo + "-03.jpg";
                ViewBag.imageFile03 = filename03;
                examImages[examImageCounter] = filename03;
                ++examImageCounter;
            } //--

            bool chkImage4 = checkImageSelected(checkBoxImages["chkImage4"]);
            if (ViewBag.imageFile04_Up && chkImage4)
            {
                string filename04 = patiendIDNo + "-04.jpg";
                ViewBag.imageFile04 = filename04;
                examImages[examImageCounter] = filename04;
                ++examImageCounter;
            } //--

            bool chkImage5 = checkImageSelected(checkBoxImages["chkImage5"]);
            if (ViewBag.imageFile05_Up && chkImage5)
            {
                string filename05 = patiendIDNo + "-05.jpg";
                ViewBag.imageFile05 = filename05;
                examImages[examImageCounter] = filename05;
                ++examImageCounter;
            } //--

            bool chkImage6 = checkImageSelected(checkBoxImages["chkImage6"]);
            if (ViewBag.imageFile06_Up && chkImage6)
            {
                string filename06 = patiendIDNo + "-06.jpg";
                ViewBag.imageFile06 = filename06;
                examImages[examImageCounter] = filename06;
                ++examImageCounter;
            } //--

            bool chkImage7 = checkImageSelected(checkBoxImages["chkImage7"]);
            if (ViewBag.imageFile07_Up && chkImage7)
            {
                string filename07 = patiendIDNo + "-07.jpg";
                ViewBag.imageFile07 = patiendIDNo + "-07.jpg";
                examImages[examImageCounter] = filename07;
                ++examImageCounter;

            } //--

            bool chkImage8 = checkImageSelected(checkBoxImages["chkImage8"]);
            if (ViewBag.imageFile08_Up && chkImage8)
            {
                string filename08 = patiendIDNo + "-08.jpg";
                ViewBag.imageFile08 = patiendIDNo + "-08.jpg";
                examImages[examImageCounter] = filename08;
                ++examImageCounter;
            } //--

            bool chkImage9 = checkImageSelected(checkBoxImages["chkImage9"]);
            if (ViewBag.imageFile09_Up && chkImage9)
            {
                string filename09 = patiendIDNo + "-09.jpg";
                ViewBag.imageFile09 = patiendIDNo + "-09.jpg";
                examImages[examImageCounter] = filename09;
                ++examImageCounter;
            } //--

            bool chkImage10 = checkImageSelected(checkBoxImages["chkImage10"]);
            if (ViewBag.imageFile10_Up && chkImage10)
            {
                string filename10 = patiendIDNo + "-10.jpg";
                ViewBag.imageFile10 = patiendIDNo + "-10.jpg";
                examImages[examImageCounter] = filename10;
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

            PatientData patientData = db.PatientDatas.Find(patientExam.PatientID);

            ViewBag.PatientSex = utHelp.getGenderDefinition(patientData.Sex.ToString());
            ViewBag.Age = patientData.Age.ToString();
            ViewBag.Status = utHelp.getStatusDefinition(patientData.Status.ToString());

            string examReport = patientExam.ExamReport.ToString();
            ViewBag.ExamReport = examReport;

            return View(patientExam);

        } //-- 
    }
}
