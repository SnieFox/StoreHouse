using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.Models
{
    class WriteOff
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string Date { get; set; }
        public string Product { get; set; }
        public decimal Sum { get; set; }
        public string Cause { get; set; }

    }
}
