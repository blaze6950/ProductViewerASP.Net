using System.Data;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;

        public UnitOfWork(IDbConnection context)
        {
            _connection = context;
            ProductsRepository = new ProductRepository(_connection);
            ProductDescriptionsRepository = new ProductDescriptionRepository(_connection);
            ProductInventoriesRepository = new ProductInventoryRepository(_connection);
            ProductListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_connection);
            ProductModelsRepository = new ProductModelRepository(_connection);
            ProductModelProductDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_connection);
        }

        public IProductsRepository ProductsRepository { get; }
        public IProductDescriptionsRepository ProductDescriptionsRepository { get; }
        public IProductInventoriesRepository ProductInventoriesRepository { get; }
        public IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository { get; }
        public IProductModelsRepository ProductModelsRepository { get; set; }
        public IProductModelProductDescriptionCulturesRepository ProductModelProductDescriptionCulturesRepository { get; set; }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}