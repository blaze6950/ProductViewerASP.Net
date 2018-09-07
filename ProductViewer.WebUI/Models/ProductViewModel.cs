using ProductViewer.Domain.Entities;

namespace ProductViewer.WebUI.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(Product productEntity, ProductDescription productDescriptionEntity, ProductInventory productInventoryEntity, ProductListPriceHistory productListPriceHistoryEntity)
        {
            ProductEntity = productEntity;
            ProductDescriptionEntity = productDescriptionEntity;
            ProductInventoryEntity = productInventoryEntity;
            ProductListPriceHistoryEntity = productListPriceHistoryEntity;
        }

        #region PrivateFields
        private Product ProductEntity { get; }
        private ProductDescription ProductDescriptionEntity { get; }
        private ProductInventory ProductInventoryEntity { get; }
        private ProductListPriceHistory ProductListPriceHistoryEntity { get; }
        #endregion

        #region PublicFieldsForView
        public string ProductName
        {
            get => ProductEntity.Name;
        }

        public string ProductDescription
        {
            get => ProductDescriptionEntity.Description;
        }

        public decimal ProductPrice
        {
            get => ProductListPriceHistoryEntity.ListPrice;
        }

        public int ProductQuantity
        {
            get => ProductInventoryEntity.Quantity;
        }

        #endregion

        public override string ToString()
        {
            // продукты, затем их описание, стоимость и количество на складе
            return $"Product: {ProductEntity.Name}, Description: {ProductDescriptionEntity.Description}, Price: {ProductListPriceHistoryEntity.ListPrice}, Count: {ProductInventoryEntity.Quantity}";
        }
    }
}