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
        private footprintdbEntities db = new footprintdbEntities();

        // GET: Recycles
        public ActionResult Index()
        {
            var recycles = db.Recycles.Include(r => r.Category);
            return View(recycles.ToList());
        }

        //GET: Recycle items 
        public ActionResult RecycleMain()
        {
            return View();
        }


        //Compute co2 results based on recycle items and provides reuced level of co2
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult RecycleMain(RecycleQuantity recycleQty)
        {
            RecycleCalculate recycleCalc = new RecycleCalculate();


            //Reduced Recycle
            float finalReducedQty = recycleCalc.getReducedCarbonValue(recycleQty);


            //TotalRecycle
            float finalTotalQty = recycleCalc.getTotalCarbonValue(recycleQty);
            double reducedCO = finalTotalQty - finalReducedQty;
            decimal decimalValue = Math.Round((decimal)reducedCO, 2);
            

            ViewBag.finalTotalQty = finalTotalQty ;
            ViewBag.finalReducedQty = finalReducedQty ;
            ViewBag.reducedCO = decimalValue;
            


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
