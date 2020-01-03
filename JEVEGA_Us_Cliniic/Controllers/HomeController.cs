using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Utilities util = new Utilities();
            util.setWidth(10);
            util.setHeight(32);

            int lotArea = util.getArea();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Vision()
        {
            return View();
        }

        public ActionResult DiagnosticServices()
        {
            ViewBag.Message = "Diagnostic Services";
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult DataPrivacy()
        {
            return View();
        }
    }
}