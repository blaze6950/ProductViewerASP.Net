using System.Web.Mvc;
using ProductViewer.Domain.Entities;

namespace ProductViewer.WebUI.Models
{
    public class ProductViewModelBuilder
    {
        private ProductViewModel _productViewModel;

        public ProductViewModelBuilder()
        {
            ProductEntity = new Product();
            ProductDescriptionEntity = new ProductDescription();
            ProductInventoryEntity = new ProductInventory();
            ProductListPriceHistoryEntity = new ProductListPriceHistory();
            ProductModelProductDescriptionCultureEntity = new ProductModelProductDescriptionCulture();
            ProductModelEntity = new ProductModel();

            AddReferencesBetweenFieldsOfEntities();
        }

        public ProductViewModelBuilder(ProductViewModel productViewModel)
        {
            ProductEntity = new Product();
            ProductDescriptionEntity = new ProductDescription();
            ProductInventoryEntity = new ProductInventory();
            ProductListPriceHistoryEntity = new ProductListPriceHistory();
            ProductModelProductDescriptionCultureEntity = new ProductModelProductDescriptionCulture();
            ProductModelEntity = new ProductModel();

            ProductViewModel = productViewModel;

            AddReferencesBetweenFieldsOfEntities();
        }

        public ProductViewModelBuilder(Product productEntity, ProductDescription productDescriptionEntity, ProductInventory productInventoryEntity, ProductListPriceHistory productListPriceHistoryEntity, ProductModelProductDescriptionCulture productModelProductDescriptionCultureEntity, ProductModel productModelEntity)
        {
            ProductEntity = productEntity;
            ProductDescriptionEntity = productDescriptionEntity;
            ProductInventoryEntity = productInventoryEntity;
            ProductListPriceHistoryEntity = productListPriceHistoryEntity;
            ProductModelProductDescriptionCultureEntity = productModelProductDescriptionCultureEntity;
            ProductModelEntity = productModelEntity;

            AddReferencesBetweenFieldsOfEntities();
        }

        private void AddReferencesBetweenFieldsOfEntities()
        {
            ProductModelEntity.ProductModelIDUpdated += newId =>
            {
                ProductEntity.ProductModelID = newId;
                ProductModelProductDescriptionCultureEntity.ProductModelID = newId;
            };
            ProductEntity.ProductIDUpdated += newId =>
            {
                ProductListPriceHistoryEntity.ProductID = newId;
                ProductInventoryEntity.ProductID = newId;
            };
            ProductEntity.ProductNameUpdated += newName =>
            {
                ProductModelEntity.Name = newName;
            };
            ProductDescriptionEntity.ProductDescriptionIDUpdated += newId =>
            {
                ProductModelProductDescriptionCultureEntity.ProductDescriptionID = newId;
            };
            ProductEntity.Name = ProductEntity.Name;
            ProductModelEntity.ProductModelID = ProductModelEntity.ProductModelID;
            ProductEntity.ProductId = ProductEntity.ProductId;
            ProductDescriptionEntity.ProductDescriptionID = ProductDescriptionEntity.ProductDescriptionID;
        }

        #region RequiredFields
        public Product ProductEntity { get; private set; }
        public ProductDescription ProductDescriptionEntity { get; private set; }
        public ProductInventory ProductInventoryEntity { get; private set; }
        public ProductListPriceHistory ProductListPriceHistoryEntity { get; private set; }
        public ProductModelProductDescriptionCulture ProductModelProductDescriptionCultureEntity { get; private set; }
        public ProductModel ProductModelEntity { get; private set; }
        #endregion

        public ProductViewModel ProductViewModel
        {
            get
            {
                if (_productViewModel == null)
                {
                    _productViewModel = BuildProductViewModel();
                }
                return _productViewModel;
            }
            set { BuildMainEntitiesFromProductModelView(value); }
        }

        private void BuildMainEntitiesFromProductModelView(ProductViewModel value)
        {
            _productViewModel = null;
            ProductDescriptionEntity.Description = value.ProductDescriptionEntityDescription;
            ProductEntity.DaysToManufacture = value.ProductEntityDaysToManufacture;
            ProductEntity.ProductId = value.ProductEntityId;
            ProductEntity.ListPrice = value.ProductEntityListPrice;
            ProductEntity.Name = value.ProductEntityName;
            ProductEntity.ProductNumber = value.ProductEntityNumber;
            ProductEntity.ReorderPoint = value.ProductEntityReorderPoint;
            ProductEntity.SafetyStockLevel = value.ProductEntitySafetyStockLevel;
            ProductEntity.SellStartDate = value.ProductEntitySellStartDate;
            ProductEntity.StandardCost = value.ProductEntityStandardCost;
            ProductInventoryEntity.Bin = value.ProductInventoryEntityBin;
            ProductInventoryEntity.Quantity = value.ProductInventoryEntityQuantity;
            ProductInventoryEntity.Shelf = value.ProductInventoryEntityShelf;
            ProductListPriceHistoryEntity.ListPrice = value.ProductListPriceHistoryEntityListPrice;
            ProductListPriceHistoryEntity.StartDate = value.ProductListPriceHistoryEntityStartDate;
            ProductModelEntity.ProductModelID = value.ProductModelEntityProductModelID;
            ProductDescriptionEntity.ProductDescriptionID = value.ProductDescriptionEntityProductDescriptionID;
            AddReferencesBetweenFieldsOfEntities();
        }

        private ProductViewModel BuildProductViewModel()
        {
            var newProductViewModel = new ProductViewModel(this)
            {
                ProductDescriptionEntityDescription = ProductDescriptionEntity.Description,
                ProductEntityDaysToManufacture = ProductEntity.DaysToManufacture,
                ProductEntityId = ProductEntity.ProductId,
                ProductEntityListPrice = ProductEntity.ListPrice,
                ProductEntityName = ProductEntity.Name,
                ProductEntityNumber = ProductEntity.ProductNumber,
                ProductEntityReorderPoint = ProductEntity.ReorderPoint,
                ProductEntitySafetyStockLevel = ProductEntity.SafetyStockLevel,
                ProductEntitySellStartDate = ProductEntity.SellStartDate,
                ProductEntityStandardCost = ProductEntity.StandardCost,
                ProductInventoryEntityBin = ProductInventoryEntity.Bin,
                ProductInventoryEntityQuantity = ProductInventoryEntity.Quantity,
                ProductInventoryEntityShelf = ProductInventoryEntity.Shelf,
                ProductListPriceHistoryEntityListPrice = ProductListPriceHistoryEntity.ListPrice,
                ProductListPriceHistoryEntityStartDate = ProductListPriceHistoryEntity.StartDate,
                ProductModelEntityProductModelID = ProductModelEntity.ProductModelID,
                ProductDescriptionEntityProductDescriptionID = ProductDescriptionEntity.ProductDescriptionID
            };
            return newProductViewModel;
        }
    }
}