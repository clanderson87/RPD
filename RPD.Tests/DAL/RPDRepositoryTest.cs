using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPD.DAL;
using Moq;
using System.Collections.Generic; // For List<>
using RPD.Models;
using System.Linq; // For IQueryable and List.AsQueryable()
using System.Data.Entity; // For DbSet

namespace RPD.Tests.DAL
{
    [TestClass]
    public class RPDRepositoryTest
    {
        Mock<RPDContext> mock_context { get; set; }
        RPDRepository Repo { get; set; }

        //Users Mocks
        Mock<DbSet<ApplicationUser>> mock_users_table { get; set; }
        IQueryable<ApplicationUser> users_data { get; set; }
        List<ApplicationUser> users_datasource { get; set; }

        //Recipe Mocks
        Mock<DbSet<Recipe>> mock_recipe_table { get; set; }
        IQueryable<Recipe> recipe_data { get; set; }
        List<Recipe> recipe_datasource { get; set; }

        //Tag Mocks
        Mock<DbSet<Tag>> mock_tags_table { get; set; }
        List<Tag> tags_datasource { get; set; }
        IQueryable<Tag> tag_data { get; set; }

        //Allergy Mocks
        Mock<DbSet<Allergy>> mock_allergy_table { get; set; }
        List<Allergy> allergy_datasource { get; set; }
        IQueryable<Allergy> allergy_data { get; set; }

        //Pantry Mocks
        Mock<DbSet<Pantry>> mock_pantry_table { get; set; }
        List<Pantry> pantry_datasource { get; set; }
        IQueryable<Pantry> pantry_data { get; set; }

        //Ingredient Mocks
        Mock<DbSet<Ingredient>> mock_ingredient_table { get; set; }
        List<Ingredient> ingredient_datasource { get; set; }
        IQueryable<Ingredient> ingredient_data { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_users_table = new Mock<DbSet<ApplicationUser>>();
            mock_recipe_table = new Mock<DbSet<Recipe>>();
            mock_allergy_table = new Mock<DbSet<Allergy>>();
            mock_ingredient_table = new Mock<DbSet<Ingredient>>();
            mock_pantry_table = new Mock<DbSet<Pantry>>();
            mock_tags_table = new Mock<DbSet<Tag>>();

            mock_context = new Mock<RPDContext>();// 
            Repo = new RPDRepository(mock_context.Object); // mock_context.Object gives me an instance of what's in angle brakcets

            users_datasource = new List<ApplicationUser>();
            recipe_datasource = new List<Recipe>();
            ingredient_datasource = new List<Ingredient>();
            allergy_datasource = new List<Allergy>();
            pantry_datasource = new List<Pantry>();
            tags_datasource = new List<Tag>();

            users_data = users_datasource.AsQueryable();
            recipe_data = recipe_datasource.AsQueryable();
            ingredient_data = ingredient_datasource.AsQueryable();
            allergy_data = allergy_datasource.AsQueryable();
            pantry_data = pantry_datasource.AsQueryable();
            tag_data = tags_datasource.AsQueryable();
        }

        [TestCleanup]
        public void Cleanup()
        {
            users_datasource = null;
            recipe_datasource = null;
            ingredient_datasource = null;
            allergy_datasource = null;
            pantry_datasource = null;
            tags_datasource = null;
        }

        void ConnectMocksToDatasource()
        {
            mock_recipe_table.As<IQueryable<Recipe>>().Setup(p => p.GetEnumerator()).Returns(recipe_data.GetEnumerator());
            mock_recipe_table.As<IQueryable<Recipe>>().Setup(p => p.ElementType).Returns(recipe_data.ElementType);
            mock_recipe_table.As<IQueryable<Recipe>>().Setup(p => p.Expression).Returns(recipe_data.Expression);
            mock_recipe_table.As<IQueryable<Recipe>>().Setup(p => p.Provider).Returns(recipe_data.Provider);
            mock_recipe_table.Setup(recipe => recipe.Add(It.IsAny<Recipe>())).Callback((Recipe recipe) => recipe_datasource.Add(recipe));

            mock_ingredient_table.As<IQueryable<Ingredient>>().Setup(p => p.GetEnumerator()).Returns(ingredient_data.GetEnumerator());
            mock_ingredient_table.As<IQueryable<Ingredient>>().Setup(p => p.ElementType).Returns(ingredient_data.ElementType);
            mock_ingredient_table.As<IQueryable<Ingredient>>().Setup(p => p.Expression).Returns(ingredient_data.Expression);
            mock_ingredient_table.As<IQueryable<Ingredient>>().Setup(p => p.Provider).Returns(ingredient_data.Provider);
            mock_ingredient_table.Setup(ingredient => ingredient.Add(It.IsAny<Ingredient>())).Callback((Ingredient ingredient) => ingredient_datasource.Add(ingredient));

            mock_tags_table.As<IQueryable<Tag>>().Setup(p => p.GetEnumerator()).Returns(tag_data.GetEnumerator());
            mock_tags_table.As<IQueryable<Tag>>().Setup(p => p.ElementType).Returns(tag_data.ElementType);
            mock_tags_table.As<IQueryable<Tag>>().Setup(p => p.Expression).Returns(tag_data.Expression);
            mock_tags_table.As<IQueryable<Tag>>().Setup(p => p.Provider).Returns(tag_data.Provider);
            mock_tags_table.Setup(tag => tag.Add(It.IsAny<Tag>())).Callback((Tag tag) => tags_datasource.Add(tag));

            mock_allergy_table.As<IQueryable<Allergy>>().Setup(p => p.GetEnumerator()).Returns(allergy_data.GetEnumerator());
            mock_allergy_table.As<IQueryable<Allergy>>().Setup(p => p.ElementType).Returns(allergy_data.ElementType);
            mock_allergy_table.As<IQueryable<Allergy>>().Setup(p => p.Expression).Returns(allergy_data.Expression);
            mock_allergy_table.As<IQueryable<Allergy>>().Setup(p => p.Provider).Returns(allergy_data.Provider);
            mock_allergy_table.Setup(allergy => allergy.Add(It.IsAny<Allergy>())).Callback((Allergy allergy) => allergy_datasource.Add(allergy));

            mock_pantry_table.As<IQueryable<Pantry>>().Setup(p => p.GetEnumerator()).Returns(pantry_data.GetEnumerator());
            mock_pantry_table.As<IQueryable<Pantry>>().Setup(p => p.ElementType).Returns(pantry_data.ElementType);
            mock_pantry_table.As<IQueryable<Pantry>>().Setup(p => p.Expression).Returns(pantry_data.Expression);
            mock_pantry_table.As<IQueryable<Pantry>>().Setup(p => p.Provider).Returns(pantry_data.Provider);
            mock_pantry_table.Setup(pantry => pantry.Add(It.IsAny<Pantry>())).Callback((Pantry pantry) => pantry_datasource.Add(pantry));

            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(p => p.GetEnumerator()).Returns(users_data.GetEnumerator());
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(p => p.ElementType).Returns(users_data.ElementType);
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(p => p.Expression).Returns(users_data.Expression);
            mock_users_table.As<IQueryable<ApplicationUser>>().Setup(p => p.Provider).Returns(users_data.Provider);
            mock_users_table.Setup(users => users.Add(It.IsAny<ApplicationUser>())).Callback((ApplicationUser users) => users_datasource.Add(users));

            mock_context.Setup(context => context.Recipes).Returns(mock_recipe_table.Object);
        }

        [TestMethod]
        public void RepoEnsureICanCreateAnInstance()
        {
            RPDRepository repo = new RPDRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureThereAreNoRecipes()
        {
            // Arrange
            ConnectMocksToDatasource();

            // Act
            int recipe_count = Repo.GetRecipeCount();

            // Assert
            Assert.AreEqual(0, recipe_count);
        }

        [TestMethod]
        public void RepoEnsureICanCreateARecipe()
        {
            // Arrange
            List<Recipe> recipe_datasource = new List<Recipe>();
            IQueryable<Recipe> recipe_data = recipe_datasource.AsQueryable();
            ConnectMocksToDatasource();

            // Act
            // Assert
        }

        [TestMethod]
        public void RepoEnsureICanGetRecipes()
        {
            // Arrange
            //List<Recipe> recipe_datasource = new List<Recipe>(); // Implied
            Recipe recipe1 = new Recipe { RId = 1, };
            Recipe recipe2 = new Recipe { RId = 2, };
            recipe_datasource.Add(recipe1);
            recipe_datasource.Add(recipe2);
            ConnectMocksToDatasource();

            // Act
            List<Recipe> recipes = Repo.GetRecipes();

            // Assert
            Assert.AreEqual(2, recipes.Count);
        }

        [TestMethod]
        public void RepoEnsureICanAddRecipes()
        {

            //Arrange
            int _prep_time = 37;
            int _active_time = 64;
            string _cuisine = "Weird";
            int _serves = 3;
            string _title = "Wild Game Peanut Butter Mollusk Pancakes";
            string _url = "www.google.com";
            Tag onePot = new Tag();
            Tag easy = new Tag();
            Allergy peanut = new Allergy();
            Allergy shellfish = new Allergy();
            List<Allergy> _allergies = new List<Allergy> { peanut, shellfish };
            Ingredient peanut_butter = new Ingredient();
            //Ingredient clam = new Ingredient{ Name = "clam", Allergies = { shellfish }, Calories = 100, Protien = 20 };
            Ingredient clam = new Ingredient();
            List<Ingredient> _ingredients = new List<Ingredient> { peanut_butter, clam };
            List<Tag> _tags = new List<Tag> { onePot, easy, };
            List<string> _prep_instructions = new List<string> { "something", "another thing", };
            List<string> _active_instructions = new List<string> { "another thing", "some thing" };
            ConnectMocksToDatasource();

            //Act
            Repo.AddRecipe(title: _title, serves: _serves, ingredients: _ingredients, allergies: _allergies, active_instructions: _active_instructions, calories: 543, prep_instructions: _prep_instructions, prep_time: _prep_time, cuisine: _cuisine, active_time: _active_time, tags: _tags, url: _url);
            Repo.AddRecipe(title: _title, serves: _serves, ingredients: _ingredients, allergies: _allergies, active_instructions: _active_instructions, cuisine: _cuisine, calories: 712, prep_time: 35, tags: _tags, active_time: 23);

            //Assert
            Assert.AreEqual(2, Repo.GetRecipeCount());
        }

        [TestMethod]
        public void RepoEnsureICanGetByGiant()
        {
            //Arrange
            int _prep_time = 37;
            int _active_time = 64;
            string _cuisine = "Weird";
            int _serves = 3;
            string _title = "Wild Game Peanut Butter Mollusk Pancakes";
            string _url = "www.google.com";
            Tag onePot = new Tag();
            Tag easy = new Tag();
            Allergy peanut = new Allergy();
            peanut.Name = "Peanut";
            Allergy shellfish = new Allergy();
            shellfish.Name = "shellfish";
            List<Allergy> _allergies = new List<Allergy> { peanut, shellfish };
            Ingredient peanut_butter = new Ingredient();
            Ingredient clam = new Ingredient();
            List<Ingredient> _ingredients = new List<Ingredient> { peanut_butter, clam };
            List<Tag> _tags = new List<Tag> { onePot, easy };
            List<string> _prep_instructions = new List<string> { "something", "another thing" };
            List<string> _active_instructions = new List<string> { "another thing", "some thing" };


            string cuisine_ = "ick";
            int serves_ = 3;
            string title_ = "Pumpkin Spice Bullshit";
            Tag basic_ = new Tag();
            Tag quick_ = new Tag();
            Allergy egg_ = new Allergy();
            egg_.Name = "egg";
            List<Allergy> allergies_ = new List<Allergy> { egg_ };
            Ingredient pumpkin_ = new Ingredient();
            Ingredient spice_ = new Ingredient();
            List<Ingredient> ingredients_ = new List<Ingredient> { pumpkin_, spice_ };
            List<Tag> tags_ = new List<Tag> { basic_, quick_ };
            List<string> prep_instructions_ = new List<string> { "Blah", "blah blah" };
            List<string> active_instructions_ = new List<string> { "yaddayaddayadda" };

            //Act
            ConnectMocksToDatasource();

            Repo.AddRecipe(title: _title, serves: _serves, ingredients: _ingredients, allergies: _allergies, active_instructions: _active_instructions, calories: 543, prep_instructions: _prep_instructions, prep_time: _prep_time, cuisine: _cuisine, active_time: _active_time, tags: _tags, url: _url);
            Repo.AddRecipe(title: title_, active_time: 20, serves: serves_, ingredients: ingredients_, allergies: allergies_, active_instructions: active_instructions_, cuisine: cuisine_, calories: 245, prep_time: 5, tags: tags_);

            List<Recipe> recipes_returned = Repo.GetRecipes(cuisene: "Weird");//this should return 1 recipe.
            recipes_returned.AddRange(Repo.GetRecipes(cals: 700, serves: 3, ingredients: _ingredients, active_time: 60)); //this should return nothing because nothing meets these criteria.
            recipes_returned.AddRange(Repo.GetRecipes(allergies: _allergies)); //This should return 1 recipe, because the recipe with _allergies should be discounted.

            //Assert
            Assert.AreEqual(2, recipes_returned.Count());
            Assert.AreEqual("Wild Game Peanut Butter Mollusk Pancakes", recipes_returned[0].Title);
            Assert.AreEqual("ick", recipes_returned[1].Cuisine);
        }

        /*[TestMethod]
        public void RepoEnsureICanGetUserPantrys()
        {
            ApplicationUser user = new ApplicationUser();
            user.MyPantry = 5;
            uid = 
        }*/

        [TestMethod]
        public void RepoEnsureICanSetUserPrefs()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser();
            
            List<string> cuisines = new List<string> { "American", "Indian" };
            List<int> favints = new List<int> { 1, 3, 6, 2, 8 };
            Tag tag = new Tag();
            Tag otherTag = new Tag();
            List<Tag> tag_list = new List<Tag>();
            tag_list.Add(tag);
            tag_list.Add(otherTag);
            ConnectMocksToDatasource();

            //Act
            Repo.SetUsersPrefs("uid", cuisines, favints, 2300, tag_list);

            //Assert
            Assert.AreEqual(2300, user.CaloriePref);
            Assert.AreEqual(cuisines, user.FaveCuisines);
        }

        [TestMethod]
        public void RepoEnsureICanGetShoppingListsAgainstPantrys()
        {

        }
    }
}