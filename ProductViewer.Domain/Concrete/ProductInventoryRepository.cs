using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductInventoryRepository : IProductInventoriesRepository
    {
        private ProductInfoContext _context;

        public ProductInventoryRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductInventory> GetProductList()
        {
            return _context.ProductInventories;
        }

        public ProductInventory GetProduct(int id)
        {
            return _context.ProductInventories.Find(id);
        }

        public void Create(ProductInventory item)
        {
            _context.ProductInventories.Add(item);
        }

        public void Update(ProductInventory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductInventory productInventory = _context.ProductInventories.Find(id);
            if (productInventory != null)
                _context.ProductInventories.Remove(productInventory);
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