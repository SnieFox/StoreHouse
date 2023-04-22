using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.ViewModels.ViewSettingMethods
{
    internal class ViewSettings
    {
        public static List<OutputWriteOff> GetOutputWriteOffs()
        {
            try
            {
                var tempList = StoreHouseContext.GetContext().WriteOffs.ToList();

                List<OutputWriteOff> dataList = new List<OutputWriteOff>();
                foreach (var temp in tempList)
                {
                    if (temp.DishId == null && temp.IngredientId == null)
                    {
                        dataList.Add(new OutputWriteOff($"{temp.Count}{DbUsage.GetIngredientUnitByName(temp.Product)}", -1, temp.Date, temp.Product,
                            $"{Math.Round(temp.Sum, 2)}грн",
                            temp.Cause));
                    }
                    else if (temp.DishId != null)
                    {
                        dataList.Add(new OutputWriteOff($"{temp.Count}{DbUsage.GetIngredientUnitByName(temp.Product)}",(int)temp.DishId, temp.Date, temp.Product,
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public static List<OutputSupplies> GetOutputSupplies()
        {
            try
            {
                var tempList = StoreHouseContext.GetContext().Supplies.ToList();
                List<OutputSupplies> dataList = new List<OutputSupplies>();
                foreach (var temp in tempList)
                {
                    dataList.Add(new OutputSupplies(temp.Id.ToString(), temp.Date, temp.Supplier, temp.Product, temp.Comment, $"{Math.Round(temp.Sum, 2)}грн", $"{Convert.ToString(temp.Count).Replace('.', ',')}{DbUsage.GetIngredientUnitByName(temp.Product)}"));
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputSupplies>();
            }
        }
        public static List<OutputIngredient> GetOutputIngredients()
        {
            try
            {
                var tempList = StoreHouseContext.GetContext().Ingredients.ToList();

                List<OutputIngredient> dataList = new List<OutputIngredient>();
                foreach (var temp in tempList)
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
        public static List<OutputDish> GetOutputDishes()
        {
            try
            {
                var tempList = StoreHouseContext.GetContext().Dishes.ToList();

                List<OutputDish> dataList = new List<OutputDish>();
                foreach (var temp in tempList)
                {
                    dataList.Add(new OutputDish(temp.Name, temp.Type, $"{temp.Category}", $"{Math.Round(temp.PrimeCost,2)}грн", $"{Math.Round(temp.Sum, 2)}грн"));
                }
                return dataList;
            }
            catch (SqlNullValueException e)
            {
                return new List<OutputDish>();

            }
        }
    }
}

