using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductDescriptionRepository : IProductDescriptionsRepository
    {
        private AdoNetContext _context;

        public ProductDescriptionRepository(AdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDescription> GetProductDescriptionList()
        {
            var productDescriptions = _context.GetProductDescriptions().Select();
            var productDescriptionList = (from p in productDescriptions
                               select new ProductDescription()
                               {
                                   Description = (string)p["Description"],
                                   ProductDescriptionID = (int)p["ProductDescriptionID"]
                               });
            return productDescriptionList;
        }

        public ProductDescription GetProductDescription(int id)
        {
            var productDescriptions = _context.GetProductDescriptions().Select();
            var productDescription = (productDescriptions.Where(p => ((int)p["ProductDescriptionID"]) == id)).Select(p => new ProductDescription()
            {
                Description = (string)p["Description"],
                ProductDescriptionID = (int)p["ProductDescriptionID"]
            })?.FirstOrDefault();
            return productDescription;
        }

        public void Create(ProductDescription item)
        {
            var newRow = _context.GetProductDescriptions().NewRow();
            newRow["Description"] = item.Description;
            _context.GetProductDescriptions().Rows.Add(newRow);
            _context.CommitChanges();
            item.ProductDescriptionID = (int)newRow["ProductDescriptionID"];
        }

        public void Update(ProductDescription item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductDescriptions().Rows) // search whole table
            {
                if ((int)dr["ProductDescriptionID"] == item.ProductDescriptionID)
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["ProductDescriptionID"] = item.ProductDescriptionID;
                dataRow["Description"] = item.Description;
                _context.CommitChanges();
            }
        }

        public void Delete(int id)
        {
            foreach (DataRow dr in _context.GetProductDescriptions().Rows)
            {
                if ((int)dr["ProductDescriptionID"] == id)
                {
                    dr.Delete();
                    break;
                }
            }
            _context.CommitChanges();
        }
    }
}