using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductViewer.Domain.Entities
{
    public class ProductListPriceHistory // Changes in the list price of a product over time.
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductID { get; set; } // Product identification number. Foreign key to Product.ProductID
        public Product Product { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime StartDate { get; set; } // List price start date.

        public decimal ListPrice { get; set; } // Product list price.
    }
}