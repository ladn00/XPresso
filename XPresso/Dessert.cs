using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPresso
{
    public class Dessert : Product
    {
        public double Kcal { get; set; }

        public Dessert(string Name, decimal Cost, string ImagePath, double Kcal) : base(Name, Cost, ImagePath)
        {
            this.Kcal = Kcal;
        }
    }
}
