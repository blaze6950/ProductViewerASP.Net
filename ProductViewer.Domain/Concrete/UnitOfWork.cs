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
            ProductsRepository = new ProductRepository(_connectionFactory.GetConnection);
            ProductDescriptionsRepository = new ProductDescriptionRepository(_connectionFactory.GetConnection);
            ProductInventoriesRepository = new ProductInventoryRepository(_connectionFactory.GetConnection);
            ProductListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_connectionFactory.GetConnection);
            ProductModelsRepository = new ProductModelRepository(_connectionFactory.GetConnection);
            ProductModelProductDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_connectionFactory.GetConnection);
        }

        public IProductsRepository ProductsRepository { get; }
        public IProductDescriptionsRepository ProductDescriptionsRepository { get; }
        public IProductInventoriesRepository ProductInventoriesRepository { get; }
        public IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository { get; }
        public IProductModelsRepository ProductModelsRepository { get; set; }
        public IProductModelProductDescriptionCulturesRepository ProductModelProductDescriptionCulturesRepository { get; set; }

        public void Dispose()
        {
            _connectionFactory?.GetConnection.Dispose();
        }
    }
}