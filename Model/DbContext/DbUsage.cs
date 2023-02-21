using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;

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
        public static List<WriteOff> GetAllWriteOffs()
        {
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var writeOffs = db.WriteOffs.ToList();
                return writeOffs;
            }
        }

    }
}
