using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RPD.Models
{
    public class Recipe
    {
        [Key]
        public int RId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Cuisine { get; set; }

        [Required]
        public int Serves { get; set; }

        [Required]
        public int ActiveTime { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<string> ActiveInstructions { get; set; }
        public int Calories { get; set; }
        public int PrepTime { get; set; }
        public List<Tag> Tags { get; set; }
        public string Url { get; set; }
        public List<string> PrepInstructions { get; set; }

        public void SetCaloriesAndAllergies(int num = 999999)
        {
            if (num != 999999)
            {
                Calories = num;
            }
            List<Allergy> total_allergies = new List<Allergy>();
            for (int i = 0; i < Ingredients.Count(); i++)
            {
                var ing = Ingredients[i];
                if (num == 999999)
                {
                    Calories = Calories + ing.Calories;
                }
                total_allergies.AddRange(ing.Allergies.Distinct());
            }
            this.Allergies = total_allergies;
        }

        public override string ToString()
        {
            return Title.ToString();
        }
    }
}