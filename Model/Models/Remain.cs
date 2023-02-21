using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.Models
{
    class Remain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string CurrentRemains { get; set; }
        public decimal PrimeCost { get; set; }
        public decimal Sum { get; set; }

    }
}
