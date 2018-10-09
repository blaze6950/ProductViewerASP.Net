using System.Data;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionFactory _connectionFactory;

        private IProductsRepository _productsRepository;
        private IProductDescriptionsRepository _productDescriptionsRepository;
        private IProductInventoriesRepository _productInventoriesRepository;
        private IProductListPriceHistoriesRepository _productListPriceHistoriesRepository;
        private IProductModelsRepository _productModelsRepository;
        private IProductModelProductDescriptionCulturesRepository _productModelProductDescriptionCulturesRepository;

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IProductsRepository ProductsRepository
        {
            get => _productsRepository ?? (ProductsRepository = new ProductRepository(_connectionFactory.GetTransaction));
            private set => _productsRepository = value;
        }

        public IProductDescriptionsRepository ProductDescriptionsRepository
        {
            get => _productDescriptionsRepository ?? (ProductDescriptionsRepository = new ProductDescriptionRepository(_connectionFactory.GetTransaction));
            private set => _productDescriptionsRepository = value;
        }

        public IProductInventoriesRepository ProductInventoriesRepository
        {
            get => _productInventoriesRepository ?? (ProductInventoriesRepository = new ProductInventoryRepository(_connectionFactory.GetTransaction));
            private set => _productInventoriesRepository = value;
        }

        public IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository
        {
            get => _productListPriceHistoriesRepository ?? (ProductListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_connectionFactory.GetTransaction));
            private set => _productListPriceHistoriesRepository = value;
        }

        public IProductModelsRepository ProductModelsRepository
        {
            get => _productModelsRepository ?? (ProductModelsRepository = new ProductModelRepository(_connectionFactory.GetTransaction));
            private set => _productModelsRepository = value;
        }

        public IProductModelProductDescriptionCulturesRepository ProductModelProductDescriptionCulturesRepository
        {
            get => _productModelProductDescriptionCulturesRepository ?? (ProductModelProductDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_connectionFactory.GetTransaction));
            private set => _productModelProductDescriptionCulturesRepository = value;
        }

        public void Commit()
        {
            try
            {
                _connectionFactory.GetTransaction.Commit();
            }
            catch
            {
                _connectionFactory.GetTransaction.Rollback();
                throw;
            }
            finally
            {
                _connectionFactory.GetTransaction.Dispose();
                _connectionFactory.ResetTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            ProductsRepository = null;
            ProductDescriptionsRepository = null;
            ProductInventoriesRepository = null;
            ProductListPriceHistoriesRepository = null;
            ProductModelsRepository = null;
            ProductModelProductDescriptionCulturesRepository = null;
        }

        public void Dispose()
        {
            _connectionFactory?.Dispose();
        }
    }
}