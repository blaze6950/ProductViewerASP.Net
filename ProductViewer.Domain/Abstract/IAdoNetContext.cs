using System;
using System.Data;

namespace ProductViewer.Domain.Abstract
{
    public interface IAdoNetContext : IDisposable
    {
        void CommitChanges();
        DataTable GetProductDescriptions();
        DataTable GetProductInventories();
        DataTable GetProductListPriceHistories();
        DataTable GetProductModelProductDescriptionCulture();
        DataTable GetProductModels();
        DataTable GetProducts();
        void RefreshData();
    }
}