using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.OutputDataModels
{
    internal class OutputWriteOff
    {
        public string Date { get; set; }
        public string Product { get; set; }
        public string Sum { get; set; }
        public string Cause { get; set; }
        public string Count { get; set; }
        public int? IngredientId { get; set; }
        public int? DishId { get; set; }

        public OutputWriteOff(string count,string date,string product,string sum, string cause, int ingredientId, int dishtId)
        {
            Count = count;
            Date = date;
            Product = product;
            Sum = sum;
            Cause = cause;
            IngredientId = ingredientId;
            DishId = dishtId;

        }
        public OutputWriteOff(string count, string date, string product, string sum, string cause, int ingredientId)
        {
            Count = count;
            Date = date;
            Product = product;
            Sum = sum;
            Cause = cause;
            IngredientId = ingredientId;

        }
        public OutputWriteOff(string count, int dishtId, string date, string product, string sum, string cause)
        {
            Count = count;
            Date = date;
            Product = product;
            Sum = sum;
            Cause = cause;
            DishId = dishtId;

        }

    }
}
