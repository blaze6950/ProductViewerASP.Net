using System;

namespace ProductViewer.Domain.Entities
{
    public class ProductDescription // Product descriptions in several languages.
    {
        private int _productDescriptionId;
        public event Action<int> ProductDescriptionIDUpdated;

        public int ProductDescriptionID
        {
            get { return _productDescriptionId; }
            set
            {
                _productDescriptionId = value;
                ProductDescriptionIDUpdated?.Invoke(_productDescriptionId);
            }
        } // Primary key for ProductDescription records.

        public string Description { get; set; } // Description of the product
    }
}