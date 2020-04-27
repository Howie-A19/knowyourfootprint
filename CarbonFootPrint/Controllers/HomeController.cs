using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarbonFootPrint.Models;


namespace CarbonFootPrint.Controllers
{
   [BasicAuthentication("HLPA", "HLPA", BasicRealm = "localhost")]
    public class HomeController : Controller
    {
        private footprintdbEntities db = new footprintdbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult CookiesPolicy()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}