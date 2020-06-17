using JEVEGA_Us_Cliniic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JEVEGA_Us_Cliniic.Controllers;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class ExamVideoController : Controller
    {
        private UsDBContext db = new UsDBContext();
        private JEVEGA_UsDbEntities dbEF = new JEVEGA_UsDbEntities();

        // GET: ExamVideo
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.ExamVideos.ToList());
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        }  //--

        // GET: ExamVideo/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ExamVideo examVideo = db.ExamVideos.Find(id);
            if (examVideo == null)
            { return HttpNotFound(); }

            return View(examVideo);
        }

        // GET: ExamVideo/Create
        public ActionResult Create()
        {
            ViewBag.ExamIdList = new SelectList(dbEF.PatientExams.OrderBy(o => o.ExamId), "ExamId", "ExamId");

            return View();
        }

        // POST: ExamVideo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Patient_IdKey,Patient_ExamId,VideoName,VideoFilename,DateUpload")] ExamVideo examVideo)
        {
            examVideo.Patient_IdKey = GetPatientIdKey(examVideo.Patient_ExamId);
            examVideo.DateUpload = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.ExamVideos.Add(examVideo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamIdList = new SelectList(dbEF.PatientExams.OrderBy(o => o.ExamId), "ExamId", "ExamId");

            return View(examVideo);
        }

        // GET: ExamVideo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExamVideo/Edit/5
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

        // GET: ExamVideo/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: ExamVideo/Delete/5
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
        } //--

        public ActionResult Video(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ExamVideo examVideo = db.ExamVideos.Find(id);
            if (examVideo == null)
            { return HttpNotFound(); }

            return View(examVideo);
        } //-- 

        public int GetPatientIdKey(string exam_id)
        {
            int idKey = 0;  string patiendId = ""; 

            PatientExam patientExam = dbEF.PatientExams.Where(p => p.ExamId == exam_id).First();
            patiendId = patientExam.PatientID.ToString();

            PatientData patientData = dbEF.PatientDatas.Where(p => p.Patient_Id == patiendId).First();
            idKey = patientData.Id;

            return idKey;
        } //--

        [HttpPost]
        public ActionResult Video([Bind(Include = "Id,Patient_IdKey,Patient_ExamId,VideoName")] ExamVideo examVideo, HttpPostedFileBase videoFile)
        {
            if (videoFile != null)
            {
                string videoFilename = "video-" + examVideo.Patient_ExamId.ToString() + ".avi";
                ViewBag.fileProfilePic = videoFilename;

                string file_ext = Path.GetExtension(videoFile.FileName);
                if (!InvalidFileExtension(file_ext))
                {
                    string path = Server.MapPath("~/ClinicVideo/" + videoFilename);
                    videoFile.SaveAs(path);

                    examVideo.VideoFilename = videoFilename;
                    examVideo.DateUpload = DateTime.Now;

                    db.Entry(examVideo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {   int pId_key = (int)examVideo.Patient_IdKey;

                    PatientData patientData = dbEF.PatientDatas.Find(pId_key);
                    ViewBag.PatientName = patientData.Lastname.ToString() + ", " + patientData.Firstname.ToString();

                    return View(examVideo);
                }

            } //--

            return RedirectToAction("Details/" + examVideo.Id.ToString(), "ExamVideo");

        } //--

        public ActionResult SendVideoToPatient(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ExamVideo examVideo = db.ExamVideos.Find(id);
            if (examVideo == null)
            { return HttpNotFound(); }

            string patientName = examVideo.getPatientName.ToString();
            string patientEmailAddress = examVideo.getPatientEmail.ToString();
            string examVideoName = examVideo.VideoName.ToString();

            string emailSubject, emailBody;

            emailSubject = "Ultrasound Exam Video - for : " + patientName + " | Title: " + examVideoName;
            emailBody = "Dear " + patientName + " \r\n\r\n";
            emailBody += "Click on the link below to see and download the video ... " + "\r\n\r\n";

            emailBody += "https://jevegausdiagnostic.com/ExamVideo/Details/" + id.ToString() + "\r\n\r\n";

            emailBody += "Thanks and regards, " + "\r\n";
            emailBody += "JEVEGA Ultrasound Diagnostic ADMIN";

            ViewBag.MailSubject = emailSubject;
            ViewBag.MailBody = emailBody;
            ViewBag.PatientEmail = patientEmailAddress;
            ViewBag.Posted = false;

            return View(examVideo);
        } //-- 

        [HttpPost]
        public ActionResult SendVideoToPatient(FormCollection formCollection)
        {
            UtilityHelper utHelp = new UtilityHelper();

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
        } //--

        public bool InvalidFileExtension(string file_ext)
        {
            string upCaseExt = file_ext.ToUpper();

            if (upCaseExt != ".AVI")
            {
                ViewBag.FileErrorMessage = "File Upload ERROR: Only AVI files are allowed to upload ... ";
                return true;
            }
            else
            {  return false;  }
        } //--

    }
}
