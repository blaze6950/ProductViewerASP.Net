using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductRepository : IProductsRepository
    {
        private AdoNetContext _context;

        public ProductRepository(AdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductList()
        {
            var products = _context.GetProducts().Select();
            var productList = (from p in products
                                  select new Product(){
                                      DaysToManufacture = (int)p["DaysToManufacture"],
                                      ProductId = (int)p["ProductID"],
                                      ListPrice = (decimal)p["ListPrice"],
                                      Name = (string)p["Name"],
                                      ProductNumber = (string)p["ProductNumber"],
                                      ReorderPoint = (int)p["ReorderPoint"],
                                      SafetyStockLevel = (int)p["SafetyStockLevel"],
                                      SellStartDate = (DateTime)p["SellStartDate"],
                                      StandartCost = (decimal)p["StandartCost"]
                                  });
            return productList;
        }

        public Product GetProduct(int id)
        {
            var products = _context.GetProducts().Select();
            var product = (products.Where(p => ((int) p["ProductID"]) == id)).Select(p => new Product()
            {
                DaysToManufacture = (int)p["DaysToManufacture"],
                ProductId = (int)p["ProductID"],
                ListPrice = (decimal)p["ListPrice"],
                Name = (string)p["Name"],
                ProductNumber = (string)p["ProductNumber"],
                ReorderPoint = (int)p["ReorderPoint"],
                SafetyStockLevel = (int)p["SafetyStockLevel"],
                SellStartDate = (DateTime)p["SellStartDate"],
                StandartCost = (decimal)p["StandartCost"]
            })?.FirstOrDefault();
            return product;
        }

        public void Create(Product item)
        {
            var newRow = _context.GetProducts().NewRow();
            newRow["ProductID"] = item.ProductId;
            newRow["DaysToManufacture"] = item.DaysToManufacture;
            newRow["ListPrice"] = item.ListPrice;
            newRow["Name"] = item.Name;
            newRow["ProductNumber"] = item.ProductNumber;
            newRow["ReorderPoint"] = item.ReorderPoint;
            newRow["SafetyStockLevel"] = item.SafetyStockLevel;
            newRow["SellStartDate"] = item.SellStartDate;
            newRow["StandartCost"] = item.StandartCost;
            _context.GetProducts().Rows.Add(newRow);
        }

        public void Update(Product item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProducts().Rows) // search whole table
            {
                if ((int)dr["ProductID"] == item.ProductId) // if id==2
                {
                    dataRow = dr;
                    break;
                }
            }
            dataRow["ProductID"] = item.ProductId;
            dataRow["DaysToManufacture"] = item.DaysToManufacture;
            dataRow["ListPrice"] = item.ListPrice;
            dataRow["Name"] = item.Name;
            dataRow["ProductNumber"] = item.ProductNumber;
            dataRow["ReorderPoint"] = item.ReorderPoint;
            dataRow["SafetyStockLevel"] = item.SafetyStockLevel;
            dataRow["SellStartDate"] = item.SellStartDate;
            dataRow["StandartCost"] = item.StandartCost;
        }

        public void Delete(int id)
        {
            foreach (DataRow dr in _context.GetProducts().Rows)
            {
                if ((int)dr["ProductID"] == id) // if id==2
                {
                    dr.Delete();
                    break;
                }
            }
        }

        public void Save()
        {
            _context.CommitChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}