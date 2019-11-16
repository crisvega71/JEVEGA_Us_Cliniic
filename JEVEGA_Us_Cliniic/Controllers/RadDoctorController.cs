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
    public class RadDoctorController : Controller
    {
        private JEVEGA_USDB_Entities db = new JEVEGA_USDB_Entities();

        // GET: RadDoctor
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.RadiologistDoctors.ToList());
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        // GET: RadDoctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            RadiologistDoctor radiologistDoctor = db.RadiologistDoctors.Find(id);
            if (radiologistDoctor == null)
            {   return HttpNotFound();    }

            ViewBag.profilePic_up = radiologistDoctor.ProfilePic;
            if (ViewBag.profilePic_up) {
                ViewBag.profilePic = "profilepic-" + radiologistDoctor.Id + ".jpg";
            }

            ViewBag.signature_up = radiologistDoctor.SignatureImage;
            if (ViewBag.signature_up)
            {
                ViewBag.signatureFile = "sign-" + radiologistDoctor.Id + ".jpg";
            }

            return View(radiologistDoctor);
        }

        // GET: RadDoctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RadDoctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PostTitle,Email,Phone,SignatureImage,ProfilePic,PrcLicenseNo")] RadiologistDoctor radiologistDoctor)
        {
            if (ModelState.IsValid)
            {
                db.RadiologistDoctors.Add(radiologistDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(radiologistDoctor);
        }

        // GET: RadDoctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            RadiologistDoctor radiologistDoctor = db.RadiologistDoctors.Find(id);
            if (radiologistDoctor == null)
            {   return HttpNotFound();}

            ViewBag.imageSign_Up = radiologistDoctor.SignatureImage;

            if (ViewBag.imageSign_Up)
            { ViewBag.imageSign = "sign-" + radiologistDoctor.Id.ToString() + ".jpg"; }
            else { ViewBag.imageSign = ""; }

            ViewBag.imageProfilePic_Up = radiologistDoctor.ProfilePic;

            if (ViewBag.imageProfilePic_Up)
            { ViewBag.imageProfilePic = "profilepic-" + radiologistDoctor.Id.ToString() + ".jpg"; }
            else { ViewBag.imageProfilePic = ""; }

            return View(radiologistDoctor);
        }

        // POST: RadDoctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PostTitle,Email,Phone,SignatureImage,ProfilePic,PrcLicenseNo")] RadiologistDoctor radiologistDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radiologistDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(radiologistDoctor);
        }

        // GET: RadDoctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RadiologistDoctor radiologistDoctor = db.RadiologistDoctors.Find(id);
            if (radiologistDoctor == null)
            {
                return HttpNotFound();
            }
            return View(radiologistDoctor);
        }

        // POST: RadDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RadiologistDoctor radiologistDoctor = db.RadiologistDoctors.Find(id);
            db.RadiologistDoctors.Remove(radiologistDoctor);
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

        public ActionResult UpdateRadiologist([Bind(Include = "Id,FirstName,LastName,PostTitle,Email,Phone,ProfilePic,SignatureImage,PrcLicenseNo")] RadiologistDoctor radiologistDoctor, HttpPostedFileBase fileSign, HttpPostedFileBase filePic)
        {
            bool upload_sign, upload_pic;

            upload_sign = (bool)radiologistDoctor.SignatureImage;
            upload_pic = (bool)radiologistDoctor.ProfilePic;

            if (fileSign != null)
            {
                string fileSignature = "sign-" + radiologistDoctor.Id.ToString() + ".jpg";
                ViewBag.fileSignature = fileSignature;

                string file_ext = Path.GetExtension(fileSign.FileName);
                if (!InvalidFileExtension(file_ext))
                {
                    string path = Server.MapPath("~/ClinicStaff/" + fileSignature);
                    fileSign.SaveAs(path);
                    upload_sign = true;
                    ViewBag.fileSign_Up = true;
                    radiologistDoctor.SignatureImage = upload_sign;
                }
            } //--

            if (filePic != null)
            {
                string fileProfilePic = "profilepic-" + radiologistDoctor.Id.ToString() + ".jpg";
                ViewBag.fileProfilePic = fileProfilePic;

                string file_ext = Path.GetExtension(filePic.FileName);
                if (!InvalidFileExtension(file_ext))
                {
                    string path = Server.MapPath("~/ClinicStaff/" + fileProfilePic);
                    filePic.SaveAs(path);
                    upload_pic = true;
                    ViewBag.fileProfilePic_Up = true;
                    radiologistDoctor.ProfilePic = upload_pic;
                }
            } //--

            string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update RadiologistDoctor Set FirstName = @firstname, LastName = @lastname, PostTitle = @post,  PrcLicenseNo = @prc, Email = @email, Phone = @phone, ProfilePic = @pic, SignatureImage = @sign  Where Id = @Id";

                sqlCmd.Parameters.AddWithValue("@firstname", radiologistDoctor.FirstName.ToString());
                sqlCmd.Parameters.AddWithValue("@lastname", radiologistDoctor.LastName.ToString());
                sqlCmd.Parameters.AddWithValue("@post", radiologistDoctor.PostTitle.ToString());
                sqlCmd.Parameters.AddWithValue("@prc", radiologistDoctor.PrcLicenseNo.ToString());
                sqlCmd.Parameters.AddWithValue("@email", radiologistDoctor.Email.ToString());
                sqlCmd.Parameters.AddWithValue("@phone", radiologistDoctor.Phone.ToString());
                sqlCmd.Parameters.AddWithValue("@pic", radiologistDoctor.ProfilePic);
                sqlCmd.Parameters.AddWithValue("@sign", radiologistDoctor.SignatureImage);

                sqlCmd.Parameters.AddWithValue("@Id", radiologistDoctor.Id);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return RedirectToAction("Details\\"+radiologistDoctor.Id.ToString(), "RadDoctor");
        } //-- 



        public bool InvalidFileExtension(string file_ext)
        {
            if (file_ext != ".jpg" && file_ext != ".png")
            {
                ViewBag.FileErrorMessage = "File Upload ERROR: Only JPG files are allowed to upload ... ";
                return true;
            }
            else
            { return false; }
        } //--

    }
}
