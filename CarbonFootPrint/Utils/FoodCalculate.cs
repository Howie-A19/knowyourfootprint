using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarbonFootPrint.Models;

namespace CarbonFootPrint.Utils
{
    public class FoodCalculate
    {
        public float calCarbonUsingFoodFrequency(string frequency, Food food)
        {
            float carbonValue = 0;
            if(frequency.Equals("1-2 times a week"))
            {
                float val = 1.5F;
                carbonValue = val * ((food.PER_SERVING_gm * food.E1_ACFP_PER_100gm) / 1000) * 52;

            }else if(frequency.Equals("3-4 times a week")){
                float val = 3.5F;
                carbonValue = val * ((food.PER_SERVING_gm * food.E1_ACFP_PER_100gm) / 1000) * 52;
            }
            else
            {
                carbonValue = 5 * ((food.PER_SERVING_gm * food.E1_ACFP_PER_100gm) / 1000) * 52;
            }
            return carbonValue;

        }

        public float calCarbonUsingNutrition(string nutritionFact, float input, Food food)
        {
            float carbonValue = 0;

            if(nutritionFact.Equals("Protein per gm"))
            {
                carbonValue = (input * food.E2_ACFP_PROTEIN_PER_100gm) / 100;
            }
            else
            {
                carbonValue = (input * food.E3_ACFP_ENERGY_PER_100KCAL) / 1000;
            }

            return carbonValue;

        }
    }
}