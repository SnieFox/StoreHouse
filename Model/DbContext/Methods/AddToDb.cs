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
        public string AddRemain(string name, string type, string category, Ingredient currentRemains, decimal primeCost, decimal sum)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                Remain remain = new Remain()
                {
                    Name = name,
                    Type = type,
                    Category = category,
                    CurrentRemains = currentRemains.CurrentRemains,
                    PrimeCost = primeCost,
                    Sum = sum
                };
                db.Remains.Add(remain);
                db.SaveChanges();
            }

            return result;
        }

        public string AddDish(string name, string type, string category, double currentRemains, decimal primeCost)
        {
            throw new NotImplementedException();
        }

        public string AddIngredient(string name, string unit, double currentRemains, decimal primeCost, decimal sum)
        {
            throw new NotImplementedException();
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
