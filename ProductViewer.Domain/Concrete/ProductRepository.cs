using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductRepository : IProductsRepository
    {
        private ProductInfoContext _context;

        public ProductRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _context.Products;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public void Create(Product item)
        {
            _context.Products.Add(item);
        }

        public void Update(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product book = _context.Products.Find(id);
            if (book != null)
                _context.Products.Remove(book);
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