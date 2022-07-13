using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<WorkersAndRestraunts> WorkersAndRestraunts { get; set; }

        public DbSet<StopItem> StopList { get; set; }


        public ApplicationDbContext(DbContextOptions opt) : base(opt) {

            Database.EnsureCreated();

        }
    }
}
