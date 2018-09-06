using System.Data.Entity;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductInfoContext : DbContext
    {
        public ProductInfoContext() : base("DefaultConnection") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
    }
}