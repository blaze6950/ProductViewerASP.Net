using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductInventoryRepository : IProductInventoriesRepository
    {
        private IAdoNetContext _context;

        public ProductInventoryRepository(IAdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductInventory> GetProductInventoryList()
        {
            var productInventories = _context.GetProductInventories().Select();
            var productInventoryList = (from p in productInventories
                                          select new ProductInventory()
                                          {
                                              Bin = (byte)p["Bin"],
                                              LocationID = (Int16)p["LocationID"],
                                              ProductID = (int)p["ProductID"],
                                              Quantity = (Int16)p["Quantity"],
                                              Shelf = (string)p["Shelf"]
                                          });
            return productInventoryList;
        }

        public ProductInventory GetProductInventory(Int16 locationId, int productId)
        {
            var productInventories = _context.GetProductInventories().Select();
            var productInventory = (productInventories.Where(p => (((int)p["ProductID"]) == productId) && ((Int16)p["LocationID"]) == locationId)).Select(p => new ProductInventory()
            {
                Bin = (byte)p["Bin"],
                LocationID = (Int16)p["LocationID"],
                ProductID = (int)p["ProductID"],
                Quantity = (Int16)p["Quantity"],
                Shelf = (string)p["Shelf"]
            })?.FirstOrDefault();
            return productInventory;
        }

        public void Create(ProductInventory item)
        {
            var newRow = _context.GetProductInventories().NewRow();
            newRow["Bin"] = item.Bin;
            newRow["LocationID"] = item.LocationID;
            newRow["ProductID"] = item.ProductID;
            newRow["Quantity"] = item.Quantity;
            newRow["Shelf"] = item.Shelf;
            _context.GetProductInventories().Rows.Add(newRow);
            _context.CommitChanges();
        }

        public void Update(ProductInventory item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductInventories().Rows) // search whole table
            {
                if ((int)dr["ProductID"] == item.ProductID && (Int16)dr["LocationID"] == item.LocationID)
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["Bin"] = item.Bin;
                dataRow["LocationID"] = item.LocationID;
                dataRow["ProductID"] = item.ProductID;
                dataRow["Quantity"] = item.Quantity;
                dataRow["Shelf"] = item.Shelf;
                _context.CommitChanges();
            }
        }

        public void Delete(Int16 locationId, int productId)
        {
            foreach (DataRow dr in _context.GetProductInventories().Rows)
            {
                if ((int)dr["ProductID"] == productId && (Int16)dr["LocationID"] == locationId)
                {
                    dr.Delete();
                    break;
                }
            }
            _context.CommitChanges();
        }
    }
}