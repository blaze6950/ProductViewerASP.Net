using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductListPriceHistoryRepository : IProductListPriceHistoriesRepository
    {
        private IAdoNetContext _context;

        public ProductListPriceHistoryRepository(IAdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductListPriceHistory> GetProductListPriceHistoryList()
        {
            var productListPriceHistories = _context.GetProductListPriceHistories().Select();
            var productListPriceHistoryList = (from p in productListPriceHistories
                                        select new ProductListPriceHistory()
                                        {
                                            ProductID = (int)p["ProductID"],
                                            ListPrice = (decimal)p["ListPrice"],
                                            StartDate = (DateTime)p["StartDate"]
                                        });
            return productListPriceHistoryList;
        }

        public ProductListPriceHistory GetProductListPriceHistory(int productId, DateTime startDate)
        {
            var productListPriceHistories = _context.GetProductListPriceHistories().Select();
            var productListPriceHistory = (productListPriceHistories.Where(p => (((int)p["ProductID"]) == productId) && ((DateTime)p["StartDate"]) == startDate)).Select(p => new ProductListPriceHistory()
            {
                ProductID = (int)p["ProductID"],
                ListPrice = (decimal)p["ListPrice"],
                StartDate = (DateTime)p["StartDate"]
            })?.FirstOrDefault();
            return productListPriceHistory;
        }

        public void Create(ProductListPriceHistory item)
        {
            var newRow = _context.GetProductListPriceHistories().NewRow();
            newRow["ProductID"] = item.ProductID;
            newRow["ListPrice"] = item.ListPrice;
            newRow["StartDate"] = item.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _context.GetProductListPriceHistories().Rows.Add(newRow);
            _context.CommitChanges();
        }

        public void Update(ProductListPriceHistory item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductListPriceHistories().Rows) // search whole table
            {
                if (((int)dr["ProductID"] == item.ProductID) && ((DateTime)dr["StartDate"] == item.StartDate))
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["ProductID"] = item.ProductID;
                dataRow["ListPrice"] = item.ListPrice;
                dataRow["StartDate"] = item.StartDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                _context.CommitChanges();
            }
        }

        public void Delete(int productId, DateTime startDate)
        {
            foreach (DataRow dr in _context.GetProductListPriceHistories().Rows)
            {
                if ((int)dr["ProductID"] == productId && (DateTime)dr["StartDate"] == startDate)
                {
                    dr.Delete();
                    break;
                }
            }
            _context.CommitChanges();
        }
    }
}