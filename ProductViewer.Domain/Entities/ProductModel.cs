using System;
using System.ComponentModel.DataAnnotations;
// ReSharper disable InconsistentNaming

namespace ProductViewer.Domain.Entities
{
    public class ProductModel // Product model classification.
    {
        private int _productModelId;
        public event Action<int> ProductModelIDUpdated;

        [Key]
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