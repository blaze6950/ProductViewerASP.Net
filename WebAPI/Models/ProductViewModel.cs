using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Models
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
        public virtual int ProductEntityId
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual int ProductModelEntityProductModelID
        {
            get;
            set;
        }

        [HiddenInput(DisplayValue = false)]
        public virtual int ProductDescriptionEntityProductDescriptionID
        {
            get;
            set;
        }

        #region FiledsForAddOrEditingView
        [Required(ErrorMessage = "Please enter name of product")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters in length!")]
        public virtual string ProductEntityName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter unique number of product")]
        [RegularExpression("[A-Z]{2}-([A-Z]\\d{3})|[A-Z]{2}-(\\d{4})", ErrorMessage = "Please enter a valid unique number of product")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "The name must be between 7 and 25 characters in length!")]
        public virtual string ProductEntityNumber
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter safety stock level")]
        [Range(1, 32767, ErrorMessage = "The safety stock level must be positive and between 1 and 32767!")]
        public virtual Int16 ProductEntitySafetyStockLevel
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter reorder point")]
        [Range(1, 32767, ErrorMessage = "The reorder point must be positive and between 1 and 32767!")]
        public virtual Int16 ProductEntityReorderPoint
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter standard cost")]
        [Range(1, 922337203685477, ErrorMessage = "The standart cost must be positive!")]
        [DataType(DataType.Currency)]
        public virtual decimal ProductEntityStandardCost
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter list price")]
        [Range(0, 922337203685477, ErrorMessage = "The list price must be positive!")]
        [DataType(DataType.Currency)]
        public virtual decimal ProductEntityListPrice
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter days to manufacture")]
        [Range(0, 2147483647, ErrorMessage = "The list price must be positive and between 0 and 2147483647!")]
        public virtual int ProductEntityDaysToManufacture
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter sell start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime ProductEntitySellStartDate
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter description")]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "The description must be between 1 and 400 characters in length!")]
        [DataType(DataType.MultilineText)]
        public virtual string ProductDescriptionEntityDescription
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter shelf")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The shelf must be between 1 and 400 characters in length!")]
        public virtual string ProductInventoryEntityShelf
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter bin")]
        [Range(0, 100, ErrorMessage = "The bin must be between 0 and 100!")]
        public virtual byte ProductInventoryEntityBin
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 32767, ErrorMessage = "The quantity must be between 0 and 32767!")]
        public virtual Int16 ProductInventoryEntityQuantity
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter start date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime ProductListPriceHistoryEntityStartDate
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter list price history")]
        [Range(0, 922337203685477, ErrorMessage = "The list price history must be positive!")]
        [DataType(DataType.Currency)]
        public virtual decimal ProductListPriceHistoryEntityListPrice
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

        public override bool Equals(object obj)
        {
            var model = obj as ProductViewModel;
            return model != null &&
                   ProductEntityId == model.ProductEntityId &&
                   ProductModelEntityProductModelID == model.ProductModelEntityProductModelID &&
                   ProductDescriptionEntityProductDescriptionID == model.ProductDescriptionEntityProductDescriptionID &&
                   ProductEntityName == model.ProductEntityName &&
                   ProductEntityNumber == model.ProductEntityNumber &&
                   ProductEntitySafetyStockLevel == model.ProductEntitySafetyStockLevel &&
                   ProductEntityReorderPoint == model.ProductEntityReorderPoint &&
                   ProductEntityStandardCost == model.ProductEntityStandardCost &&
                   ProductEntityListPrice == model.ProductEntityListPrice &&
                   ProductEntityDaysToManufacture == model.ProductEntityDaysToManufacture &&
                   ProductEntitySellStartDate == model.ProductEntitySellStartDate &&
                   ProductDescriptionEntityDescription == model.ProductDescriptionEntityDescription &&
                   ProductInventoryEntityShelf == model.ProductInventoryEntityShelf &&
                   ProductInventoryEntityBin == model.ProductInventoryEntityBin &&
                   ProductInventoryEntityQuantity == model.ProductInventoryEntityQuantity &&
                   ProductListPriceHistoryEntityStartDate == model.ProductListPriceHistoryEntityStartDate &&
                   ProductListPriceHistoryEntityListPrice == model.ProductListPriceHistoryEntityListPrice &&
                   PriceForAll == model.PriceForAll;
        }

        public virtual ProductViewModelBuilder GetBuilder()
        {
            if (_builder == null)
            {
                _builder = new ProductViewModelBuilder(this);
            }
            return _builder;
        }

        public override int GetHashCode()
        {
            var hashCode = -791488338;
            hashCode = hashCode * -1521134295 + ProductEntityId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductModelEntityProductModelID.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductDescriptionEntityProductDescriptionID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductEntityName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductEntityNumber);
            hashCode = hashCode * -1521134295 + ProductEntitySafetyStockLevel.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductEntityReorderPoint.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductEntityStandardCost.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductEntityListPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductEntityDaysToManufacture.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductEntitySellStartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductDescriptionEntityDescription);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductInventoryEntityShelf);
            hashCode = hashCode * -1521134295 + ProductInventoryEntityBin.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductInventoryEntityQuantity.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductListPriceHistoryEntityStartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductListPriceHistoryEntityListPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + PriceForAll.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            // продукты, затем их описание, стоимость и количество на складе
            return $"Product: {ProductEntityName}, Description: {ProductDescriptionEntityDescription}, Price: {ProductListPriceHistoryEntityListPrice}, Count: {ProductInventoryEntityQuantity}";
        }
    }
}