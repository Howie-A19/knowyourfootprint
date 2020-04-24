using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarbonFootPrint.Models;

namespace CarbonFootPrint.Utils
{
    public class ApparelCalculate
    {
        private footprintdbEntities db = new footprintdbEntities();

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
        public float choicesApparelCalculateOne(float carbonValue)
        {
            
            float reducedCarbonValue = carbonValue - (carbonValue * 0.245f);
            float differenceCarbonValue = carbonValue - reducedCarbonValue;
             
            return differenceCarbonValue;

        }

        //Would you buy the apparels that are design for durability
        public float choicesApparelCalculateTwo(float carbonValue)
        {

            float reducedCarbonValue = carbonValue - (carbonValue * 0.273f);
            float differenceCarbonValue = carbonValue - reducedCarbonValue;

            return differenceCarbonValue;
        }

        //Would you clean clothing less frequency?
        public float choicesApparelCalculateThree(float carbonValue)
        {
           
            float reducedCarbonValue = carbonValue - (carbonValue * 0.039f);
            float differenceCarbonValue = carbonValue - reducedCarbonValue;

            return differenceCarbonValue;
        }

        // Would you wash your clothes at lower temperature (around room temp)?
        //Would you increase the size of washing/drying load?
        public float choicesApparelCalculateFF(float carbonValue)
        {
            
            float reducedCarbonValue = carbonValue - (carbonValue * 0.027f);
            float differenceCarbonValue = carbonValue - reducedCarbonValue;

            return differenceCarbonValue;
        }

        //Would you dispose less and reuse more?
        public float choicesApparelCalculateSix(float carbonValue)
        {
            
            float reducedCarbonValue = carbonValue - (carbonValue * 0.021f);
            float differenceCarbonValue = carbonValue - reducedCarbonValue;

            return differenceCarbonValue;
        }

    }
}