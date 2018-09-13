using System.Collections.Generic;
using System.Data;
using System.Linq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductModelProductDescriptionCultureRepository : IProductModelProductDescriptionCulturesRepository
    {
        private AdoNetContext _context;

        public ProductModelProductDescriptionCultureRepository(AdoNetContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultureList()
        {
            var productModelsProductDescriptionCultures = _context.GetProductModelProductDescriptionCulture().Select();
            var productModelProductDescriptionCultureList = (from pmpdc in productModelsProductDescriptionCultures
                select new ProductModelProductDescriptionCulture()
                {
                    ProductDescriptionID = (int)pmpdc["ProductDescriptionID"],
                    ProductModelID = (int)pmpdc["ProductModelID"]
                });
            return productModelProductDescriptionCultureList;
        }

        public ProductModelProductDescriptionCulture GetProductModelProductDescriptionCulture(int productModelId,
            int productDescriptionId)
        {
            var productModelsProductDescriptionCultures = _context.GetProductModelProductDescriptionCulture().Select();
            var productModelProductDescriptionCulture = (productModelsProductDescriptionCultures.Where(pmpdc => ((int)pmpdc["ProductModelID"]) == productModelId)).Select(pmpdc => new ProductModelProductDescriptionCulture()
            {
                ProductDescriptionID = (int)pmpdc["ProductDescriptionID"],
                ProductModelID = (int)pmpdc["ProductModelID"]
            })?.FirstOrDefault();
            return productModelProductDescriptionCulture;
        }

        public void Create(ProductModelProductDescriptionCulture item)
        {
            var newRow = _context.GetProductModelProductDescriptionCulture().NewRow();
            newRow["ProductModelID"] = item.ProductModelID;
            newRow["ProductDescriptionID"] = item.ProductDescriptionID;
            newRow["CultureID"] = item.CultureID;
            _context.GetProductModelProductDescriptionCulture().Rows.Add(newRow);
            _context.CommitChanges();
        }

        public void Update(Entities.ProductModelProductDescriptionCulture item)
        {
            DataRow dataRow = null;
            foreach (DataRow dr in _context.GetProductModelProductDescriptionCulture().Rows) // search whole table
            {
                if ((int)dr["ProductModelID"] == item.ProductModelID && (int)dr["ProductDescriptionID"] == item.ProductDescriptionID)
                {
                    dataRow = dr;
                    break;
                }
            }
            if (dataRow != null)
            {
                dataRow["ProductModelID"] = item.ProductModelID;
                dataRow["ProductDescriptionID"] = item.ProductDescriptionID;
                _context.CommitChanges();
            }
        }

        public void Delete(int productModelId, int productDescriptionId)
        {
            foreach (DataRow dr in _context.GetProductModelProductDescriptionCulture().Rows)
            {
                if ((int)dr["ProductModelID"] == productModelId && (int)dr["ProductDescriptionID"] == productDescriptionId)
                {
                    dr.Delete();
                    break;
                }
            }
            _context.CommitChanges();
        }
    }
}