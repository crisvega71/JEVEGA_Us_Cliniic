using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JEVEGA_Us_Cliniic;

namespace JEVEGA_Us_Cliniic.Controllers
{
    [Authorize]
    public class SonographersController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();

        // GET: Sonographers
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.Sonographers.ToList());
            }
            else { return RedirectToAction("UnauthorizedAccess", "Users"); }

        } //--

        // GET: Sonographers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            Sonographer sonographer = db.Sonographers.Find(id);
            if (sonographer == null)
            {   return HttpNotFound();  }

            bool file_uploaded = (bool)sonographer.ProfilePic;
            if (file_uploaded)
            {
                ViewBag.ProfilePic = "sonographer-" + sonographer.Id.ToString() + ".jpg";
                ViewBag.profilePic_up = true;
            }
            else {
                ViewBag.profilePic_up = false;
            }

            return View(sonographer);
        }

        // GET: Sonographers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sonographers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PostTitle,Email,Phone")] Sonographer sonographer)
        {
            if (ModelState.IsValid)
            {
                db.Sonographers.Add(sonographer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sonographer);
        }

        // GET: Sonographers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sonographer sonographer = db.Sonographers.Find(id);
            if (sonographer == null)
            {
                return HttpNotFound();
            }
            return View(sonographer);
        }

        // POST: Sonographers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PostTitle,Email,Phone,ProfilePic")] Sonographer sonographer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sonographer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details\\" + sonographer.Id.ToString());
            }
            return View(sonographer);
        }

        // GET: Sonographers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sonographer sonographer = db.Sonographers.Find(id);
            if (sonographer == null)
            {
                return HttpNotFound();
            }
            return View(sonographer);
        }

        // POST: Sonographers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sonographer sonographer = db.Sonographers.Find(id);
            db.Sonographers.Remove(sonographer);
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

        [HttpGet]
        public ActionResult ProfilePic(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            Sonographer sonographer = db.Sonographers.Find(id);
            if (sonographer == null)
            {   return HttpNotFound();  }

            bool? profilepic_uploaded;
            if (sonographer.ProfilePic == null)
            {
                profilepic_uploaded = false;
            }
            else {
                profilepic_uploaded = sonographer.ProfilePic;
            }
            ViewBag.imageProfilePic_Up = profilepic_uploaded;
            
            if (ViewBag.imageProfilePic_Up)
            {
                ViewBag.imageProfilePic = "sonographer-" + id.ToString() +".jpg";
            }

            return View(sonographer);
        }

        [HttpPost]
        public ActionResult UploadSonoProfilePic([Bind(Include = "Id,FirstName,LastName")] Sonographer sonographer, HttpPostedFileBase filePic)
        {

            if (filePic != null)
            {
                string fileProfilePic = "sonographer-" + sonographer.Id.ToString() + ".jpg";
                ViewBag.fileProfilePic = fileProfilePic;

                string file_ext = Path.GetExtension(filePic.FileName);
                if (!InvalidFileExtension(file_ext))
                {
                    string path = Server.MapPath("~/ClinicStaff/" + fileProfilePic);
                    filePic.SaveAs(path);

                    string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString;
                    using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
                    {
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandText = "Update Sonographer Set ProfilePic = @profile_pic  Where Id = @Id";

                        sqlCmd.Parameters.AddWithValue("@profile_pic", true);
                        sqlCmd.Parameters.AddWithValue("@Id", sonographer.Id);

                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();
                        int rowaffected = sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }

            } //--

            return RedirectToAction("Details/"+ sonographer.Id.ToString(), "Sonographers");
        }


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
