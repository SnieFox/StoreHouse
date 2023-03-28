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

        public OutputWriteOff(string date,string product,string sum, string cause)
        {
            Date = date;
            Product = product;
            Sum = sum;
            Cause = cause;
        }

    }
}
