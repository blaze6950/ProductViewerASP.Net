using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Entities
{
    public class Product // Products sold or used in the manfacturing of sold products.
    {
        private int _productId;
        public event Action<int> ProductIDUpdated;

        public int ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                ProductIDUpdated?.Invoke(_productId);
            }
        } // Primary key for Product records.

        public string Name { get; set; } // Name of the product.
        public string ProductNumber { get; set; } // Unique product identification number.
        public Int16 SafetyStockLevel { get; set; } // Minimum inventory quantity.
        public Int16 ReorderPoint { get; set; } // Inventory level that triggers a purchase order or work order.
        public decimal StandardCost { get; set; } // Standard cost of the product.
        public decimal ListPrice { get; set; } // Selling price.
        public int DaysToManufacture { get; set; } // Number of days required to manufacture the product.
        public DateTime SellStartDate { get; set; } // Date the product was available for sale.
        public int? ProductModelID  { get; set; } // Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
    }
}
