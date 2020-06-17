using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using JEVEGA_Us_Cliniic;
using System.Data.SqlClient;
using System.Configuration;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class CashDisbursementController : Controller
    {
        private JEVEGA_UsDbEntities db = new JEVEGA_UsDbEntities();

        UtilityHelper utHelp = new UtilityHelper();
        public string[] fullMonthName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        // GET: CashDisbursement
        public ActionResult Index()
        {
            if (Session["USER_TYPE"] == null)
            {
                return RedirectToAction("Login", "Users", new { @ReturnUrl = "/CashDisbursement/Index" });
            }

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
            listOfYears = utHelp.CreateListOfYears(this_year);

            List<SelectListItem> listOfMonths = new List<SelectListItem>();
            listOfMonths = utHelp.CreateListMonthNames(this_month);

            ViewBag.YearsList = listOfYears.ToList();
            ViewBag.MonthsList = listOfMonths.ToList();
            ViewBag.DateList = fullMonthName[this_month - 1] + " " + this_year;

            List<CashDisbursement> cashDisbursementList = db.CashDisbursements.Where(c => c.DateSpent.Month == this_month).ToList();

            decimal totalAmount = cashDisbursementList.Sum(a => a.Amount);
            string strTotalCashDisburseAmount = string.Format("{0:0,0.00}", totalAmount);
            ViewBag.TotalCashDisburseAmount = strTotalCashDisburseAmount;

            return View(cashDisbursementList);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string str_year = formCollection["ListOfYears"];
            int this_year = Convert.ToInt32(str_year);

            List<SelectListItem> listOfYears = new List<SelectListItem>();
            listOfYears = utHelp.CreateListOfYears(this_year);

            string str_month = formCollection["ListOfMonths"];
            int this_month = Convert.ToInt32(str_month);

            List<SelectListItem> listOfMonths = new List<SelectListItem>();
            listOfMonths = utHelp.CreateListMonthNames(this_month);

            ViewBag.YearsList = listOfYears.ToList();
            ViewBag.MonthsList = listOfMonths.ToList();
            ViewBag.DateList = fullMonthName[this_month - 1] + " " + this_year;

            List<CashDisbursement> cashDisbursementList = db.CashDisbursements.Where(c => c.DateSpent.Month == this_month).ToList();

            decimal totalAmount = cashDisbursementList.Sum(a => a.Amount);
            string strTotalCashDisburseAmount = string.Format("{0:0,0.00}", totalAmount);
            ViewBag.TotalCashDisburseAmount = strTotalCashDisburseAmount;

            return View(cashDisbursementList);

        } //--

        // GET: CashDisbursement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            if (cashDisbursement == null)
            {   return HttpNotFound();  }

            ViewBag.DetailMonth = cashDisbursement.DateSpent.Month.ToString();
            ViewBag.DetailYear = cashDisbursement.DateSpent.Year.ToString();
            ViewBag.imageCashFile = "cashimageref-" + cashDisbursement.Id.ToString() + ".jpg";

            return View(cashDisbursement);
        }

        public ActionResult ViewCashRef(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            if (cashDisbursement == null)
            { return HttpNotFound(); }

            ViewBag.imageCashFile = "cashimageref-" + cashDisbursement.Id.ToString() + ".jpg";

            return View(cashDisbursement);
        } //-- 

        // GET: CashDisbursement/Create
        public ActionResult Create()
        {
            string usertype = Session["USER_TYPE"].ToString();
            if (!utHelp.CheckAdminAccess(usertype))
            {
                return RedirectToAction("UnauthorizedAccess", "Users");
            }

            ViewBag.ExpenseCategories = new SelectList(db.ExpenseCategories, "Id", "ExpenseCategory1");

            return View();
        }

        // POST: CashDisbursement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemDescription,ItemCategory,Amount,DateSpent,ReferenceNo,ItemImage")] CashDisbursement cashDisbursement)
        {
            if (ModelState.IsValid)
            {
                db.CashDisbursements.Add(cashDisbursement);
                db.SaveChanges();
                string month = cashDisbursement.DateSpent.Month.ToString();

                return RedirectToAction("Index", new { mo = month });
            }

            ViewBag.ExpenseCategories = new SelectList(db.ExpenseCategories, "Id", "ExpenseCategory1");

            return View(cashDisbursement);
        }

        // GET: CashDisbursement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            if (cashDisbursement == null)
            {   return HttpNotFound();  }

            ViewBag.ExpenseCategories = new SelectList(db.ExpenseCategories, "Id", "ExpenseCategory1");
            return View(cashDisbursement);
        } //--

        // POST: CashDisbursement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemDescription,ItemCategory,Amount,DateSpent,ReferenceNo,ItemImage")] CashDisbursement cashDisbursement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashDisbursement).State = EntityState.Modified;
                db.SaveChanges();
                string month = cashDisbursement.DateSpent.Month.ToString();
                string year = cashDisbursement.DateSpent.Year.ToString();

                return RedirectToAction("Index", new { mo = month, yr = year });
            }
            return View(cashDisbursement);
        }

        // GET: CashDisbursement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            if (cashDisbursement == null)
            {
                return HttpNotFound();
            }
            return View(cashDisbursement);
        }

        // POST: CashDisbursement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            db.CashDisbursements.Remove(cashDisbursement);
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

        public ActionResult UploadCashRef(int id)
        {
            CashDisbursement cashDisbursement = db.CashDisbursements.Find(id);
            if (cashDisbursement == null)
            {
                return HttpNotFound();
            }

            ViewBag.DetailMonth = cashDisbursement.DateSpent.Month.ToString();
            ViewBag.DetailYear = cashDisbursement.DateSpent.Year.ToString();

            return View(cashDisbursement);
        } //==

        [HttpPost]
        public ActionResult UploadCashRefImage([Bind(Include = "Id")] CashDisbursement cashdisburse, HttpPostedFileBase fileCashRef)
        {
            if (fileCashRef != null)
            {
                string fileCashRefImage = "cashimageref-" + cashdisburse.Id.ToString() + ".jpg";
                ViewBag.fileCashRefImage = fileCashRefImage;

                string file_ext = Path.GetExtension(fileCashRef.FileName);
                if (!InvalidFileExtension(file_ext))
                {
                    string path = Server.MapPath("~/CashImages/" + fileCashRefImage);
                    fileCashRef.SaveAs(path);

                    string US_ConnStr = ConfigurationManager.ConnectionStrings["USClinic_ADO"].ConnectionString;
                    using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
                    {
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandText = "Update CashDisbursement Set ItemImage = @cashpic  Where Id = @Id";

                        sqlCmd.Parameters.AddWithValue("@cashpic", true);
                        sqlCmd.Parameters.AddWithValue("@Id", cashdisburse.Id);

                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();
                        int rowaffected = sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }

            } //--

            return RedirectToAction("Details/" + cashdisburse.Id.ToString(), "CashDisbursement");

        } //== 

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
