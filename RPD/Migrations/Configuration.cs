namespace RPD.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RPD.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RPD.DAL.RPDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RPD.DAL.RPDContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Ingredients.AddOrUpdate(
             i => i.Name,
             new Ingredient { Name = "Chicken Breast", Calories = 100, Protien = 7, IsLiquid = false },
             new Ingredient { Name = "Beer", Calories = 100, Carbs = 8, IsLiquid = true, },
             new Ingredient { Name = "Apple", Calories = 60, Carbs = 23, IsLiquid = false, Fat = 0 }
           ); // Need to seed 50 common ingredients.

            context.Allergys.AddOrUpdate(
              a => a.Name,
              new Allergy { Name = "Egg" },
              new Allergy { Name = "Dairy" },
              new Allergy { Name = "Wheat" },
              new Allergy { Name = "Shellfish" },
              new Allergy { Name = "Tree Nuts" },
              new Allergy { Name = "Soy" },
              new Allergy { Name = "Peanuts" },
              new Allergy { Name = "Fish" }
            );

            //Need to Seed 20ish tags. (One pot, Easy, 15m, 30m, 1h, Paleo, Keto, Whole30, Smoothie, Juice)

            context.Recipes.AddOrUpdate(
                  r => r.Title,
                  new Recipe { Title = "Stuffed Chicken", Cuisine = "American", Serves = 2, ActiveTime = 30 },
                  new Recipe { Title = "Beer cheese dip", Cuisine = "American", Serves = 6, ActiveTime = 20 },
                  new Recipe { Title = "Apple Pie", Cuisine = "American", Serves = 8, ActiveTime = 75 }
                );
        }
    }
}
