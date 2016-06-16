using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPD.Models
{
    public class Pantry
    {
        [Key]
        public int PId { get; set; }
        public ApplicationUser Belongs_to { get; set; }
        public List<Ingredient> Has { get; set; }
    }
}