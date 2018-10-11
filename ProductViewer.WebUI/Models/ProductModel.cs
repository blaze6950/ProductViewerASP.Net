using System;

namespace ProductViewer.WebUI.Models
{
    public class ProductModel // Product model classification.
    {
        private int _productModelId;
        public event Action<int> ProductModelIDUpdated;

        public int ProductModelID
        {
            get { return _productModelId; }
            set
            {
                _productModelId = value;
                ProductModelIDUpdated?.Invoke(_productModelId);
            }
        } // Primary key for ProductModel records

        public string Name { get; set; } // Product model description
    }
}