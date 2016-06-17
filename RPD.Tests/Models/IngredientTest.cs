using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPD.Models;


namespace RPD.Tests.Models
{
    /// <summary>
    /// Test for ingredient methods
    /// </summary>
    [TestClass]
    public class IngredientTest
    {
        [TestMethod]
        public void IngEnsureICanCreateAnInstance()
        {
            //Arrange
            Ingredient ing = new Ingredient();
            //Act
            //Assert
            Assert.IsNotNull(ing);
        }

        [TestMethod]
        public void IngEnsureICanSetCalories()
        {
            //Arrange
            Ingredient ing_1 = new Ingredient();
            ing_1.Fat = 1;
            ing_1.Carbs = 2;
            ing_1.Protien = 4;
            //((1*9) + (2*4) + (4*4)) = 33

            Ingredient ing_2 = new Ingredient();
            ing_2.Fat = 1;
            ing_2.Carbs = 1;
            ing_2.Protien = 1;
            //((1*9) + (1*4) + (1*4)) = 17

            //act

            ing_1.SetCalories(); //should calculate.
            ing_2.SetCalories(100); //should set.


            //assert
            Assert.AreEqual(33, ing_1.Calories);
            Assert.AreEqual(100, ing_2.Calories);
        }

        [TestMethod]
        public void IngEnsureICanSetAllergy()
        {
            //Arrange
            Ingredient ing = new Ingredient();

            //Act
            ing.SetAllergy("egg");

            //Assert
            Assert.AreEqual(1, ing.Allergies.Count);
        }

        [TestMethod]
        public void IngEnsureICanPlaceACupConversion()
        {
            //Arrange
            Ingredient ing = new Ingredient();
            ing.IsLiquid = true;
            Ingredient ing_2 = new Ingredient();
            ing_2.IsLiquid = false;

            //Act
            ing.SetCupConversion(8);
            ing_2.SetCupConversion(5);

            //Assert
            Assert.AreEqual(237, ing.CupConversion);
            Assert.IsTrue(ing.MeasureUnit == "mls");
            Assert.AreEqual(142, ing_2.CupConversion);
            Assert.IsTrue(ing_2.MeasureUnit == "g");
        }

        [TestMethod]
        public void IngEnsureICanConvertToGramsOrMils()
        {
            //Arrange
            Ingredient ing = new Ingredient();
            ing.SetCupConversion(8);
            Ingredient ing_2 = new Ingredient();
            ing_2.SetCupConversion(5);
            Ingredient ing_3 = new Ingredient();
            ing_3.SetCupConversion(8);
            ing_3.IsLiquid = true;

            //Act
            string ing_converted = ing.ConvertToGramsOrMls(9, "cups");
            string ing_2_converted = ing_2.ConvertToGramsOrMls(4, "kg");
            string ing_3_converted = ing_3.ConvertToGramsOrMls(1, "gal");

            //Assert
            Assert.AreEqual("2043 g", ing_converted);
            Assert.AreEqual("4 kg", ing_2_converted);
            Assert.AreEqual("3632 ml", ing_3_converted);
        }
    }
}