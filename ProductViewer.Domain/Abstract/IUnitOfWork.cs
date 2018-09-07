using System;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository ProductsRepository { get; }
        IProductDescriptionsRepository ProductDescriptionsRepository { get; }
        IProductInventoriesRepository ProductInventoriesRepository { get; }
        IProductListPriceHistoriesRepository ProductListPriceHistoriesRepository { get; }

        /// <summary>
        /// Commits all changes
        /// </summary>
        void Commit();
    }
}