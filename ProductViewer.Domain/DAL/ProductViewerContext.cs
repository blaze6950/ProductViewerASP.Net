using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.DAL
{
    public class ProductViewerContext : DbContext
    {
        public ProductViewerContext() : base("name=ProductViewerContext")
        {
            Database.SetInitializer(new ProductViewerInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
