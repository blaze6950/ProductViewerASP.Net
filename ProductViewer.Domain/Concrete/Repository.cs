using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.DAL;

namespace ProductViewer.Domain.Concrete
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly IProductViewerContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(IProductViewerContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public T FindById(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Delete(T item)
        {
            _context.SetDeleted(item);
        }

        public void Update(T item)
        {
            _context.SetModified(item);
        }
    }
}
