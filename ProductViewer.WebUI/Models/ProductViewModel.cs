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
        }

        public ProductViewModel(Product productEntity, ProductDescription productDescriptionEntity, ProductInventory productInventoryEntity, ProductListPriceHistory productListPriceHistoryEntity, ProductModelProductDescriptionCulture productModelProductDescriptionCultureEntity, ProductModel productModelEntity)
        {
            ProductEntity = productEntity;
            ProductDescriptionEntity = productDescriptionEntity;
            ProductInventoryEntity = productInventoryEntity;
            ProductListPriceHistoryEntity = productListPriceHistoryEntity;
            ProductModelProductDescriptionCultureEntity = productModelProductDescriptionCultureEntity;
            ProductModelEntity = productModelEntity;
        }

        //public ProductViewModel(Product productEntity, ProductDescription productDescriptionEntity, ProductInventory productInventoryEntity, ProductListPriceHistory productListPriceHistoryEntity)
        //{
        //    ProductEntity = productEntity;
        //    ProductDescriptionEntity = productDescriptionEntity;
        //    ProductInventoryEntity = productInventoryEntity;
        //    ProductListPriceHistoryEntity = productListPriceHistoryEntity;
        //}

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
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter name of product")]
        public string ProductEntityName
        {
            get => ProductEntity.Name;
            set => ProductEntity.Name = value;
        }

        [Display(Name = "Number")]
        [Required(ErrorMessage = "Please enter unique number of product")]
        [RegularExpression("[0-9]", ErrorMessage = "Please enter a valid unique number of product")]
        public string ProductEntityNumber
        {
            get => ProductEntity.ProductNumber;
            set => ProductEntity.ProductNumber = value;
        }

        [Display(Name = "Safety stock level")]
        [Required(ErrorMessage = "Please enter safety stock level")]
        public Int16 ProductEntitySafetyStockLevel
        {
            get => ProductEntity.SafetyStockLevel;
            set => ProductEntity.SafetyStockLevel = value;
        }

        [Display(Name = "Reorder point")]
        [Required(ErrorMessage = "Please enter reorder point")]
        public Int16 ProductEntityReorderPoint
        {
            get => ProductEntity.ReorderPoint;
            set => ProductEntity.ReorderPoint = value;
        }

        [Display(Name = "Standard cost")]
        [Required(ErrorMessage = "Please enter standard cost")]
        public decimal ProductEntityStandardCost
        {
            get => ProductEntity.StandardCost;
            set => ProductEntity.StandardCost = value;
        }

        [Display(Name = "List price")]
        [Required(ErrorMessage = "Please enter list price")]
        public decimal ProductEntityListPrice
        {
            get => ProductEntity.ListPrice;
            set => ProductEntity.ListPrice = value;
        }

        [Display(Name = "Days to manufacture")]
        [Required(ErrorMessage = "Please enter days to manufacture")]
        public int ProductEntityDaysToManufacture
        {
            get => ProductEntity.DaysToManufacture;
            set => ProductEntity.DaysToManufacture = value;
        }

        [Display(Name = "Sell start date")]
        [Required(ErrorMessage = "Please enter sell start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductEntitySellStartDate
        {
            get => ProductEntity.SellStartDate.Date;
            set => ProductEntity.SellStartDate = value;
        }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter description")]
        public string ProductDescriptionEntityDescription
        {
            get => ProductDescriptionEntity.Description;
            set => ProductDescriptionEntity.Description = value;
        }

        [Display(Name = "Shelf")]
        [Required(ErrorMessage = "Please enter shelf")]
        public string ProductInventoryEntityShelf
        {
            get => ProductInventoryEntity.Shelf;
            set => ProductInventoryEntity.Shelf = value;
        }

        [Display(Name = "Bin")]
        [Required(ErrorMessage = "Please enter bin")]
        public byte ProductInventoryEntityBin
        {
            get => ProductInventoryEntity.Bin;
            set => ProductInventoryEntity.Bin = value;
        }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please enter quantity")]
        public Int16 ProductInventoryEntityQuantity
        {
            get => ProductInventoryEntity.Quantity;
            set => ProductInventoryEntity.Quantity = value;
        }

        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Please enter start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductListPriceHistoryEntityStartDate
        {
            get => ProductListPriceHistoryEntity.StartDate.Date;
            set => ProductListPriceHistoryEntity.StartDate = value;
        }

        [Display(Name = "List price history")]
        [Required(ErrorMessage = "Please enter list price history")]
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