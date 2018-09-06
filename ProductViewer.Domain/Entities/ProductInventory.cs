namespace ProductViewer.Domain.Entities
{
    public class ProductInventory // Product inventory information
    {
        public int ProductID { get; set; } // Product identification number. Foreign key to Product.ProductID
        public int LocationID { get; set; } // Inventory location identification number. Foreign key to Location.LocationID
        public string Shelf { get; set; } // Storage compartment within an inventory location.
        public int Bin { get; set; } // Storage container on a shelf in an inventory location.
        public int Quantity { get; set; } // Quantity of products in the inventory location.
    }
}