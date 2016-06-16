using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RPD.Models
{
    public class Tag
    {
        [Key]
        public int TId { get; set; }
        public string Name { get; set; }
        public int TagId { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}