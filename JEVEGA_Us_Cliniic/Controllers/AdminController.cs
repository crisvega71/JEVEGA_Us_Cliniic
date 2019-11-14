using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Patients()
        {
            return View();
        }
    }
}