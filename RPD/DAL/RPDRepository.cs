using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RPD.Models;

namespace RPD.DAL
{
    public class RPDRepository
    {
        public RPDContext context { get; set; }
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public ApplicationUser GetUser(string user_id)
        {
            return context.Users.FirstOrDefault(i => i.Id == user_id);
        }

        public void SetUsersPrefs(string uid, List<string> fave_cuisines = null, List<int> _allergies_ids = null, int pref_cals = 2000, List<Tag> pref_tags = null)
        {
            ApplicationUser user = GetUser(uid);
            user.FaveCuisines = fave_cuisines;
            user.Allergy_ids = _allergies_ids;
            user.CaloriePref = pref_cals;
            user.PrefTags = pref_tags;
            context.SaveChanges();

            //NEEDS TESTING
        }

        public RPDRepository()
        {
            context = new RPDContext();
        }

        public RPDRepository(RPDContext _context)
        {
            context = _context;
        }

        public int GetRecipeCount()
        {
            return context.Recipes.Count();
        }

        public List<Recipe> GetRecipes()
        {
            return context.Recipes.ToList();
        }

        public List<Tag> GetTags()
        {
            return context.Tags.ToList();
        }

        private bool IsInListOfTags(Tag tag, List<Tag> tags)
        {
            for (int i = 0; i < tags.Count(); i++)
            {
                if (tags[i].Name == tag.Name)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsInListOfIngredients(Ingredient ing, List<Ingredient> ings)
        {
            for (int i = 0; i < ings.Count(); i++)
            {
                if (ings[i].Name == ing.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Allergy> GetAllergies()
        {
            return context.Allergys.ToList();
        }

        private bool IsInListOfAllergies(Allergy allergy, List<Allergy> allergies)
        {
            for (int i = 0; i < allergies.Count(); i++)
            {
                if (allergies[i].Name == allergy.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Pantry> GetPantries()
        {
            return context.Pantrys.ToList();
        }

        public List<Ingredient> GetUsersPantry(string user_id)
        {
            ApplicationUser user = GetUser(user_id);
            var pantries = context.Pantrys;
            List<Ingredient> selected_pantry = pantries.FirstOrDefault(p => p.PId == user.MyPantry).Has;
            return selected_pantry;
        }

        public void AddRecipe(
            int serves,
            string title,
            string cuisine,
            int active_time,
            //everything above is required
            List<Ingredient> ingredients,
            List<Allergy> allergies,
            List<string> active_instructions,
            int prep_time = 20,
            List<Tag> tags = null,
            string url = "www.example.com/recipes",
            List<string> prep_instructions = null,
            int calories = 9999999
            )
        {
            Recipe new_recipe = new Recipe
            {
                Title = title,
                Cuisine = cuisine,
                Serves = serves,
                Ingredients = ingredients,
                Allergies = allergies,
                ActiveInstructions = active_instructions,
                ActiveTime = active_time,
                PrepTime = prep_time,
                PrepInstructions = prep_instructions,
                Tags = tags,
                Url = url,
                Calories = calories
            };
            context.Recipes.Add(new_recipe);
            context.SaveChanges();
        }

        public List<Recipe> GetRecipes(
            string uid = "",
            string cuisene = null,
            List<Tag> tags = null,
            List<Allergy> allergies = null,
            int serves = 0,
            int cals = 0, // I have to pass in perameters for prep time, active time and calories in my JS controller
            int cal_range = 75,
            int time_range = 10,
            int prep_time = 0,
            int active_time = 0,
            List<Ingredient> ingredients = null,
            int limit = 21)
        {
            IQueryable<Recipe> q = context.Recipes.AsQueryable<Recipe>();
            if (cuisene != null)
            {
                q = q.Where(recipe => recipe.Cuisine == cuisene);
            }
            if (tags != null)
            {
                q = q.Where(recipe => recipe.Tags.All(tag => IsInListOfTags(tag, tags)));
            }
            if (allergies != null)
            {
                q = q.Where(recipe => recipe.Allergies.All(allergy => IsInListOfAllergies(allergy, allergies) == false));
            }
            if (serves > 0)
            {
                q = q.Where(recipe => recipe.Serves == serves);
            }
            if (cals > 0)
            {
                q = q.Where(recipe => ((recipe.Calories >= (cals - cal_range)) && (recipe.Calories <= (cals + cal_range))));
            }
            if (prep_time > 0)
            {
                q = q.Where(recipe => ((recipe.PrepTime >= prep_time - time_range) && (recipe.PrepTime <= prep_time + time_range)));
            }
            if (active_time > 0)
            {
                q = q.Where(recipe => ((recipe.ActiveTime >= active_time - time_range) && (recipe.ActiveTime <= active_time + time_range)));
            }
            if (ingredients != null)
            {
                q = q.Where(recipe => recipe.Ingredients.All(ing => IsInListOfIngredients(ing, ingredients)));
            }
            var recs_returned = q.Take(limit).ToList(); //look into offset
            //ReturnShoppingList(recs_returned, uid);
            return recs_returned;
        }

        public List<Ingredient> ReturnShoppingList(List<Recipe> recipe_list, string uid)
        {
            List<Ingredient> ings_in_pantry = GetUsersPantry(uid);
            List<Ingredient> ings_in_rec_list = new List<Ingredient>();

            for (int i = 0; i < recipe_list.Count(); i++)
            {
                var recipe = recipe_list[i];
                ings_in_rec_list.AddRange(recipe.Ingredients);
            }

            var ings_to_return = from ing in ings_in_rec_list
                                 join secondIng in ings_in_pantry
                                 on ing.Name equals secondIng.Name
                                 select ing;

            return ings_to_return as List<Ingredient>;
        }


    }
}