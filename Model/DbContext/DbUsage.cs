using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shell;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels;

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
        public static List<OutputAddDish> GetDishIngredientList()
        {
            List<OutputAddDish> list = DbUsage.GetAllDishIngById(DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name));
            List<OutputAddDish> returnList = new List<OutputAddDish>();
            foreach (var dish in list)
            {
                string[] dishCountSplit = dish.Count.Split('к');
                string dishCount = dishCountSplit[0];
                returnList.Add(new OutputAddDish(dish.Name, $"{dishCount}{DbUsage.GetIngredientUnitByName(dish.Name)}", $"{dish.Sum}грн"));
            }
            return returnList;
        }
        public static List<OutputAddDish> GetDishIngredientList(int dishId)
        {
            List<OutputAddDish> dishList;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                dishList = (from dish in db.OutputAddDishes
                               where dish.DishId == dishId
                                   select dish).ToList();
            }
            return dishList;
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
        public static int GetOutputAddDishesIdByName(string name, int dishId)
        {
            int id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from ingredient in db.OutputAddDishes
                    where ingredient.Name == name && ingredient.DishId == dishId
                    select ingredient.Id).FirstOrDefault();
            }

            return id;
        }
        public static List<Ingredient> GetIngredientsByName(string name)
        {
            List<Ingredient> IngList;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                IngList = (from ingredient in db.Ingredients
                    where ingredient.Name == name
                    select ingredient).ToList();
            }

            return IngList;
        }
        public static List<OutputIngredient> SearchIngredientsByName(string name)
        {
            try
            {
                List<Ingredient> IngList;
                using (StoreHouseContext db = new StoreHouseContext())
                {
                    IngList = (from ingredient in db.Ingredients
                        where ingredient.Name.Contains(name)
                        select ingredient).ToList();
                }
                List<OutputIngredient> dataList = new List<OutputIngredient>();
                foreach (var temp in IngList)
                {
                    var tempRemains = temp.CurrentRemains.Split(' ');
                    dataList.Add(new OutputIngredient(temp.Name, temp.Unit,
                        $"{Convert.ToString(Math.Round(Convert.ToDecimal(tempRemains[0].Replace('.', ',')), 2))}{temp.Unit}",
                        $"{Math.Round(temp.PrimeCost, 2)}грн", $"{Math.Round(temp.Sum, 2)}грн", temp.Type));
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputIngredient>();

            }
        }
        public static List<OutputDish> SearchDishesByName(string name)
        {
            try
            {
                List<Dish> dishList;
                using (StoreHouseContext db = new StoreHouseContext())
                {
                    dishList = (from dish in db.Dishes
                        where dish.Name.Contains(name)
                        select dish).ToList();
                }
                List<OutputDish> dataList = new List<OutputDish>();
                foreach (var temp in dishList)
                {
                    dataList.Add(new OutputDish(
                        temp.Name,
                        temp.Type,
                        temp.Category,
                        $"{Math.Round(temp.PrimeCost, 2)}грн",
                        $"{Math.Round(temp.Sum, 2)}грн"
                        ));
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputDish>();

            }
        }

        public static List<OutputSupplies> SearchSuppliesByName(string name)
        {
            try
            {
                List<Supply> dishList;
                using (StoreHouseContext db = new StoreHouseContext())
                {
                    dishList = (from supl in db.Supplies
                        where supl.Product.Contains(name)
                        select supl).ToList();
                }
                List<OutputSupplies> dataList = new List<OutputSupplies>();
                foreach (var temp in dishList)
                {
                    dataList.Add(new OutputSupplies(
                        Convert.ToString(temp.Id),
                        temp.Date,
                        temp.Supplier,
                        temp.Product,
                        temp.Comment,
                        $"{Math.Round(temp.Sum, 2)}грн",
                        $"{Convert.ToString(Math.Round(temp.Count, 2))}{DbUsage.GetIngredientUnitByName(temp.Product)}"
                    ));
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputSupplies>();

            }
        }
        public static List<OutputWriteOff> SearchWriteOffsByName(string name)
        {
            try
            {
                List<WriteOff> dishList;
                using (StoreHouseContext db = new StoreHouseContext())
                {
                    dishList = (from wrOff in db.WriteOffs
                        where wrOff.Product.Contains(name)
                        select wrOff).ToList();
                }
                List<OutputWriteOff> dataList = new List<OutputWriteOff>();
                foreach (var temp in dishList)
                {
                    if (temp.DishId != null)
                    {
                        dataList.Add(new OutputWriteOff($"{temp.Count}{DbUsage.GetIngredientUnitByName(temp.Product)}", (int)temp.DishId, temp.Date, temp.Product,
                            $"{Math.Round(temp.Sum, 2)}грн",
                            temp.Cause));
                    }
                    else
                    {
                        dataList.Add(new OutputWriteOff($"{temp.Count}{DbUsage.GetIngredientUnitByName(temp.Product)}", temp.Date, temp.Product, $"{Math.Round(temp.Sum, 2)}грн",
                            temp.Cause, (int)temp.IngredientId));
                    }
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputWriteOff>();

            }
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
        public static int GetSupplyIdByName(string name)
        {
            int id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from sup in db.Supplies
                    where sup.Product == name
                    select sup.Id).FirstOrDefault();
            }

            return id;
        }
        public static int GetWriteOffIdByName(string name)
        {
            int id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from wr in db.WriteOffs
                    where wr.Product == name
                    select wr.Id).FirstOrDefault();
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
        public static List<string> GetDishNames()
        {
            List<string> dishesNamesList = new List<string>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                dishesNamesList = (from dish in db.Dishes
                    select dish.Name).ToList();
            }

            return dishesNamesList;
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
        public static decimal GetSum(string count, List<OutputAddDish> ingrList)
        {
            decimal sum = 0;

            foreach (var ingr in ingrList)
            {
                sum += Convert.ToDecimal(ingr.Sum);
            }
            string[] tempCount = count.Split('к');
            return sum * Convert.ToDecimal(tempCount[0]);

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

        public static List<Supply> GetIngredientsSupplyListByName(string name)
        {
            List<Supply> id;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                id = (from supl in db.Supplies
                    where supl.Product == name
                    select supl).ToList();
            }
            return id;
        }

        public static string GetIngredientUnitByName(string name)
        {
            string unit;
            using (StoreHouseContext db = new StoreHouseContext())
            {
                unit = (from ingr in db.Ingredients
                    where ingr.Name == name
                    select ingr.Unit).FirstOrDefault();
            }
            return unit;
        }

    }
}
