using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.Models
{
    class Supply
    {
         public int Id { get; set; }
         public int IngredientId { get; set; }
         public double Count { get; set; }
         public string Date { get; set; }
         public string Supplier { get; set; }
         public string Product { get; set; }
         public string? Comment { get; set; }
         public decimal Sum { get; set; }
    }
}
