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


        //GET: Foods list with consumed frequency
        public ActionResult FoodMain()
        {

           ViewBag.Category= new SelectList(db.Foods, "Category", "Category");

            var Values = new List<String> { "1-2 times a week" , "3-4 times a week" , "more than 5 days" };
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewFrequencyList = new SelectList(aList, "Value", "Data");

            return View();
        }

        //Compute and provide CO2 results based on food selection 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FoodMain(Food food)
        {
            String category = food.Category;
            String freq = food.frequency;
 

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

                       
            ViewBag.carbonValue = Math.Round((decimal)carbonValue, 2);
            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");


            var Values = new List<String> { "1-2 times a week", "3-4 times a week", "more than 5 days" };
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewFrequencyList = new SelectList(aList, "Value", "Data");

            return View();
        }

        //Advance feature to calculate co2 for specific food result
    public ActionResult FoodAdvance()
        {
            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");

            var nutritionValues = new List<String> { "Protein per gm", "Energy per Calories" };
            var nList = nutritionValues.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewNutritionList = new SelectList(nList, "Value", "Data");

            return View();
        }

        //Compute and provide co2 results based on food with additonal options.

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


            ViewBag.carbonValue = Math.Round((decimal)carbonValue, 2);
            ViewBag.Category = new SelectList(db.Foods, "Category", "Category");

            var nutritionValues = new List<String> { "Protein per gm", "Energy per Calories" };
            var nList = nutritionValues.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.ViewNutritionList = new SelectList(nList, "Value", "Data");

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
