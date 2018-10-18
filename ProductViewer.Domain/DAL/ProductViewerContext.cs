using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;
using ProductViewer.Domain.Migrations;

namespace ProductViewer.Domain.DAL
{
    public class ProductViewerContext : DbContext, IProductViewerContext
    {
        public ProductViewerContext() : base("name=ProductViewerContext")
        {
            //Database.SetInitializer(new ProductViewerInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductViewerContext, Configuration>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void SetModified(object entity)
        {
            if (entity != null) Entry(entity).State = EntityState.Modified;
        }

        public void SetDeleted(object entity)
        {
            if (entity != null) Entry(entity).State = EntityState.Deleted;
        }
    }
}
