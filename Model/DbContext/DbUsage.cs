using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.Model.DbContext
{
    internal class DbUsage
    {
        public static List<Dish> GetAllDishes()
        {
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var dishes = db.Dishes.ToList();
                return dishes;
            }
        }
        public static List<Ingredient> GetAllIngredients()
        {
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var ingredients = db.Ingredients.ToList();
                return ingredients;
            }
        }
        public static List<Supply> GetAllSupplies()
        {
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var supplies = db.Supplies.ToList();
                return supplies;
            }
        }

        public static int GetIngredientIdByName(string name)
        {
            int id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from ingredient in db.Ingredients
                      where ingredient.Name ==name
                          select ingredient.Id).FirstOrDefault();
            }

            return id;
        }
        public static int GetDishIdByName(string name)
        {
            int id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from dish in db.Dishes
                    where dish.Name == name
                    select dish.Id).FirstOrDefault();
            }

            return id;
        }
        public static List<OutputAddDish> GetAllDishIngById(int dishId)
        {
            List<OutputAddDish> id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from ingr in db.OutputAddDishes
                    where ingr.DishId == dishId
                    select ingr).ToList();
            }

            return id;
        }
        public static int GetDishId(List<Dish> dishes)
        {
            int id = 1;
            if (dishes.Count != 0)
            {
                foreach (var dish in dishes)
                {
                    id = dish.Id;
                }
                id += 1;
                return id;
            }
            return id;

        }
        public static List<WriteOff> GetAllWriteOffs()
        {
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var writeOffs = db.WriteOffs.ToList();
                return writeOffs;
            }
        }

        public static List<string> GetProductNames()
        {
            List<string> productNamesList = new List<string>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                productNamesList = (from ingredient in db.Ingredients
                    select ingredient.Name).ToList();
            }

            return productNamesList;
        }

        public static decimal GetSum(string primeCost, string currentRemains)
        {
            decimal sum = 0;
            if (primeCost != null)
            {
                try
                {
                    sum = Convert.ToDecimal(primeCost.Replace('.', ',')) *
                          Convert.ToDecimal(currentRemains.Replace('.', ','));
                    return sum;
                }
                catch (NullReferenceException)
                {
                    return sum;
                }
                
            }

            return sum;
        }
        public static string GetPrimeCost(decimal sum, string currentRemains)
        {
            try
            {
                string primeCost = "";
                primeCost = Convert.ToString(Math.Round(sum / Convert.ToDecimal(currentRemains.Replace('.', ',')),2));
                return primeCost;
            }
            catch (NullReferenceException e)
            {
                return "";
            }

        }
        public static string GetPrimeCost(List<OutputAddDish> ingredientList)
        {
            try
            {
                double primeCost = 0;
                foreach (var ingr in ingredientList)
                {
                    primeCost += Convert.ToDouble(ingr.Sum);
                }

                return Convert.ToString(Math.Round(primeCost,2)); ;
            }
            catch (NullReferenceException e)
            {
                return "";
            }

        }
        public static string GetPrimeCost(int ingredientId, string SeletedProduct)
        {
            try
            {
                string primeCost = "";
                using (StoreHouseContext db = new StoreHouseContext())
                {
                    var primeCostList = (from ingredient in db.Ingredients
                        where ingredient.Id == DbUsage.GetIngredientIdByName(SeletedProduct)
                        select ingredient.PrimeCost);
                    foreach (var pc in primeCostList)
                    {
                        primeCost += pc.ToString();
                    }
                }
                return primeCost;
            }
            catch (NullReferenceException e)
            {
                return "";
            }

        }

    }
}
