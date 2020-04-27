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
    public class SubscribersController : Controller
    {
        private footprintdbEntities db = new footprintdbEntities();

        // GET: Subscribers
        public ActionResult Index()
        {
            var subscribers = db.Subscribers.Include(s => s.Tip);
            return View(subscribers.ToList());
        }


      
        //Allow users to enter mail-id and select category list to get subscribed
        public ActionResult _SubscriberPartial()
        {
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind");
            return View();
        }


       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
