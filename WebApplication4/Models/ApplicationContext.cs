using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class ApplicationContext : DbContext
    {
        static bool flag = false;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {
          
            Database.EnsureCreated();
        }
        public DbSet<Coins> Wallets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }



}
