namespace ProductViewer.WebUI.Models
{
    public class ProductModelProductDescriptionCulture // Cross-reference table mapping product descriptions and the language the description is written in.
    {
        public ProductModelProductDescriptionCulture()
        {
            CultureID = "en";
        }

        public int ProductModelID { get; set; } // Primary key. Foreign key to ProductModel.ProductModelID.
        public int ProductDescriptionID { get; set; } // Primary key. Foreign key to ProductDescription.ProductDescriptionID.
        public string CultureID { get; set; } // Culture identification number. Foreign key to Culture.CultureID
    }
}