using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdoNetContext _context;

        public UnitOfWork(AdoNetContext context)
        {
            _context = context;
            ProductsRepository = new ProductRepository(_context);
            ProductDescriptionsRepository = new ProductDescriptionRepository(_context);
            ProductInventoriesRepository = new ProductInventoryRepository(_context);
            ProductListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_context);
            ProductModelsRepository = new ProductModelRepository(_context);
            ProductModelProductDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_context);
        }

        public IProductsRepository ProductsRepository { get; }
        public IProductDescriptionsRepository ProductDescriptionsRepository { get; }
        public IProductInventoriesRepository ProductInventoriesRepository { get; }
        public IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository { get; }
        public IProductModelsRepository ProductModelsRepository { get; set; }
        public IProductModelProductDescriptionCulturesRepository ProductModelProductDescriptionCulturesRepository { get; set; }

        public void Commit()
        {
            _context?.CommitChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}