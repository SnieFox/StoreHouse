using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Models;

namespace StoreHouse.Model.OutputDataModels
{
    class OutputAddDish
    {
        public int Id { get; set; }
        public int? DishId { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public string Sum { get; set; }
        public Dish Dish { get; set; }
        
        public OutputAddDish(string name, string count, string sum)
        {
            Name = name;
            Count = count;
            Sum = sum;
        }

        public OutputAddDish()
        {

        }
    }
}
