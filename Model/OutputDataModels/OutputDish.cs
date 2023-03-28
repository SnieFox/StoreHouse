using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.OutputDataModels
{
    class OutputDish
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string PrimeCost { get; set; }
        public string Price { get; set; }

        public OutputDish(string name, string type, string category, string primeCost, string price)
        {
            Name = name;
            Type = type;
            Category = category;
            PrimeCost = primeCost;
            Price = price;
        }
    }
}
