using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;

namespace StoreHouse.Model.DbContext.Methods
{
    internal class AddToDb : IAddToDb
    {
        public string AddDish(string name, string type, string category, double currentRemains, decimal primeCost)
        {
            throw new NotImplementedException();
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
                    id.Sum = DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains);
                }
                db.SaveChanges();
            }

            return result;
        }

        public string AddWriteOff(DateTime date, Ingredient product, double currentRemains, decimal sum, string cause)
        {
            throw new NotImplementedException();
        }
    }
}
