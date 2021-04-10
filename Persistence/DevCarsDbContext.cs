using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DevCars.API.Entities;
using DevCars.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base(options)
        {
            
        }

        public DbSet<Car> Cars { set; get; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.ApplyConfiguration(new CarDbConfiguration());

            //modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());

            //modelBuilder.ApplyConfiguration(new OrderDbConfiguration());

            //modelBuilder.ApplyConfiguration(new ExtraOrderItemDbConfiguration());



        }
    }
}
