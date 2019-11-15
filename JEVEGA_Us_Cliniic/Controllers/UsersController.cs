using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JEVEGA_Us_Cliniic;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class UsersController : Controller
    {
        private JEVEGA_USDB_Entities db = new JEVEGA_USDB_Entities();

        UtilityHelper utHelper = new UtilityHelper();
        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (usertype == "1")   //-- Admin user ... 
            {
                return View(db.Users.ToList());
            }
            else {  return RedirectToAction("UnauthorizedAccess", "Users");     }
        } //-- 

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Position,Email,MobileNo,Username,Password,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = sha256_hash(user.Password);
                user.Password = hashPassword;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Authorize]
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            User user = db.Users.Find(id);
            if (user == null)
            {   return HttpNotFound();}

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Position,Email,MobileNo,Username,Password,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = sha256_hash(user.Password);
                user.Password = hashPassword;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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


        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["USER_ID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        } //--

        [HttpPost]
        public ActionResult Login(User userInfo, string ReturnUrl)
        {
            string userID = userInfo.Username.ToString();
            string userPass = userInfo.Password.ToString();
            string hashPassword = "";

            User user_entity = db.Users.FirstOrDefault(usr => usr.Username == userID);
            if (user_entity != null)
            {
                string userEntityPwd = user_entity.Password.ToString();
                //string userEntitySaltcode = user_entity.SaltCode.ToString();

                hashPassword = sha256_hash(userPass);
                //hashPassword = userPass;

                if (userEntityPwd == hashPassword)
                {
                    Session["USER_ID"] = user_entity.Id;
                    Session["USER_NAME"] = userID;
                    Session["USER_TYPE"] = user_entity.UserType;
                    Session["USER_EMAIL"] = user_entity.Email;
                    
                    FormsAuthentication.SetAuthCookie(userID, false);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ViewBag.errorLogin = "Incorrect Password! Please try again.";
                    return View();
                }
            }
            else
            {
                ViewBag.errorLogin = "Invalid User Id! Please try again.";
                return View();
            }


        } //..**

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        } //--

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session["USER_ID"] = null;
            Session["USER_NAME"] = null;
            Session["USER_TYPE"] = null;
            Session["USER_EMAIL"] = null;

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult UnauthorizedAccess()
        {
            ViewBag.UnauthorizeMessage = utHelper.UnauthorizedAccessMessage();

            return View();
        }
    }
}
