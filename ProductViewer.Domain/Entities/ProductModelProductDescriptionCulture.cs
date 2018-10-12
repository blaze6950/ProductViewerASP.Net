using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductViewer.Domain.Entities
{
    public class ProductModelProductDescriptionCulture // Cross-reference table mapping product descriptions and the language the description is written in.
    {
        public ProductModelProductDescriptionCulture()
        {
            CultureID = "en";
        }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("ProductModel")]
        public int ProductModelID { get; set; } // Primary key. Foreign key to ProductModel.ProductModelID.
        public ProductModel ProductModel { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("ProductDescription")]
        public int ProductDescriptionID { get; set; } // Primary key. Foreign key to ProductDescription.ProductDescriptionID.
        public ProductDescription ProductDescription { get; set; }

        [Key]
        [Column(Order = 3)]
        public string CultureID { get; set; } // Culture identification number. Foreign key to Culture.CultureID
    }
}