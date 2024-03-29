﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.Model.DbContext.Methods
{
    class DeleteFromDb
    {
        public string DeleteSupply(int supplyId)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var supl = (from suply in db.Supplies
                    where suply.Id == supplyId
                            select suply);
                var ingrId = 0;
                double ingrCount = 0;
                foreach (var id in supl)
                {
                    ingrCount = id.Count;
                    ingrId = id.IngredientId;
                    db.Supplies.Remove(id);
                }
                var ingr = (from ingredient in db.Ingredients
                    where ingredient.Id == ingrId
                            select ingredient);
                foreach (var id in ingr)
                {
                    string[] tempRemainsSplit = id.CurrentRemains.Split(' ');
                    decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                    id.CurrentRemains = Convert.ToString(tempRemains - Convert.ToDecimal(ingrCount));
                    id.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains), 2);
                }
                db.SaveChanges();
            }

            return result;
        }
        public string DeleteWriteOff(int writeOffId)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var writeOff = (from writeOffs in db.WriteOffs
                    where writeOffs.Id == writeOffId
                                select writeOffs).FirstOrDefault();
                if (writeOff.DishId != null)
                {
                    var dishIngr = new List<OutputAddDish>();

                    dishIngr = (from ingredient in db.OutputAddDishes
                        where ingredient.DishId == writeOff.DishId
                        select ingredient).ToList();
                    foreach (var dIng in dishIngr)
                    {
                        var ingr = (from ingredient in db.Ingredients
                            where ingredient.Name == dIng.Name
                            select ingredient).FirstOrDefault();

                        string[] tempRemainsSplit = ingr.CurrentRemains.Split(' ');
                        string[] tempCountSplit = dIng.Count.Split('к');
                        decimal decTempCountSplit = Convert.ToDecimal(writeOff.Count.Replace('.', ','));
                        decimal decTempIngrCountSplit = Convert.ToDecimal(tempCountSplit[0].Replace('.', ','));
                        decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                        ingr.CurrentRemains = Convert.ToString(tempRemains + (decTempCountSplit * decTempIngrCountSplit));
                        ingr.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingr.PrimeCost), ingr.CurrentRemains), 2);

                        db.Entry(ingr).State = EntityState.Modified;
                    }
                    db.WriteOffs.Remove(writeOff);
                    db.SaveChanges();
                }
                else
                {
                    var ingr = (from ingredient in db.Ingredients
                        where ingredient.Id == (int)writeOff.IngredientId
                                select ingredient).FirstOrDefault();

                    string[] tempRemainsSplit = ingr.CurrentRemains.Split(' ');
                    decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                    ingr.CurrentRemains = Convert.ToString(tempRemains + Convert.ToDecimal(writeOff.Count.Replace('.', ',')));
                    ingr.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingr.PrimeCost), ingr.CurrentRemains), 2);

                    db.WriteOffs.Remove(writeOff);
                    db.Entry(ingr).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return result;
        }
        public string DeleteDishIngr(int dishIngId)
        {
            string result = "Готово!";
            OutputAddDish dishIngr;
            using (StoreHouseContext db = new StoreHouseContext())
            {

                dishIngr = (from dish in db.OutputAddDishes
                    where dish.Id == dishIngId
                    select dish).FirstOrDefault();
                db.OutputAddDishes.Remove(dishIngr);
                db.SaveChanges();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var dish = (from dishes in db.Dishes
                    where dishes.Id == dishIngr.DishId
                            select dishes).FirstOrDefault();
                dish.PrimeCost = Convert.ToDecimal(DbUsage.GetPrimeCost(DbUsage.GetDishIngredientList(dish.Id)));
                db.SaveChanges();
            }
            

            return result;
        }
        public string DeleteDish(int dishId)
        {
            string result = "Готово!";
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var dish = (from dishes in db.OutputAddDishes
                    where dishes.DishId == dishId
                            select dishes).ToList();
                foreach (var dh in dish)
                {
                    db.OutputAddDishes.Remove(dh);
                }
                db.SaveChanges();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {

                var writeOff = (from wrOff in db.WriteOffs
                    where wrOff.DishId == dishId
                    select wrOff).ToList();
                foreach (var wr in writeOff)
                {
                    db.WriteOffs.Remove(wr);
                }
                db.SaveChanges();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                var dish = (from dishes in db.Dishes
                    where dishes.Id == dishId
                    select dishes).FirstOrDefault();
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }
            return result;
        }


    }
}
