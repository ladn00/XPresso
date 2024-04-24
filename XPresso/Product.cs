using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPresso
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string ImagePath { get; set; }

        public Product(string Name, decimal Cost, string ImagePath)
        {
            this.Name = Name;
            this.Cost = Cost;
            this.ImagePath = ImagePath;
        }
    }
}
