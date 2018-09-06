using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductDescriptionRepository : IProductDescriptionsRepository
    {
        private ProductInfoContext _context;

        public ProductDescriptionRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDescription> GetProductList()
        {
            return _context.ProductDescriptions;
        }

        public ProductDescription GetProduct(int id)
        {
            return _context.ProductDescriptions.Find(id);
        }

        public void Create(ProductDescription item)
        {
            _context.ProductDescriptions.Add(item);
        }

        public void Update(ProductDescription item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductDescription productDescription = _context.ProductDescriptions.Find(id);
            if (productDescription != null)
                _context.ProductDescriptions.Remove(productDescription);
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