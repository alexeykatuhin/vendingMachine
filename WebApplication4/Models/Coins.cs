using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Coins
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
        public Coins(int quantity, int value, int userId)
        {
            Quantity = quantity;
            Value = value;
            UserId = userId;
        }

        public User User { get; set; }
    }
}
