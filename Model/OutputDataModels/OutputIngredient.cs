using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.OutputDataModels
{
    internal class OutputIngredient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string CurrentRemains { get; set; }
        public string PrimeCost { get; set; }
        public string Sum { get; set; }
        public string Type { get; set; }

        public OutputIngredient(string name, string unit, string currentRemains, string primeCost, string sum, string type)
        {
            Name = name;
            Unit = unit;
            CurrentRemains = currentRemains;
            PrimeCost = primeCost;
            Sum = sum;
            Type = type;
        }
    }
}
