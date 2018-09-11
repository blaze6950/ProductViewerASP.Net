namespace ProductViewer.Domain.Entities
{
    public class ProductModelProductDescriptionCulture // Cross-reference table mapping product descriptions and the language the description is written in.
    {
        public int ProductModelID { get; set; } // Primary key. Foreign key to ProductModel.ProductModelID.
        public int ProductDescriptionID { get; set; } // Primary key. Foreign key to ProductDescription.ProductDescriptionID.
        public static string CultureID
        {
            get => "en";
        } // Culture identification number. Foreign key to Culture.CultureID
    }
}