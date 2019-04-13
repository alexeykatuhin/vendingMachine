using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.App_Code
{
    public static class DbInit
    {
        public static void Initialize(ApplicationContext context)
        {

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User("CUSTOMER"),
                    new User("VM"),
                    new User("TEMP"));
                context.SaveChanges();
            }

            if (!context.Wallets.Any())
            {
                var customer = context.Users.First(x => x.Name == "CUSTOMER");
                context.Wallets.AddRange(
                    new Coins(10, 1, customer.Id),
                    new Coins(30, 2, customer.Id),
                    new Coins(20, 5, customer.Id),
                    new Coins(15, 10, customer.Id));

                var vm = context.Users.First(x => x.Name == "VM");
                context.Wallets.AddRange(
                    new Coins(100, 1, vm.Id),
                    new Coins(100, 2, vm.Id),
                    new Coins(100, 5, vm.Id),
                    new Coins(100, 10, vm.Id));

                var temp = context.Users.First(x => x.Name == "TEMP");
                context.Wallets.AddRange(
                    new Coins(0, 1, temp.Id),
                    new Coins(0, 2, temp.Id),
                    new Coins(0, 5, temp.Id),
                    new Coins(0, 10, temp.Id));
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product("Чай", 13, 10),
                    new Product("Кофе", 18, 20),
                    new Product("Кофе с молоком", 21, 20),
                    new Product("Сок", 35, 15));
                context.SaveChanges();
            }

        }
    }
}
