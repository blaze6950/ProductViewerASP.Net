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
                                      ReorderPoint = (Int16)p["ReorderPoint"],
                                      SafetyStockLevel = (Int16)p["SafetyStockLevel"],
                                      SellStartDate = (DateTime)p["SellStartDate"],
                                      StandardCost = (decimal)p["StandardCost"],
                                      ProductModelID = (int?)p["ProductModelID"]
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
                ReorderPoint = (Int16)p["ReorderPoint"],
                SafetyStockLevel = (Int16)p["SafetyStockLevel"],
                SellStartDate = (DateTime)p["SellStartDate"],
                StandardCost = (decimal)p["StandardCost"],
                ProductModelID = (int?)p["ProductModelID"]
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
            newRow["StandardCost"] = item.StandardCost;
            newRow["ProductModelID"] = item.ProductModelID;
            _context.GetProducts().Rows.Add(newRow);
        }

        public void Update(Product item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProducts().Rows) // search whole table
            {
                if ((int)dr["ProductID"] == item.ProductId) 
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["ProductID"] = item.ProductId;
                dataRow["DaysToManufacture"] = item.DaysToManufacture;
                dataRow["ListPrice"] = item.ListPrice;
                dataRow["Name"] = item.Name;
                dataRow["ProductNumber"] = item.ProductNumber;
                dataRow["ReorderPoint"] = item.ReorderPoint;
                dataRow["SafetyStockLevel"] = item.SafetyStockLevel;
                dataRow["SellStartDate"] = item.SellStartDate;
                dataRow["StandartCost"] = item.StandardCost;
                dataRow["ProductModelID"] = item.ProductModelID;
            }
        }

        public void Delete(int id)
        {
            foreach (DataRow dr in _context.GetProducts().Rows)
            {
                if ((int)dr["ProductID"] == id) 
                {
                    dr.Delete();
                    break;
                }
            }
        }
    }
}