using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class DataContext : DbContext
    {
        //public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        //public DbSet<CustomerPreference> CustomerPreferences { get; set; }
        //public DataContext() => Database.EnsureCreated();


        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        Email = "owner@somemail.ru",
                        FirstName = "Иван",
                        LastName = "Сергеев",
                            //Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
                            Role = null,
                        AppliedPromocodesCount = 5
                    }
            );
        }
    }
}
