using StoreHouse.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.DbContext.Methods
{
    internal interface IAddToDb
    {
        string AddRemain(string name, string type, string category, Ingredient currentRemains, decimal primeCost, decimal sum);
        string AddDish(string name, string type, string category, double currentRemains, decimal primeCost);
        string AddIngredient(string name, string unit, double currentRemains, decimal primeCost, decimal sum);
        string AddSupply(int ingredientId, DateTime date, string supplier, Ingredient product, string Comment, decimal sum);
        string AddWriteOff(DateTime date, Ingredient product, double currentRemains, decimal sum, string cause);

    }
}
