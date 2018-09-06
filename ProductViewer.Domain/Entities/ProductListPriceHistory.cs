using System;

namespace ProductViewer.Domain.Entities
{
    public class ProductListPriceHistory // Changes in the list price of a product over time.
    {
        public int ProductID { get; set; } // Product identification number. Foreign key to Product.ProductID.

        public DateTime StartDate { get; set; } // List price start date.

        public DateTime EndDate { get; set; } // List price end date.

        public decimal ListPrice { get; set; } // Product list price.
    }
}