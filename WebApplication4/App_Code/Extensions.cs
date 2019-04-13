using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.App_Code
{
    public static class Extensions
    {
        public static int GetSum(this List<Coins> list)
        {
            int sum = 0;
            list.ForEach(x => sum += x.Quantity * x.Value);
            return sum;
        }

   
    }
}
