using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductModelRepository : IProductModelsRepository
    {
        private AdoNetContext _context;

        public ProductModelRepository(AdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductModel> GetProductModelList()
        {
            var productModels = _context.GetProductModels().Select();
            var productModelList = (from pm in productModels
                select new ProductModel()
                {
                    Name = (string)pm["Name"],
                    ProductModelID = (int)pm["ProductModelID"]
                });
            return productModelList;
        }

        public ProductModel GetProductModel(int id)
        {
            var productModels = _context.GetProductModels().Select();
            var productModel = (productModels.Where(pm => ((int)pm["ProductModelID"]) == id)).Select(pm => new ProductModel()
            {
                Name = (string)pm["Name"],
                ProductModelID = (int)pm["ProductModelID"]
            })?.FirstOrDefault();
            return productModel;
        }

        public void Create(ProductModel item)
        {
            var newRow = _context.GetProductModels().NewRow();
            newRow["Name"] = item.Name;
            _context.GetProductModels().Rows.Add(newRow);
            _context.CommitChanges();
            item.ProductModelID = (int)newRow["ProductModelID"];
        }

        public void Update(ProductModel item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductModels().Rows) // search whole table
            {
                if ((int)dr["ProductModelID"] == item.ProductModelID)
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["ProductModelID"] = item.ProductModelID;
                dataRow["Name"] = item.Name;
                _context.CommitChanges();
            }
        }

        public void Delete(int id)
        {
            foreach (DataRow dr in _context.GetProductModels().Rows)
            {
                if ((int)dr["ProductModelID"] == id)
                {
                    dr.Delete();
                    break;
                }
            }
            _context.CommitChanges();
        }
    }
}