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
    public class FoodsController : Controller
    {
        private footprintdbEntities db = new footprintdbEntities();

        // GET: Foods
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.Category1);
            return View(foods.ToList());
        }


        public ActionResult FoodMain()
        {


           ViewBag.Category= new SelectList(db.Foods, "Category", "Category");


            var Values = new List<String> { "1-2 times a week" , "3-4 times a week" , "more than 5 days" };
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewFrequencyList = new SelectList(aList, "Value", "Data");

           

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FoodMain(Food food)
        {
            String category = food.Category;
            String freq = food.frequency;
           // String nutrition = food.nutrition;

            float carbonValue = 0;
            FoodCalculate foodCalc = new FoodCalculate();
   
            var tempFood = db.Foods.Where(c => c.Category == category.Trim()).FirstOrDefault();
            Food foodOne = tempFood;

            if(foodOne != null)
            {
                if (food.frequency != null)
                {
                    carbonValue = foodCalc.calCarbonUsingFoodFrequency(food.frequency, foodOne);
                }
               
            }

           

            ViewBag.carbonValue = carbonValue+ " kg of CO2";

            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");


            var Values = new List<String> { "1-2 times a week", "3-4 times a week", "more than 5 days" };
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewFrequencyList = new SelectList(aList, "Value", "Data");

          
            return View();

        }

    public ActionResult FoodAdvance()
        {
            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");


            var nutritionValues = new List<String> { "Protein per gm", "Energy per Calories" };
            var nList = nutritionValues.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewNutritionList = new SelectList(nList, "Value", "Data");

            return View();
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult FoodAdvance(Food food)
        {
            String category = food.Category;
            //String freq = food.frequency;
            String nutrition = food.nutrition;

            float carbonValue = 0;
            FoodCalculate foodCalc = new FoodCalculate();
          

            var tempFood = db.Foods.Where(c => c.Category == category.Trim()).FirstOrDefault();
            Food foodOne = tempFood;

            if (foodOne != null)
            {
               
                if (nutrition != null)
                {
                    carbonValue = foodCalc.calCarbonUsingNutrition(food.nutrition, food.input, foodOne);
                }
            }



            ViewBag.carbonValue = carbonValue + " kg of CO2";

            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");

            var nutritionValues = new List<String> { "Protein per gm", "Energy per Calories" };
            var nList = nutritionValues.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewNutritionList = new SelectList(nList, "Value", "Data");

            return View();


        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category,Energy_PER_100gm,PER_SERVING_gm,E1_ACFP_PER_100gm,PROTEIN_PER_100gm,E2_ACFP_PROTEIN_PER_100gm,E3_ACFP_ENERGY_PER_100KCAL,Image_Path,Suggestions,Category_Id")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", food.Category_Id);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", food.Category_Id);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,Energy_PER_100gm,PER_SERVING_gm,E1_ACFP_PER_100gm,PROTEIN_PER_100gm,E2_ACFP_PROTEIN_PER_100gm,E3_ACFP_ENERGY_PER_100KCAL,Image_Path,Suggestions,Category_Id")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Kind", food.Category_Id);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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
