using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductListPriceHistoryRepository : IProductListPriceHistoriesRepository
    {
        private ProductInfoContext _context;

        public ProductListPriceHistoryRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductListPriceHistory> GetProductListPriceHistoryList()
        {
            return _context.ProductListPriceHistories;
        }

        public ProductListPriceHistory GetProductListPriceHistory(int id)
        {
            return _context.ProductListPriceHistories.Find(id);
        }

        public void Create(ProductListPriceHistory item)
        {
            _context.ProductListPriceHistories.Add(item);
        }

        public void Update(ProductListPriceHistory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductListPriceHistory productListPriceHistory = _context.ProductListPriceHistories.Find(id);
            if (productListPriceHistory != null)
                _context.ProductListPriceHistories.Remove(productListPriceHistory);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}