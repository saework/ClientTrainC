using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore; //!!!
using Microsoft.Extensions.Configuration; //!!!

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Startup
    {

        //!!!
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //!!!

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
/*            services.AddScoped(typeof(IRepository<Employee>), (x) => 
                new InMemoryRepository<Employee>(FakeDataFactory.Employees));
            services.AddScoped(typeof(IRepository<Role>), (x) => 
                new InMemoryRepository<Role>(FakeDataFactory.Roles));
            services.AddScoped(typeof(IRepository<Preference>), (x) => 
                new InMemoryRepository<Preference>(FakeDataFactory.Preferences));
            services.AddScoped(typeof(IRepository<Customer>), (x) => 
                new InMemoryRepository<Customer>(FakeDataFactory.Customers));*/


            //!!!
            var connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<DataContext>(options => options.UseSqlite(connection));
            //!!!

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        //!!!
/*        public class DataContext : DbContext
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
                Database.EnsureDeleted();
                Database.EnsureCreated();
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
        }*/

        
        //!!!

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //DbInitializer.Initialize(dataContext); //!!!

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });

            //app.UseHttpsRedirection(); //!!!
            //app.MapGet("/", (ApplicationContext db) => db.Users.ToList()); //!!!
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/db", (DataContext db) => db.Employees.ToList()); //!!!
            });
        }
    }
}