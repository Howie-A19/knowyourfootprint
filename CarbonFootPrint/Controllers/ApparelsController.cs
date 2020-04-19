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
    public class ApparelsController : Controller
    {
        private FootPrintOneEntities1 db = new FootPrintOneEntities1();

        // GET: Apparels
        public ActionResult Index()
        {
            var apparels = db.Apparels.Include(a => a.Category);
            return View(apparels.ToList());
        }



        public ActionResult ApparelMain()
        {
            ViewBag.ApparelOne = new SelectList(db.Apparels, "Name", "Name");
            ViewBag.ApparelTwo = new SelectList(db.Apparels, "Name", "Name");
            ViewBag.ApparelThree = new SelectList(db.Apparels, "Name", "Name");
           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApparelMain(Apparel_List apparelList)
        {
            ApparelCalculate apparelCalc = new ApparelCalculate();

            float qtyOneCFP = apparelCalc.generalApparelCalculate(apparelList.apparelOne, apparelList.quantityOne);
            float qtyTwoCFP = apparelCalc.generalApparelCalculate(apparelList.apparelTwo, apparelList.quantityTwo);
            float qtyThreeCFP = apparelCalc.generalApparelCalculate(apparelList.apparelThree, apparelList.quantityThree);
            
            float totalCFP = (qtyOneCFP + qtyTwoCFP + qtyThreeCFP);


            float choicesApparelCalculateOne = apparelCalc.choicesApparelCalculateOne(totalCFP, apparelList.optionOne);
            float choicesApparelCalculateTwo = apparelCalc.choicesApparelCalculateTwo(choicesApparelCalculateOne, apparelList.optionTwo);
            float choicesApparelCalculateThree = apparelCalc.choicesApparelCalculateThree(choicesApparelCalculateTwo, apparelList.optionThree);
            float choicesApparelCalculateFour = apparelCalc.choicesApparelCalculateFF(choicesApparelCalculateThree, apparelList.optionFour);
            float choicesApparelCalculateFive = apparelCalc.choicesApparelCalculateFF(choicesApparelCalculateFour, apparelList.optionFive);
            float finalCFP = apparelCalc.choicesApparelCalculateSix(choicesApparelCalculateFive, apparelList.optionSix);

            //float finalCFP = choicesApparelCalculateThree;

            ViewBag.totalCFP = totalCFP + " kg of CO2";
            ViewBag.finalCFP = finalCFP + " kg of CO2";
           

            ViewBag.ApparelOne = new SelectList(db.Apparels, "Name", "Name");
            ViewBag.ApparelTwo = new SelectList(db.Apparels, "Name", "Name");
            ViewBag.ApparelThree = new SelectList(db.Apparels, "Name", "Name");

            return View();

        }


        // GET: Apparels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apparel apparel = db.Apparels.Find(id);
            if (apparel == null)
            {
                return HttpNotFound();
            }
            return View(apparel);
        }

        // GET: Apparels/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind");
            return View();
        }

        // POST: Apparels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Avg_Carbon_Footprint,Image_Path,Suggestions,Category_Id")] Apparel apparel)
        {
            if (ModelState.IsValid)
            {
                db.Apparels.Add(apparel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", apparel.Category_Id);
            return View(apparel);
        }

        // GET: Apparels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apparel apparel = db.Apparels.Find(id);
            if (apparel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", apparel.Category_Id);
            return View(apparel);
        }

        // POST: Apparels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Avg_Carbon_Footprint,Image_Path,Suggestions,Category_Id")] Apparel apparel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apparel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", apparel.Category_Id);
            return View(apparel);
        }

        // GET: Apparels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apparel apparel = db.Apparels.Find(id);
            if (apparel == null)
            {
                return HttpNotFound();
            }
            return View(apparel);
        }

        // POST: Apparels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apparel apparel = db.Apparels.Find(id);
            db.Apparels.Remove(apparel);
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
