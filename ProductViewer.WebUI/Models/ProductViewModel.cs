using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProductViewer.Domain.Entities;

namespace ProductViewer.WebUI.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductEntity = new Product();
            ProductDescriptionEntity = new ProductDescription();
            ProductInventoryEntity = new ProductInventory();
            ProductListPriceHistoryEntity = new ProductListPriceHistory();
            ProductModelProductDescriptionCultureEntity = new ProductModelProductDescriptionCulture();
            ProductModelEntity = new ProductModel();

            AddReferencesBetweenFiledsOfEntities();
        }

        public ProductViewModel(Product productEntity, ProductDescription productDescriptionEntity, ProductInventory productInventoryEntity, ProductListPriceHistory productListPriceHistoryEntity, ProductModelProductDescriptionCulture productModelProductDescriptionCultureEntity, ProductModel productModelEntity)
        {
            ProductEntity = productEntity;
            ProductDescriptionEntity = productDescriptionEntity;
            ProductInventoryEntity = productInventoryEntity;
            ProductListPriceHistoryEntity = productListPriceHistoryEntity;
            ProductModelProductDescriptionCultureEntity = productModelProductDescriptionCultureEntity;
            ProductModelEntity = productModelEntity;

            AddReferencesBetweenFiledsOfEntities();
        }

        private void AddReferencesBetweenFiledsOfEntities()
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
            ProductDescriptionEntity.ProductDescriptionIDUpdated += newId =>
            {
                ProductModelProductDescriptionCultureEntity.ProductDescriptionID = newId;
            };

            ProductModelEntity.ProductModelID = ProductModelEntity.ProductModelID;
            ProductEntity.ProductId = ProductEntity.ProductId;
            ProductDescriptionEntity.ProductDescriptionID = ProductDescriptionEntity.ProductDescriptionID;
        }

        #region HiddenFields
        [HiddenInput(DisplayValue = false)]
        public Product ProductEntity { get; private set; }
        [HiddenInput(DisplayValue = false)]
        public ProductDescription ProductDescriptionEntity { get; private set; }
        [HiddenInput(DisplayValue = false)]
        public ProductInventory ProductInventoryEntity { get; private set; }
        [HiddenInput(DisplayValue = false)]
        public ProductListPriceHistory ProductListPriceHistoryEntity { get; private set; }
        [HiddenInput(DisplayValue = false)]
        public ProductModelProductDescriptionCulture ProductModelProductDescriptionCultureEntity { get; private set; }
        [HiddenInput(DisplayValue = false)]
        public ProductModel ProductModelEntity { get; private set; }
        #endregion

        [HiddenInput(DisplayValue = false)]
        public int ProductEntityId
        {
            get => ProductEntity.ProductId;
            set => ProductEntity.ProductId = value;
        }

        #region FiledsForAddOrEditingView
        [Required(ErrorMessage = "Please enter name of product")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters in length!")]
        public string ProductEntityName
        {
            get => ProductEntity.Name;
            set
            {
                ProductEntity.Name = value?.TrimEnd(' ').TrimStart(' ');
                ProductModelEntity.Name = ProductEntity.Name;
            }
        }

        [Required(ErrorMessage = "Please enter unique number of product")]
        [RegularExpression("[A-Z]{2}-([A-Z]\\d{3})|[A-Z]{2}-(\\d{4})", ErrorMessage = "Please enter a valid unique number of product")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "The name must be between 7 and 25 characters in length!")]
        public string ProductEntityNumber
        {
            get => ProductEntity.ProductNumber;
            set => ProductEntity.ProductNumber = value?.TrimEnd(' ').TrimStart(' ');
        }

        [Required(ErrorMessage = "Please enter safety stock level")]
        [Range(1, 32767, ErrorMessage = "The safety stock level must be positive and between 1 and 32767!")]
        public Int16 ProductEntitySafetyStockLevel
        {
            get => ProductEntity.SafetyStockLevel;
            set => ProductEntity.SafetyStockLevel = value;
        }

        [Required(ErrorMessage = "Please enter reorder point")]
        [Range(1, 32767, ErrorMessage = "The reorder point must be positive and between 1 and 32767!")]
        public Int16 ProductEntityReorderPoint
        {
            get => ProductEntity.ReorderPoint;
            set => ProductEntity.ReorderPoint = value;
        }

        [Required(ErrorMessage = "Please enter standard cost")]
        [Range(1, 922337203685477, ErrorMessage = "The standart cost must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductEntityStandardCost
        {
            get => ProductEntity.StandardCost;
            set => ProductEntity.StandardCost = value;
        }

        [Required(ErrorMessage = "Please enter list price")]
        [Range(0, 922337203685477, ErrorMessage = "The list price must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductEntityListPrice
        {
            get => ProductEntity.ListPrice;
            set => ProductEntity.ListPrice = value;
        }

        [Required(ErrorMessage = "Please enter days to manufacture")]
        [Range(0, 2147483647, ErrorMessage = "The list price must be positive and between 0 and 2147483647!")]
        public int ProductEntityDaysToManufacture
        {
            get => ProductEntity.DaysToManufacture;
            set => ProductEntity.DaysToManufacture = value;
        }

        [Required(ErrorMessage = "Please enter sell start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductEntitySellStartDate
        {
            get => ProductEntity.SellStartDate.Date;
            set => ProductEntity.SellStartDate = value;
        }

        [Required(ErrorMessage = "Please enter description")]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "The description must be between 1 and 400 characters in length!")]
        [DataType(DataType.MultilineText)]
        public string ProductDescriptionEntityDescription
        {
            get => ProductDescriptionEntity.Description;
            set => ProductDescriptionEntity.Description = value?.TrimEnd(' ').TrimStart(' ');
        }

        [Required(ErrorMessage = "Please enter shelf")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The shelf must be between 1 and 400 characters in length!")]
        public string ProductInventoryEntityShelf
        {
            get => ProductInventoryEntity.Shelf;
            set => ProductInventoryEntity.Shelf = value;
        }

        [Required(ErrorMessage = "Please enter bin")]
        [Range(0, 100, ErrorMessage = "The bin must be between 0 and 100!")]
        public byte ProductInventoryEntityBin
        {
            get => ProductInventoryEntity.Bin;
            set => ProductInventoryEntity.Bin = value;
        }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 32767, ErrorMessage = "The quantity must be between 0 and 32767!")]
        public Int16 ProductInventoryEntityQuantity
        {
            get => ProductInventoryEntity.Quantity;
            set => ProductInventoryEntity.Quantity = value;
        }

        [Required(ErrorMessage = "Please enter start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductListPriceHistoryEntityStartDate
        {
            get => ProductListPriceHistoryEntity.StartDate.Date;
            set => ProductListPriceHistoryEntity.StartDate = value;
        }

        [Required(ErrorMessage = "Please enter list price history")]
        [Range(0, 922337203685477, ErrorMessage = "The list price history must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductListPriceHistoryEntityListPrice
        {
            get => ProductListPriceHistoryEntity.ListPrice;
            set => ProductListPriceHistoryEntity.ListPrice = value;
        }
        #endregion

        public override string ToString()
        {
            // продукты, затем их описание, стоимость и количество на складе
            return $"Product: {ProductEntity.Name}, Description: {ProductDescriptionEntity.Description}, Price: {ProductListPriceHistoryEntity.ListPrice}, Count: {ProductInventoryEntity.Quantity}";
        }
    }
}