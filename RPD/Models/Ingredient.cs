using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPD.Models
{
    public class Ingredient
    {
        [Key]
        public int IId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string MeasureUnit { get; set; }
        public string BuySize { get; set; }
        public bool IsLiquid { get; set; }
        public int CupConversion { get; set; }
        public int Calories { get; set; }
        public List<Allergy> Allergies { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Protien { get; set; }



        public void SetCalories(int cal = -1)
        {
            if (cal != -1)
            {
                this.Calories = cal;
            }
            else
            {
                this.Calories = (this.Fat * 9) + (this.Carbs * 4) + (this.Protien * 4);
            }
        }

        public void SetAmount(int num = 0)
        {
            if (num > 0)
            {
                Amount = num;
            }
        }

        public void SetBuySize(int number, string unit = "grams")
        {
            this.BuySize = number.ToString() + " " + unit;
        }

        public void SetMeasure(string unit)
        {
            this.MeasureUnit = unit;
        }

        public void SetAllergies(List<Allergy> Allergys)
        {
            this.Allergies = Allergys;
        }

        public void SetAllergy(string allergy)
        {

            Allergy new_allergen = new Allergy();
            new_allergen.Name = allergy;
            new_allergen.Allergens = null;
            if (this.Allergies != null)
            {
                for (int i = 0; i < Allergies.Count(); i++)
                {
                    if (this.Allergies[i].Name == new_allergen.Name)
                    {
                        continue;
                    }
                    else
                    {
                        this.Allergies.Add(new_allergen);
                        return;
                    }
                }
            }
            else
            {
                this.Allergies = new List<Allergy> { new_allergen };
            }
        }

        public void SetIsLiquid(bool _bool)
        {
            this.IsLiquid = _bool;
        }

        public void SetCupConversion(int num)
        {

            if (this.IsLiquid == true)
            {
                this.SetMeasure("mls");
                this.CupConversion = Convert.ToInt32(num * 29.5735);
            }
            else
            {
                this.SetMeasure("g");
                this.CupConversion = Convert.ToInt32(num * 28.349);
            }
        }

        public string ConvertToGramsOrMls(int num, string measure)
        {
            measure = measure.ToLower();
            if (measure.LastIndexOf('s') == measure.Length - 1)
            {
                //removes 's' from plural units
                int last_s = measure.Length - 1;
                measure = measure.Remove(last_s);
            }
            if (measure == "kilogram" || measure == "kg" || measure == "liter" || measure == "litre")
            {
                return num.ToString() + " " + measure;
            }

            while (measure != "g" || measure != "ml")
            {
                if (measure == "ounce" || measure == "oz")
                {
                    if (this.IsLiquid == true)
                    {
                        measure = "ml";
                        this.SetMeasure("ml");
                        num = Convert.ToInt32(num * 29.5735);
                    }
                    else
                    {
                        num = Convert.ToInt32(num * 28.349);
                        measure = "g";
                    }
                }
                else if (measure == "cup" || measure == "c")
                {
                    if (this.IsLiquid == true)
                    {
                        measure = "ml";
                        this.SetMeasure("ml");
                        num = Convert.ToInt32(num * this.CupConversion);
                    }
                    else
                    {
                        num = Convert.ToInt32(num * this.CupConversion);
                        measure = "g";
                    }
                    break;
                }
                else if (measure == "pint" || measure == "pnt")
                {
                    num = num * 2;
                    measure = "cup";
                }
                else if (measure == "quart" || measure == "qt")
                {
                    num = num * 4;
                    measure = "cup";
                }
                else if (measure == "gallon" || measure == "gal" || measure == "pound" || measure == "lb")
                {
                    num = num * 16;
                    if (measure == "pound" || measure == "lb")
                    {
                        measure = "oz";
                    }
                    else
                    {
                        measure = "cup";
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            } // end while loop
            return num.ToString() + " " + measure;
            //I want to DRY this without risking infinite recursion.
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}