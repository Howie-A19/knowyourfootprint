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

        // GET: Subscribers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        // GET: Subscribers/Create
        public ActionResult Create()
        {
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind");
            return View();
        }

        // POST: Subscribers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mail_Id,Tips_Id")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                db.Subscribers.Add(subscriber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind", subscriber.Tips_Id);
            return View(subscriber);
        }


        public ActionResult _SubscriberPartial()
        {
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind");
            return View();
        }

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


        // GET: Subscribers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind", subscriber.Tips_Id);
            return View(subscriber);
        }

        // POST: Subscribers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mail_Id,Tips_Id")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tips_Id = new SelectList(db.Tips, "Id", "Tips_Kind", subscriber.Tips_Id);
            return View(subscriber);
        }

        // GET: Subscribers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        // POST: Subscribers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            db.Subscribers.Remove(subscriber);
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
    }
}
