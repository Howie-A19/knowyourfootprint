using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarbonFootPrint.Models;
using CarbonFootPrint.Utils;

namespace CarbonFootPrint.Controllers
{
    public class RecyclesController : Controller
    {
        private FootPrintOneEntities1 db = new FootPrintOneEntities1();

        // GET: Recycles
        public ActionResult Index()
        {
            var recycles = db.Recycles.Include(r => r.Category);
            return View(recycles.ToList());
        }

        public ActionResult RecycleMain()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecycleMain(RecycleQuantity recycleQty)
        {
            RecycleCalculate recycleCalc = new RecycleCalculate();


            //Reduced Recycle
            float finalReducedQty = recycleCalc.getReducedCarbonValue(recycleQty);


            //TotalRecycle
            float finalTotalQty = recycleCalc.getTotalCarbonValue(recycleQty);

            ViewBag.finalTotalQty = finalTotalQty + " kg of CO2";
            ViewBag.finalReducedQty = finalReducedQty + " kg of CO2";
            


            return View();
        }

        // GET: Recycles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recycle recycle = db.Recycles.Find(id);
            if (recycle == null)
            {
                return HttpNotFound();
            }
            return View(recycle);
        }

        // GET: Recycles/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind");
            return View();
        }

        // POST: Recycles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Carbon_Footprint_Primary,Carbon_Footprint_Secondary,Footprint_Differences,Image_Path,Suggestions,Category_Id")] Recycle recycle)
        {
            if (ModelState.IsValid)
            {
                db.Recycles.Add(recycle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", recycle.Category_Id);
            return View(recycle);
        }

        // GET: Recycles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recycle recycle = db.Recycles.Find(id);
            if (recycle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", recycle.Category_Id);
            return View(recycle);
        }

        // POST: Recycles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Carbon_Footprint_Primary,Carbon_Footprint_Secondary,Footprint_Differences,Image_Path,Suggestions,Category_Id")] Recycle recycle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recycle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", recycle.Category_Id);
            return View(recycle);
        }

        // GET: Recycles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recycle recycle = db.Recycles.Find(id);
            if (recycle == null)
            {
                return HttpNotFound();
            }
            return View(recycle);
        }

        // POST: Recycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recycle recycle = db.Recycles.Find(id);
            db.Recycles.Remove(recycle);
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
