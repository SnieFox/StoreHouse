using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.Model.DbContext.Methods
{
    internal class EditDb
    {
        public string EditSupply(int supplyId ,int newIngredientId, string newDate, string newSupplier, string newProduct, string newCount, string newComment, decimal newSum)
        {
            string result = "Готово!";
            List<Ingredient> newIngr = new List<Ingredient>();
            List<Supply> supl = new List<Supply>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                newIngr = (from ingredient in db.Ingredients
                    where ingredient.Id == newIngredientId
                    select ingredient).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                supl = (from supply in db.Supplies
                    where supply.Id == supplyId
                    select supply).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                foreach (var id in supl)
                {
                    var ingr = (from ingredient in db.Ingredients
                        where ingredient.Id == id.IngredientId
                        select ingredient);
                    if (newIngredientId != id.IngredientId)
                    {
                        foreach (var ingrId in ingr)
                        {
                            string[] tempRemainsSplit = ingrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            ingrId.CurrentRemains = Convert.ToString(tempRemains - Convert.ToDecimal(id.Count));
                            ingrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingrId.PrimeCost), ingrId.CurrentRemains), 2);
                        }
                        foreach (var newIngrId in newIngr)
                        {
                            string[] tempRemainsSplit = newIngrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            newIngrId.CurrentRemains = Convert.ToString(tempRemains + Convert.ToDecimal(newCount.Replace('.', ',')));
                            newIngrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(newIngrId.PrimeCost), newIngrId.CurrentRemains), 2);
                        }
                    }
                    else
                    {
                        foreach (var ingrId in ingr)
                        {
                            string[] tempRemainsSplit = ingrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            ingrId.CurrentRemains = Convert.ToString(tempRemains - Convert.ToDecimal(id.Count) + Convert.ToDecimal(newCount.Replace('.', ',')));
                            ingrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingrId.PrimeCost), ingrId.CurrentRemains), 2);
                        }
                    }

                    id.IngredientId = newIngredientId;
                    id.Supplier = newSupplier;
                    id.Product = newProduct;
                    id.Count = Convert.ToDouble(newCount.Replace('.', ','));
                    id.Comment = newComment;
                    id.Sum = newSum;
                    db.Entry(id).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return result;
        }
        public string EditWriteOffIngredient(int writeOffId, int newIngredientId, string newProduct, string newCount, decimal newSum, string newCause)
        {
            string result = "Готово!";
            List<Ingredient> newIngr = new List<Ingredient>();
            List<WriteOff> writeOff = new List<WriteOff>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                newIngr = (from ingredient in db.Ingredients
                    where ingredient.Id == newIngredientId
                    select ingredient).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                writeOff = (from wr in db.WriteOffs
                    where wr.Id == writeOffId
                            select wr).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                foreach (var id in writeOff)
                {
                    var ingr = (from ingredient in db.Ingredients
                        where ingredient.Id == id.IngredientId
                        select ingredient);
                    if (newIngredientId != id.IngredientId)
                    {
                        foreach (var ingrId in ingr)
                        {
                            string[] tempRemainsSplit = ingrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            ingrId.CurrentRemains = Convert.ToString(tempRemains + Convert.ToDecimal(id.Count));
                            ingrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingrId.PrimeCost), ingrId.CurrentRemains), 2);
                        }
                        foreach (var newIngrId in newIngr)
                        {
                            string[] tempRemainsSplit = newIngrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            newIngrId.CurrentRemains = Convert.ToString(tempRemains - Convert.ToDecimal(newCount.Replace('.', ',')));
                            newIngrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(newIngrId.PrimeCost), newIngrId.CurrentRemains), 2);
                        }
                    }
                    else
                    {
                        foreach (var ingrId in ingr)
                        {
                            string[] tempRemainsSplit = ingrId.CurrentRemains.Split(' ');
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            ingrId.CurrentRemains = Convert.ToString(tempRemains + Convert.ToDecimal(id.Count) - Convert.ToDecimal(newCount.Replace('.', ',')));
                            ingrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingrId.PrimeCost), ingrId.CurrentRemains), 2);
                        }
                    }

                    id.IngredientId = newIngredientId;
                    id.Product = newProduct;
                    id.Count = newCount.Replace('.', ',');
                    id.Cause = newCause;
                    id.Sum = newSum;
                    db.Entry(id).State = EntityState.Modified;
                    db.Entry(newIngr.FirstOrDefault()).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return result;
        }
        public string EditWriteOffDish(int writeOffId, int newDishId, string newProduct, string newCount, decimal newSum, string newCause)
        {
            string result = "Готово!";
            List<OutputAddDish> newDish = new List<OutputAddDish>();
            List<WriteOff> writeOff = new List<WriteOff>();
            List<Ingredient> newIngr = new List<Ingredient>();
            using (StoreHouseContext db = new StoreHouseContext())
            {
                newDish = (from ingr in db.OutputAddDishes
                           where ingr.DishId == newDishId
                           select ingr).ToList();
                newIngr = (from ingredient in db.Ingredients
                    where ingredient.Name == newDish.FirstOrDefault().Name
                    select ingredient).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                writeOff = (from wr in db.WriteOffs
                        where wr.Id == writeOffId
                            select wr).ToList();
            }
            using (StoreHouseContext db = new StoreHouseContext())
            {
                foreach (var dishId in newDish)
                {
                    var ingr = (from ingredient in db.Ingredients
                        where ingredient.Name == dishId.Name
                        select ingredient);
                    if (newDishId != dishId.DishId)
                    {
                        foreach (var id in ingr)
                        {
                            string[] tempRemainsSplit = id.CurrentRemains.Split(' ');
                            string[] tempCountSplit = dishId.Count.Split('к');
                            decimal decTempCountSplit = Convert.ToDecimal(writeOff.Count);
                            decimal decTempIngrCountSplit = Convert.ToDecimal(tempCountSplit[0].Replace('.', ','));
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            id.CurrentRemains = Convert.ToString(tempRemains + (decTempCountSplit * decTempIngrCountSplit));
                            id.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(id.PrimeCost), id.CurrentRemains), 2);
                        }
                        foreach (var newIngrId in newIngr)
                        {
                            string[] tempRemainsSplit = newIngrId.CurrentRemains.Split(' ');
                            string[] tempCountSplit = dishId.Count.Split('к');
                            decimal decTempCountSplit = Convert.ToDecimal(newCount.Replace('.', ','));
                            decimal decTempIngrCountSplit = Convert.ToDecimal(tempCountSplit[0].Replace('.', ','));
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            newIngrId.CurrentRemains = Convert.ToString(tempRemains - (decTempCountSplit * decTempIngrCountSplit));
                            newIngrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(newIngrId.PrimeCost), newIngrId.CurrentRemains), 2);
                        }
                    }
                    else
                    {
                        foreach (var ingrId in ingr)
                        {
                            string[] tempRemainsSplit = ingrId.CurrentRemains.Split(' ');
                            string[] tempCountSplit = dishId.Count.Split('к');
                            decimal decTempCountSplit = Convert.ToDecimal(writeOff.Count);
                            decimal decTempIngrCountSplit = Convert.ToDecimal(tempCountSplit[0].Replace('.', ','));
                            decimal tempRemains = Convert.ToDecimal(tempRemainsSplit[0].Replace('.', ','));
                            ingrId.CurrentRemains = Convert.ToString(tempRemains + (decTempCountSplit * decTempIngrCountSplit)- (Convert.ToDecimal(newCount.Replace('.', ',')) * decTempIngrCountSplit));
                            ingrId.Sum = Math.Round(DbUsage.GetSum(Convert.ToString(ingrId.PrimeCost), ingrId.CurrentRemains), 2);
                        }
                    }

                    writeOff.FirstOrDefault().DishId = newDishId;
                    writeOff.FirstOrDefault().Product = newProduct;
                    writeOff.FirstOrDefault().Cause = newCause;
                    writeOff.FirstOrDefault().Count = newCount;
                    writeOff.FirstOrDefault().Sum = newSum;
                    
                    db.Entry(writeOff.FirstOrDefault()).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return result;
        }


    }
}
