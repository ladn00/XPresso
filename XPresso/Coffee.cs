using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPresso
{
    public class Coffee : Product
    {
        public int CoffeeStrength { get; set; }
        public int Sugar { get; set; }

        public Coffee(string Name, decimal Cost, string ImagePath, int CoffeeStrength, int Sugar) : base(Name, Cost, ImagePath)
        {
            this.CoffeeStrength = CoffeeStrength;
            this.Sugar = Sugar;
        }
    }
}
