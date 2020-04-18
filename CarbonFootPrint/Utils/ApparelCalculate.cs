using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarbonFootPrint.Models;

namespace CarbonFootPrint.Utils
{
    public class ApparelCalculate
    {
        private FootPrintOneEntities1 db = new FootPrintOneEntities1();

        public float generalApparelCalculate(String apparelName, float quantity)
        {
            float totalCarbonValue = 0;

            //var tempApparel = db.Apparels.Where(c => (c.Name == apparelName.Trim())).FirstOrDefault();
            var tempApparel = db.Apparels.Where(c => c.Name == apparelName.Trim()).ToList();
            if (tempApparel.Count != 0)
            {
                Apparel apparel = tempApparel[0];
                totalCarbonValue = quantity * apparel.Avg_Carbon_Footprint;
            }

            
            return totalCarbonValue;

        }

        //Would you buy the apparels from the brands that is Eco-efficiency across supply chain? 
        public float choicesApparelCalculateOne(float carbonValue, String check)
        {
            if (check.Equals("Yes"))
               carbonValue = carbonValue - (carbonValue * 0.245f);
             
            return carbonValue;

        }

        //Would you buy the apparels that are design for durability
        public float choicesApparelCalculateTwo(float carbonValue, String check)
        {
            if(check.Equals("Yes"))
            carbonValue = carbonValue - (carbonValue * 0.273f);

            return carbonValue;
        }

        //Would you clean clothing less frequency?
        public float choicesApparelCalculateThree(float carbonValue, String check)
        {
            if(check.Equals("Yes"))
            carbonValue = carbonValue - (carbonValue * 0.039f);

            return carbonValue;
        }

        // Would you wash your clothes at lower temperature (around room temp)?
        //Would you increase the size of washing/drying load?
        public float choicesApparelCalculateFF(float carbonValue, String check)
        {
            if (check.Equals("Yes"))
                carbonValue = carbonValue - (carbonValue * 0.027f);

            return carbonValue;
        }

        //Would you dispose less and reuse more?
        public float choicesApparelCalculateSix(float carbonValue, String check)
        {
            if (check.Equals("Yes"))
                carbonValue = carbonValue - (carbonValue * 0.021f);

            return carbonValue;
        }

    }
}