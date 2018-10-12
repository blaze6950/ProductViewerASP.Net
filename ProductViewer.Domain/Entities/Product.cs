using System;
using System.ComponentModel.DataAnnotations;

namespace ProductViewer.Domain.Entities
{
    public class Product // Products sold or used in the manfacturing of sold products.
    {
        private int _productId;
        private string _name;
        public event Action<int> ProductIDUpdated;
        public event Action<string> ProductNameUpdated;

        [Key]
        public int ProductID
        {
            get { return _productId; }
            set
            {
                _productId = value;
                ProductIDUpdated?.Invoke(_productId);
            }
        } // Primary key for Product records.

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                ProductNameUpdated?.Invoke(_name);
            }
        } // Name of the product.

        public string ProductNumber { get; set; } // Unique product identification number.
        public Int16 SafetyStockLevel { get; set; } // Minimum inventory quantity.
        public Int16 ReorderPoint { get; set; } // Inventory level that triggers a purchase order or work order.
        public decimal StandardCost { get; set; } // Standard cost of the product.
        public decimal ListPrice { get; set; } // Selling price.
        public int DaysToManufacture { get; set; } // Number of days required to manufacture the product.
        public DateTime SellStartDate { get; set; } // Date the product was available for sale.

        public int? ProductModelID  { get; set; } // Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
        public ProductModel ProductModel { get; set; }
    }
}
