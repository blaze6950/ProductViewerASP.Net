using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable InconsistentNaming

namespace ProductViewer.Domain.Entities
{
    public class ProductInventory // Product inventory information
    {
        public ProductInventory()
        {
            LocationID = 1;
        }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductID { get; set; } // Product identification number. Foreign key to Product.ProductID
        public Product Product { get; set; }

        [Key]
        [Column(Order = 2)]
        public Int16 LocationID { get; set; } // Inventory location identification number. Foreign key to Location.LocationID
        public string Shelf { get; set; } // Storage compartment within an inventory location.
        public byte Bin { get; set; } // Storage container on a shelf in an inventory location.
        public Int16 Quantity { get; set; } // Quantity of products in the inventory location.
    }
}