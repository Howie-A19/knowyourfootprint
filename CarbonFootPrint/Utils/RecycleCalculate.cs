using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarbonFootPrint.Models;

namespace CarbonFootPrint.Utils
{
    public class RecycleCalculate
    {
        private FootPrintOneEntities1 db = new FootPrintOneEntities1();



        public float getReducedCarbonValue(RecycleQuantity recycleQty)
        {
            float reducedCarbonValue = 0;

            float reducedGlassQty = calculateReducedRecycle(recycleQty.glassQty, "Glass");
            float reducedAluminiumQty = calculateReducedRecycle(recycleQty.aluminiumQty, "Aluminium");
            float reducedSteelQty = calculateReducedRecycle(recycleQty.steelQty, "Steel");
            float reducedPlasticsQty = calculateReducedRecycle(recycleQty.plasticsQty, "Plastics");
            float reducedPcQty = calculateReducedRecycle(recycleQty.pcQty, "Paper and Cardboard");
            float reducedOwcQty = calculateReducedRecycle(recycleQty.owcQty, "Organic Waste(composting)");
            float reducedOwdQty = calculateReducedRecycle(recycleQty.owdQty, "Organic Waste(digestion)");

            reducedCarbonValue = (reducedAluminiumQty + reducedGlassQty + reducedSteelQty + reducedPlasticsQty + reducedPcQty + reducedOwcQty + reducedOwdQty);

            return reducedCarbonValue;

        }


        public float getTotalCarbonValue(RecycleQuantity recycleQty)
        {
            float productionCarbonValue = 0;

            float totalGlassQty = calculateTotalRecycle(recycleQty.glassQty, "Glass");
            float totalAluminiumQty = calculateTotalRecycle(recycleQty.aluminiumQty, "Aluminium");
            float totalSteelQty = calculateTotalRecycle(recycleQty.steelQty, "Steel");
            float totalPlasticsQty = calculateTotalRecycle(recycleQty.plasticsQty, "Plastics");
            float totalPcQty = calculateTotalRecycle(recycleQty.pcQty, "Paper and Cardboard");
            float totalOwcQty = calculateTotalRecycle(recycleQty.owcQty, "Organic Waste(composting)");
            float totalOwdQty = calculateTotalRecycle(recycleQty.owdQty, "Organic Waste(digestion)");

            productionCarbonValue = (totalGlassQty + totalAluminiumQty + totalSteelQty + totalPlasticsQty + totalPcQty + totalOwcQty + totalOwdQty);

            return productionCarbonValue;
        }

        public float calculateTotalRecycle(float rcQty, String itemName)
        {
            float carbonValue = 0;
            var tempRecycle = db.Recycles.Where(c => c.Name == itemName.Trim()).FirstOrDefault();

            if (tempRecycle != null)
            {
                Recycle recycle = tempRecycle;
                carbonValue = rcQty * recycle.Carbon_Footprint_Primary;
            }

            return carbonValue;
        }

        public float calculateReducedRecycle(float rcQty, String itemName)
        {
            float carbonValue = 0;
            var tempRecycle = db.Recycles.Where(c => c.Name == itemName.Trim()).FirstOrDefault();

            if(tempRecycle != null)
            {
                Recycle recycle =tempRecycle;
                carbonValue = rcQty * recycle.Footprint_Differences;
            }

            return carbonValue;
        }


    }
}