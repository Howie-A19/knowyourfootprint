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


        //Saves users mail-id into the database for sending mail to them based on their categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _SubscriberPartial([Bind(Include = "Mail_Id, Tips_Id")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                if (subscriber.Id == 0)
                {
                    var max = db.Subscribers.Max(i => i.Id);
                    var newId = 0;
                    newId = max + 1; 
                    
                    subscriber.Id = newId;
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges();

                    ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind", subscriber.Tips_Id);
                    ViewBag.Result = "Your mail id "+subscriber.Mail_Id+" has been Subscribed successfully";
                    ModelState.Clear();
                    return View();
                }
             
                
               
            }
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind", subscriber.Tips_Id);
            return View("Index");

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
