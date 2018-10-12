using System.Data.Entity;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.DAL;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductViewerContext _dbContext;

        private IRepository<Product> _productsRepository;
        private IRepository<ProductDescription> _productDescriptionsRepository;
        private IRepository<ProductInventory> _productInventoriesRepository;
        private IRepository<ProductListPriceHistory> _productListPriceHistoriesRepository;
        private IRepository<ProductModel> _productModelsRepository;
        private IRepository<ProductModelProductDescriptionCulture> _productModelProductDescriptionCulturesRepository;

        public UnitOfWork(ProductViewerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Product> ProductsRepository
        {
            get => _productsRepository ?? (ProductsRepository = new Repository<Product>(_dbContext));
            private set => _productsRepository = value;
        }

        public IRepository<ProductDescription> ProductDescriptionsRepository
        {
            get => _productDescriptionsRepository ?? (ProductDescriptionsRepository = new Repository<ProductDescription>(_dbContext));
            private set => _productDescriptionsRepository = value;
        }

        public IRepository<ProductInventory> ProductInventoriesRepository
        {
            get => _productInventoriesRepository ?? (ProductInventoriesRepository = new Repository<ProductInventory>(_dbContext));
            private set => _productInventoriesRepository = value;
        }

        public IRepository<ProductListPriceHistory> ProductListPriceHistoriesRepository
        {
            get => _productListPriceHistoriesRepository ?? (ProductListPriceHistoriesRepository = new Repository<ProductListPriceHistory>(_dbContext));
            private set => _productListPriceHistoriesRepository = value;
        }

        public IRepository<ProductModel> ProductModelsRepository
        {
            get => _productModelsRepository ?? (ProductModelsRepository = new Repository<ProductModel>(_dbContext));
            private set => _productModelsRepository = value;
        }

        public IRepository<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulturesRepository
        {
            get => _productModelProductDescriptionCulturesRepository ?? (ProductModelProductDescriptionCulturesRepository = new Repository<ProductModelProductDescriptionCulture>(_dbContext));
            private set => _productModelProductDescriptionCulturesRepository = value;
        }

        public void Commit()
        {
            _dbContext?.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}