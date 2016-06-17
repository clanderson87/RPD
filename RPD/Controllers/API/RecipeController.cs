using RPD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RPD.Models;

namespace RPD.Controllers
{
    public class RecipeController : ApiController
    {
        RPDRepository Repo = new RPDRepository();

        // GET: api/Recipe/perams
        public IEnumerable<Recipe> Get(
            string _cuisene = null,
            List<Tag> _tags = null,
            List<Allergy> _allergies = null,
            int _serves = 0,
            int _cals = 0,
            int _cal_range = 75,
            int _time_range = 10,
            int _prep_time = 0,
            int _active_time = 0,
            List<Ingredient> _ingredients = null, //try listing ings on commas, on ing.name
            int _limit = 21)
        {
            return Repo.GetRecipes(
                cuisene: _cuisene,
                tags: _tags,
                allergies: _allergies,
                serves: _serves,
                cals: _cals,
                cal_range: _cal_range,
                time_range: _time_range,
                prep_time: _prep_time,
                active_time: _active_time,
                ingredients: _ingredients,
                limit: _limit
                );
        }

        // GET: api/Recipe/
        public List<Recipe> Get()
        {
            return Repo.GetRecipes();
        }

        // POST: api/Recipe
        //public void Post([FromBody]string value)
        public void Post(
            string _title,
            string _cuisine,
            int _active_time,
            int _serves, //Everything from here above is required
            List<Ingredient> _ingredients,
            List<Allergy> _allergies,
            List<string> _active_instructions,
            int _prep_time = 20,
            List<Tag> _tags = null,
            string _url = "www.example.com/recipes",
            List<string> _prep_instructions = null,
            int _calories = 9999999)
        {
            Repo.AddRecipe(
                 title: _title,
                 cuisine: _cuisine,
                 serves: _serves,
                 ingredients: _ingredients,
                 allergies: _allergies,
                 active_instructions: _active_instructions,
                 active_time: _active_time,
                 prep_time: _prep_time,
                 tags: _tags,
                 url: _url,
                 prep_instructions: _prep_instructions,
                 calories: _calories
                 );
        }

        // PUT: api/Recipe/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Recipe/5
        public void Delete(int id)
        {

        }
    }
}