namespace ProductViewer.Domain.Entities
{
    public class ProductDescription // Product descriptions in several languages.
    {
        public int ProductDescriptionID { get; set; } // Primary key for ProductDescription records.
        public string Description { get; set; } // Description of the product
    }
}