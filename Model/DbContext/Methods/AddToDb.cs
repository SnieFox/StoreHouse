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

        public string AddSupply(int ingredientId, DateTime date, string supplier, Ingredient product, string Comment, decimal sum)
        {
            throw new NotImplementedException();
        }

        public string AddWriteOff(DateTime date, Ingredient product, double currentRemains, decimal sum, string cause)
        {
            throw new NotImplementedException();
        }
    }
}
