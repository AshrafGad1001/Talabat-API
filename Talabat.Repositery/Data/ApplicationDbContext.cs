using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregate;
using Talabat.Repositery.Data.Config;

namespace Talabat.Repositery.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //OR Call All ConFiguration
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
