using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.Model.DbContext.Methods
{
    internal class AddToDb : IAddToDb
    {
        public string AddOutputAddDish(int dishId, string name, string count, string sum)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                OutputAddDish outputAddDish = new OutputAddDish()
                {
                    DishId = dishId,
                    Name = name,
                    Count = count,
                    Sum = sum

                };
                db.OutputAddDishes.Add(outputAddDish);
                db.SaveChanges();
            }

            return result;
        }

        public string AddDish(string name, string type, string category, decimal primeCost, decimal sum, List<OutputAddDish> ingredientsList)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                Dish dish = new Dish()
                {
                    Name = name,
                    Type = type,
                    Category = category,
                    PrimeCost = primeCost,
                    Sum = sum,
                    IngredientsList = ingredientsList
                };
                db.Dishes.Add(dish);
                db.SaveChanges();
            }

            return result;
        }

        public string AddIngredient(string name, string unit, string currentRemains, decimal primeCost, decimal sum, string type)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                Ingredient ingredient = new Ingredient()
                {
                    Name = name,
                    Unit = unit,
                    CurrentRemains = currentRemains,
                    PrimeCost = primeCost,
                    Sum = sum,
                    Type = type

                };
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
            }

            return result;
        }

        public string AddSupply(int ingredientId, string date, string supplier, string product, string count, string comment, decimal sum)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                Supply supply = new Supply()
                {
                    IngredientId = ingredientId,
                    Date = date,
                    Supplier = supplier,
                    Product = product,
                    Comment = comment,
                    Sum = sum
                };
                db.Supplies.Add(supply);
                var ingr = (from ingredient in db.Ingredients
                            where ingredient.Id == ingredientId
                                select ingredient);
                foreach (var id in ingr)
                {
                    string[] tempRemainsSplit = id.CurrentRemains.Split(' ');
                    decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                    id.CurrentRemains = Convert.ToString(tempRemains + Convert.ToDecimal(count));
                    id.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains),2);
                }
                db.SaveChanges();
            }

            return result;
        }

        public string AddWriteOffIngredient(int ingredientId, string date, string product, string count, decimal sum, string cause)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                WriteOff writeOff = new WriteOff()
                {
                    IngredientId = ingredientId,
                    Date = date,
                    Product = product,
                    Sum = sum,
                    Cause = cause
                    
                };
                db.WriteOffs.Add(writeOff);
                var ingr = (from ingredient in db.Ingredients
                    where ingredient.Id == ingredientId
                    select ingredient);
                foreach (var id in ingr)
                {
                    string[] tempRemainsSplit = id.CurrentRemains.Split(' ');
                    decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                    id.CurrentRemains = Convert.ToString(tempRemains - Convert.ToDecimal(count.Replace('.', ',')));
                    id.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains), 2);
                }
                db.SaveChanges();
            }

            return result;
        }
        public string AddWriteOffDish(int dishId, string date, string product, string count, decimal sum, string cause)
        {
            string result = "Готово!";
            var dishIngr = new List<OutputAddDish>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                WriteOff writeOff = new WriteOff()
                {
                    DishId = dishId,
                    Date = date,
                    Product = product,
                    Sum = sum,
                    Cause = cause

                };
                db.WriteOffs.Add(writeOff);
                dishIngr = (from ingredient in db.OutputAddDishes
                    where ingredient.DishId == dishId
                              select ingredient).ToList();
                db.SaveChanges();
            }

            using (StoreHouseContext db = new StoreHouseContext())
            {
                foreach (var dIng in dishIngr)
                {
                    var ingr = (from ingredient in db.Ingredients
                        where ingredient.Name == dIng.Name
                        select ingredient);
                    foreach (var id in ingr)
                    {
                        string[] tempRemainsSplit = id.CurrentRemains.Split(' ');
                        string[] tempCountSplit = dIng.Count.Split('к');
                        decimal decTempCountSplit = Convert.ToDecimal(count.Replace('.', ','));
                        decimal decTempIngrCountSplit = Convert.ToDecimal(tempCountSplit[0].Replace('.', ','));
                        decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                        id.CurrentRemains = Convert.ToString(tempRemains -(decTempCountSplit*decTempIngrCountSplit));
                        id.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains), 2);
                    }
                }
                db.SaveChanges();
            }

            return result;
        }

    }
}
