using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;

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
    }
}
