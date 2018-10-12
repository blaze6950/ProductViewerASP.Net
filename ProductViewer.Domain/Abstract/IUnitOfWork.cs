using System;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductsRepository { get; }
        IRepository<ProductDescription> ProductDescriptionsRepository { get; }
        IRepository<ProductInventory> ProductInventoriesRepository { get; }
        IRepository<ProductListPriceHistory> ProductListPriceHistoriesRepository { get; }
        IRepository<ProductModel> ProductModelsRepository { get; }
        IRepository<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulturesRepository { get; }

        void Commit();
    }
}