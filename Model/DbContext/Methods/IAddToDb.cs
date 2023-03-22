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
        string AddDish(string name, string type, string category, double currentRemains, decimal primeCost);
        string AddIngredient(string name, string unit, string currentRemains, decimal primeCost, decimal sum, string type);
        string AddSupply(int ingredientId, string date, string supplier, string product, string count, string comment, decimal sum);
        string AddWriteOff(int ingredientId, string date, string product, string count, decimal sum, string cause);

    }
}
