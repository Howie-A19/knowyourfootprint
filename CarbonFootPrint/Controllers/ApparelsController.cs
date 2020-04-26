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
        private footprintdbEntities db = new footprintdbEntities();

        // GET: Apparels
        public ActionResult Index()
        {
            var apparels = db.Apparels.Include(a => a.Category);
            return View(apparels.ToList());
        }


        //GET: Clothes list
        public ActionResult ApparelMain()
        {
            ViewBag.ApparelOne = new SelectList(db.Apparels, "Name", "Name");
           
            return View();
        }

        //Compute and provide co2 results for the  lcothes along woth tips and sugggestions

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApparelMain(Apparel_List apparelList)
        {
            ApparelCalculate apparelCalc = new ApparelCalculate();

            float qtyOneCFP = apparelCalc.generalApparelCalculate(apparelList.apparelOne, apparelList.quantityOne);
    
            float choicesApparelCalculateOne = apparelCalc.choicesApparelCalculateOne(qtyOneCFP);
            float choicesApparelCalculateTwo = apparelCalc.choicesApparelCalculateTwo(qtyOneCFP);
            float choicesApparelCalculateThree = apparelCalc.choicesApparelCalculateThree(qtyOneCFP);
            float choicesApparelCalculateFour = apparelCalc.choicesApparelCalculateFF(qtyOneCFP);
            float choicesApparelCalculateFive = apparelCalc.choicesApparelCalculateFF(qtyOneCFP);


            ViewBag.actualCFP = qtyOneCFP;

            ViewBag.ecoEfficiency = Math.Round((decimal)choicesApparelCalculateOne, 2) ;
            ViewBag.durable = Math.Round((decimal)choicesApparelCalculateTwo, 2) ;
            ViewBag.cleanCloth = Math.Round((decimal)choicesApparelCalculateThree, 2) ;
            ViewBag.washClothesOrIncreaseWashingSize = Math.Round((decimal)choicesApparelCalculateFour, 2);
            ViewBag.reuseMore = Math.Round((decimal)choicesApparelCalculateFive, 2) ;

            ViewBag.ApparelOne = new SelectList(db.Apparels, "Name", "Name");
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
