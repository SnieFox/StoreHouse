using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string CurrentRemains { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal Sum { get; set; }

    }
}
