using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using BE;
namespace User.Model
{
    class UserCollectionDL: DbContext
    {
        public DbSet<BE.User> DbUsers { get; set; }
        public DbSet<Test> Testes { get; set; } //forigin k to User
        public DbSet<Meal> Meals { get; set; }  //forigin k to User
        public DbSet<Food> FoodSet { get; set; } //forigin k to Meal
        public DbSet<DailyDosage> DbDailyDosage { get; set; }

        public UserCollectionDL()
        {
            Database.SetInitializer<UserCollectionDL>(new DropCreateDatabaseIfModelChanges<UserCollectionDL>());
        }
    }
}
