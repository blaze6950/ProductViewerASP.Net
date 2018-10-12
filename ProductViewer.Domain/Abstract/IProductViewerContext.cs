using System;
using System.Data.Entity;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductViewerContext : IDisposable
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductDescription> ProductDescriptions { get; set; }
        DbSet<ProductInventory> ProductInventories { get; set; }
        DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        DbSet<ProductModel> ProductModels { get; set; }
        DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
    }
}