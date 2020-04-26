using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarbonFootPrint.Models;

namespace CarbonFootPrint.Controllers
{
    public class TipsController : Controller
    {
        private footprintdbEntities db = new footprintdbEntities();

        // GET: Tips
        public ActionResult Index()
        {
            return View(db.Tips.ToList());
        }

     
    }
}