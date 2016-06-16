using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPD.Models
{
    public class Allergy
    {
        public string Name { get; set; }
        public List<string> Allergens { get; set; }


        [Key]
        public int AId { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

    }
}