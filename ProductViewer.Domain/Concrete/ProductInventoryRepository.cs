using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductInventoryRepository : IProductInventoriesRepository
    {
        private AdoNetContext _context;

        public ProductInventoryRepository(AdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductInventory> GetProductInventoryList()
        {
            var productInventories = _context.GetProductInventories().Select();
            var productInventoryList = (from p in productInventories
                                          select new ProductInventory()
                                          {
                                              Bin = (int)p["Bin"],
                                              LocationID = (int)p["LocationID"],
                                              ProductID = (int)p["ProductID"],
                                              Quantity = (int)p["Quantity"],
                                              Shelf = (string)p["Shelf"]
                                          });
            return productInventoryList;
        }

        public ProductInventory GetProductInventory(int locationId, int productId)
        {
            var productInventories = _context.GetProductInventories().Select();
            var productInventory = (productInventories.Where(p => (((int)p["ProductID"]) == productId) && ((int)p["LocationID"]) == locationId)).Select(p => new ProductInventory()
            {
                Bin = (int)p["Bin"],
                LocationID = (int)p["LocationID"],
                ProductID = (int)p["ProductID"],
                Quantity = (int)p["Quantity"],
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
        }

        public void Update(ProductInventory item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductInventories().Rows) // search whole table
            {
                if ((int)dr["ProductID"] == item.ProductID && (int)dr["LocationID"] == item.LocationID)
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
            }
        }

        public void Delete(int locationId, int productId)
        {
            foreach (DataRow dr in _context.GetProductInventories().Rows)
            {
                if ((int)dr["ProductID"] == productId && (int)dr["LocationID"] == locationId)
                {
                    dr.Delete();
                    break;
                }
            }
        }
    }
}