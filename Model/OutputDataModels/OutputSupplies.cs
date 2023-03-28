using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.Model.OutputDataModels
{
    internal class OutputSupplies
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Supplier { get; set; }
        public string Product { get; set; }
        public string Comment { get; set; }
        public string Sum { get; set; }

        public OutputSupplies(string id ,string date, string supplier, string product, string comment, string sum)
        {
            Id = id;
            Date = date;
            Supplier = supplier;
            Product = product;
            Comment = comment;
            Sum = sum;
        }
    }


}
