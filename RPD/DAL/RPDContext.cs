using System;
using RPD.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RPD.DAL
{
    public class RPDContext : ApplicationDbContext
    {
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Allergy> Allergys { get; set; }
        public virtual DbSet<Pantry> Pantrys { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
    }
}