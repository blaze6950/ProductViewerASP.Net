using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ProductViewer.Domain.Entities;

namespace ProductViewer.WebUI.Models
{
    public class ProductViewModel
    {
        private ProductViewModelBuilder _builder;

        public ProductViewModel()
        {
            _builder = null;
        }

        public ProductViewModel(ProductViewModelBuilder builder)
        {
            _builder = builder;
        }
        
        [HiddenInput(DisplayValue = false)]
        public int ProductEntityId
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public int ProductModelEntityProductModelID
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public int ProductDescriptionEntityProductDescriptionID
        {
            get;
            set;
        }

        #region FiledsForAddOrEditingView
        [Required(ErrorMessage = "Please enter name of product")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters in length!")]
        public string ProductEntityName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter unique number of product")]
        [RegularExpression("[A-Z]{2}-([A-Z]\\d{3})|[A-Z]{2}-(\\d{4})", ErrorMessage = "Please enter a valid unique number of product")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "The name must be between 7 and 25 characters in length!")]
        public string ProductEntityNumber
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter safety stock level")]
        [Range(1, 32767, ErrorMessage = "The safety stock level must be positive and between 1 and 32767!")]
        public Int16 ProductEntitySafetyStockLevel
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter reorder point")]
        [Range(1, 32767, ErrorMessage = "The reorder point must be positive and between 1 and 32767!")]
        public Int16 ProductEntityReorderPoint
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter standard cost")]
        [Range(1, 922337203685477, ErrorMessage = "The standart cost must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductEntityStandardCost
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter list price")]
        [Range(0, 922337203685477, ErrorMessage = "The list price must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductEntityListPrice
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter days to manufacture")]
        [Range(0, 2147483647, ErrorMessage = "The list price must be positive and between 0 and 2147483647!")]
        public int ProductEntityDaysToManufacture
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter sell start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductEntitySellStartDate
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter description")]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "The description must be between 1 and 400 characters in length!")]
        [DataType(DataType.MultilineText)]
        public string ProductDescriptionEntityDescription
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter shelf")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The shelf must be between 1 and 400 characters in length!")]
        public string ProductInventoryEntityShelf
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter bin")]
        [Range(0, 100, ErrorMessage = "The bin must be between 0 and 100!")]
        public byte ProductInventoryEntityBin
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 32767, ErrorMessage = "The quantity must be between 0 and 32767!")]
        public Int16 ProductInventoryEntityQuantity
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProductListPriceHistoryEntityStartDate
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter list price history")]
        [Range(0, 922337203685477, ErrorMessage = "The list price history must be positive!")]
        [DataType(DataType.Currency)]
        public decimal ProductListPriceHistoryEntityListPrice
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// Computed property. Only getter without setter returned Quantity * Price 
        /// </summary>
        public decimal PriceForAll
        {
            get { return ProductInventoryEntityQuantity * ProductListPriceHistoryEntityListPrice; }
        }

        public ProductViewModelBuilder GetBuilder()
        {
            if (_builder == null)
            {
                _builder = new ProductViewModelBuilder(this);
            }
            return _builder;
        }

        public override string ToString()
        {
            // продукты, затем их описание, стоимость и количество на складе
            return $"Product: {ProductEntityName}, Description: {ProductDescriptionEntityDescription}, Price: {ProductListPriceHistoryEntityListPrice}, Count: {ProductInventoryEntityQuantity}";
        }
    }
}