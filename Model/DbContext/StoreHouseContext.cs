using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.Model
{
    class StoreHouseContext : Microsoft.EntityFrameworkCore.DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-O510GJE;Database=StoreHouse;Trusted_Connection=True; TrustServerCertificate=True");
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<WriteOff> WriteOffs { get; set; }
        public DbSet<OutputAddDish> OutputAddDishes { get; set; }


        private static StoreHouseContext _Context;
        public static StoreHouseContext GetContext()
        {
            _Context = new StoreHouseContext();
            return _Context;
            //if (_Context == null)
            //    _Context = new StoreHouseContext();
            //return _Context;
        }
    }
}
