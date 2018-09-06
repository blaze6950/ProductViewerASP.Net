using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductViewer.Domain.Entities
{
    class Product // Products sold or used in the manfacturing of sold products.
    {
        public int ProductId { get; set; } // Primary key for Product records.
        public string Name { get; set; } // Name of the product.
        public string ProductNumber { get; set; } // Unique product identification number.
        public int SafetyStockLevel { get; set; } // Minimum inventory quantity.
        public int ReorderPoint { get; set; } // Inventory level that triggers a purchase order or work order.
        public decimal StandartCost { get; set; } // Standard cost of the product.
        public decimal ListPrice { get; set; } // Selling price.
        public int DaysToManufacture { get; set; } // Number of days required to manufacture the product.
        public DateTime SellStartDate { get; set; } // Date the product was available for sale.
    }
}
