using System.Data;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionFactory _connectionFactory;

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            ProductsRepository = new ProductRepository(_connectionFactory);
            ProductDescriptionsRepository = new ProductDescriptionRepository(_connectionFactory);
            ProductInventoriesRepository = new ProductInventoryRepository(_connectionFactory);
            ProductListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_connectionFactory);
            ProductModelsRepository = new ProductModelRepository(_connectionFactory);
            ProductModelProductDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_connectionFactory);
        }

        public IProductsRepository ProductsRepository { get; }
        public IProductDescriptionsRepository ProductDescriptionsRepository { get; }
        public IProductInventoriesRepository ProductInventoriesRepository { get; }
        public IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository { get; }
        public IProductModelsRepository ProductModelsRepository { get; set; }
        public IProductModelProductDescriptionCulturesRepository ProductModelProductDescriptionCulturesRepository { get; set; }

        public void Dispose()
        {
            _connectionFactory?.Dispose();
        }
    }
}