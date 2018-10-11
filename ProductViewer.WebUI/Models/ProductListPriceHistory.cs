using System;

namespace ProductViewer.WebUI.Models
{
    public class ProductListPriceHistory // Changes in the list price of a product over time.
    {
        public int ProductID { get; set; } // Product identification number. Foreign key to Product.ProductID.

        public DateTime StartDate { get; set; } // List price start date.

        public decimal ListPrice { get; set; } // Product list price.
    }
}