using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.ViewModels.ViewSettingMethods
{
    internal class ViewSettings
    {
        public static List<OutputWriteOff> GetOutputWriteOffs()
        {
            var tempList = StoreHouseContext.GetContext().WriteOffs.ToList();

            List<OutputWriteOff> dataList = new List<OutputWriteOff>();
            foreach (var temp in tempList)
            {
                dataList.Add(new OutputWriteOff(temp.Date, temp.Product, $"{Math.Round(temp.Sum,2)}грн", temp.Cause));
            }
            return dataList;
        }
        public static List<OutputSupplies> GetOutputSupplies()
        {
            var tempList = StoreHouseContext.GetContext().Supplies.ToList();

            List<OutputSupplies> dataList = new List<OutputSupplies>();
            foreach (var temp in tempList)
            {
                dataList.Add(new OutputSupplies(temp.Id.ToString(), temp.Date, temp.Supplier, temp.Product, temp.Comment, $"{Math.Round(temp.Sum,2)}грн"));
            }
            return dataList;
        }
        public static List<OutputIngredient> GetOutputIngredients()
        {
            var tempList = StoreHouseContext.GetContext().Ingredients.ToList();

            List<OutputIngredient> dataList = new List<OutputIngredient>();
            foreach (var temp in tempList)
            {
                dataList.Add(new OutputIngredient(temp.Name, temp.Unit,$"{Convert.ToString(Math.Round(Convert.ToDecimal(temp.CurrentRemains.Replace('.', ',')), 2))}{temp.Unit}",$"{Math.Round(temp.PrimeCost,2)}грн",$"{Math.Round(temp.Sum, 2)}грн", temp.Type));
            }
            return dataList;
        }
        public static List<OutputDish> GetOutputDishes()
        {
            var tempList = StoreHouseContext.GetContext().Dishes.ToList();

            List<OutputDish> dataList = new List<OutputDish>();
            foreach (var temp in tempList)
            {
                dataList.Add(new OutputDish(temp.Name, temp.Type, $"{temp.Category}", $"{Math.Round(temp.PrimeCost,2)}грн", $"{Math.Round(temp.Sum, 2)}грн"));
            }
            return dataList;
        }
    }
}

