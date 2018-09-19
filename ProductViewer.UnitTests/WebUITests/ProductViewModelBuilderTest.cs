using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.WebUI.Models;

namespace ProductViewer.UnitTests.WebUITests
{
    [TestClass]
    public class ProductViewModelBuilderTest
    {
        private Mock<ProductViewModel> _mock;
        private ProductViewModelBuilder _builder;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<ProductViewModel>();
            _mock.SetupGet(c => c.ProductEntityId).Returns(1);
            _mock.SetupGet(c => c.ProductDescriptionEntityDescription).Returns("TestDescription");
            _mock.SetupGet(c => c.ProductDescriptionEntityProductDescriptionID).Returns(1);
            _mock.SetupGet(c => c.ProductEntityDaysToManufacture).Returns(10);
            _mock.SetupGet(c => c.ProductEntityListPrice).Returns(22.22m);
            _mock.SetupGet(c => c.ProductEntityName).Returns("Test");
            _mock.SetupGet(c => c.ProductEntityNumber).Returns("TestNumber");
            _mock.SetupGet(c => c.ProductEntityReorderPoint).Returns(3);
            _mock.SetupGet(c => c.ProductEntitySafetyStockLevel).Returns(5);
            _mock.SetupGet(c => c.ProductEntitySellStartDate).Returns(DateTime.Today);
            _mock.SetupGet(c => c.ProductEntityStandardCost).Returns(55.55m);
            _mock.SetupGet(c => c.ProductInventoryEntityBin).Returns(2);
            _mock.SetupGet(c => c.ProductInventoryEntityQuantity).Returns(23);
            _mock.SetupGet(c => c.ProductInventoryEntityShelf).Returns("Z");
            _mock.SetupGet(c => c.ProductModelEntityProductModelID).Returns(1);
            _mock.SetupGet(c => c.ProductListPriceHistoryEntityListPrice).Returns(66.66m);
            _mock.SetupGet(c => c.ProductListPriceHistoryEntityStartDate).Returns(DateTime.Today);
            _mock.Setup(c => c.GetBuilder()).Returns(_builder);
            _builder = new ProductViewModelBuilder(_mock.Object);
        }

        [TestMethod]
        public void ProductEntityPropertyIsCorrectReturn()
        {
            //Act
            var productEntity = _builder.ProductEntity;
            //Assert
            Assert.IsNotNull(productEntity);
            Assert.AreEqual(productEntity.ProductId, _mock.Object.ProductEntityId);
            Assert.AreEqual(productEntity.Name, _mock.Object.ProductEntityName);
            Assert.AreEqual(productEntity.ProductNumber, _mock.Object.ProductEntityNumber);
            Assert.AreEqual(productEntity.SafetyStockLevel, _mock.Object.ProductEntitySafetyStockLevel);
            Assert.AreEqual(productEntity.ReorderPoint, _mock.Object.ProductEntityReorderPoint);
            Assert.AreEqual(productEntity.StandardCost, _mock.Object.ProductEntityStandardCost);
            Assert.AreEqual(productEntity.ListPrice, _mock.Object.ProductEntityListPrice);
            Assert.AreEqual(productEntity.DaysToManufacture, _mock.Object.ProductEntityDaysToManufacture);
            Assert.AreEqual(productEntity.SellStartDate, _mock.Object.ProductEntitySellStartDate);
            Assert.AreEqual(productEntity.ProductModelID, _mock.Object.ProductModelEntityProductModelID);
        }

        [TestMethod]
        public void ProductDescriptionEntityPropertyIsCorrectReturn()
        {
            //Act
            var productDescriptionEntity = _builder.ProductDescriptionEntity;
            //Assert
            Assert.IsNotNull(productDescriptionEntity);
            Assert.AreEqual(productDescriptionEntity.ProductDescriptionID,
                _mock.Object.ProductDescriptionEntityProductDescriptionID);
            Assert.AreEqual(productDescriptionEntity.Description,
                _mock.Object.ProductDescriptionEntityDescription);
        }

        [TestMethod]
        public void ProductInventoryEntityPropertyIsCorrectReturn()
        {
            //Act
            var productInventoryEntity = _builder.ProductInventoryEntity;
            //Assert
            Assert.IsNotNull(productInventoryEntity);
            Assert.AreEqual(productInventoryEntity.ProductID, _mock.Object.ProductEntityId);
            Assert.AreEqual(productInventoryEntity.LocationID, 1);
            Assert.AreEqual(productInventoryEntity.Shelf, _mock.Object.ProductInventoryEntityShelf);
            Assert.AreEqual(productInventoryEntity.Bin, _mock.Object.ProductInventoryEntityBin);
            Assert.AreEqual(productInventoryEntity.Quantity, _mock.Object.ProductInventoryEntityQuantity);
        }

        [TestMethod]
        public void ProductListPriceHistoryEntityPropertyIsCorrectReturn()
        {
            //Act
            var productListPriceHistoryEntity = _builder.ProductListPriceHistoryEntity;
            //Assert
            Assert.IsNotNull(productListPriceHistoryEntity);
            Assert.AreEqual(productListPriceHistoryEntity.ProductID, _mock.Object.ProductEntityId);
            Assert.AreEqual(productListPriceHistoryEntity.StartDate, _mock.Object.ProductListPriceHistoryEntityStartDate);
            Assert.AreEqual(productListPriceHistoryEntity.ListPrice, _mock.Object.ProductListPriceHistoryEntityListPrice);
        }

        [TestMethod]
        public void ProductModelProductDescriptionCultureEntityPropertyIsCorrectReturn()
        {
            //Act
            var productModelProductDescriptionCultureEntity = _builder.ProductModelProductDescriptionCultureEntity;
            //Assert
            Assert.IsNotNull(productModelProductDescriptionCultureEntity);
            Assert.AreEqual(productModelProductDescriptionCultureEntity.ProductModelID, _mock.Object.ProductModelEntityProductModelID);
            Assert.AreEqual(productModelProductDescriptionCultureEntity.ProductDescriptionID, _mock.Object.ProductDescriptionEntityProductDescriptionID);
            Assert.AreEqual(productModelProductDescriptionCultureEntity.CultureID, "en");
        }

        [TestMethod]
        public void ProductModelEntityPropertyIsCorrectReturn()
        {
            //Act
            var productModelEntity = _builder.ProductModelEntity;
            //Assert
            Assert.IsNotNull(productModelEntity);
            Assert.AreEqual(productModelEntity.ProductModelID, _mock.Object.ProductModelEntityProductModelID);
            Assert.AreEqual(productModelEntity.Name, _mock.Object.ProductEntityName);
        }

        [TestMethod]
        public void ProductViewModelPropertyIsCorrectReturn()
        {
            //Act
            var productViewModel = _builder.ProductViewModel;
            //Assert
            Assert.IsNotNull(productViewModel);
            Assert.AreEqual(productViewModel, _mock.Object);
            Assert.AreNotSame(productViewModel, _mock.Object);
        }
    }
}
