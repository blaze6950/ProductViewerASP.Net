using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;
using ProductViewer.UnitTests.Fakes;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        private IUnitOfWork _unitOfWork;
        private Mock<IProductViewerContext> _mock;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IProductViewerContext>();
            var products = new FakeDbSet<Product>
            {
                new Product() {DaysToManufacture = 1, ListPrice = new decimal(11.11), Name = "Test1", ProductID = 1, ProductModelID = 1, ProductNumber = "TT-T001", ReorderPoint = 1, SafetyStockLevel = 1, SellStartDate = DateTime.Today, StandardCost = new decimal(11.11)},
                new Product() {DaysToManufacture = 2, ListPrice = new decimal(22.22), Name = "Test2", ProductID = 2, ProductModelID = 2, ProductNumber = "TT-T002", ReorderPoint = 2, SafetyStockLevel = 2, SellStartDate = DateTime.Today, StandardCost = new decimal(22.22)},
                new Product() {DaysToManufacture = 3, ListPrice = new decimal(33.33), Name = "Test3", ProductID = 3, ProductModelID = 3, ProductNumber = "TT-T003", ReorderPoint = 3, SafetyStockLevel = 3, SellStartDate = DateTime.Today, StandardCost = new decimal(33.33)}
            };
            _mock.SetupGet(c => c.Products).Returns(() => products);
            var productDescriptions = new FakeDbSet<ProductDescription>
            {
                new ProductDescription(){Description = "TestDescription1", ProductDescriptionID = 1},
                new ProductDescription(){Description = "TestDescription2", ProductDescriptionID = 2},
                new ProductDescription(){Description = "TestDescription3", ProductDescriptionID = 3}
            };
            _mock.SetupGet(c => c.ProductDescriptions).Returns(() => productDescriptions);
            var productInventories = new FakeDbSet<ProductInventory>
            {
                new ProductInventory(){Bin = 0, LocationID = 1, Product = products[0], ProductID = 1, Quantity = 1, Shelf = "A"},
                new ProductInventory(){Bin = 1, LocationID = 2, Product = products[1], ProductID = 2, Quantity = 2, Shelf = "B"},
                new ProductInventory(){Bin = 2, LocationID = 3, Product = products[2], ProductID = 3, Quantity = 3, Shelf = "C"},
            };
            _mock.SetupGet(c => c.ProductInventories).Returns(() => productInventories);
            var productListPriceHistories = new FakeDbSet<ProductListPriceHistory>
            {
                new ProductListPriceHistory(){ListPrice = (decimal) 11.11, Product = products[0], ProductID = 1, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = (decimal) 22.22, Product = products[1], ProductID = 2, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = (decimal) 33.33, Product = products[2], ProductID = 3, StartDate = DateTime.Today},
            };
            _mock.SetupGet(c => c.ProductListPriceHistories).Returns(() => productListPriceHistories);
            var productModels = new FakeDbSet<ProductModel>
            {
                new ProductModel(){Name = "TestProductModelName1", ProductModelID = 1},
                new ProductModel(){Name = "TestProductModelName2", ProductModelID = 2},
                new ProductModel(){Name = "TestProductModelName3", ProductModelID = 3}
            };
            _mock.SetupGet(c => c.ProductModels).Returns(() => productModels);
            var productModelProductDescriptionCultures = new FakeDbSet<ProductModelProductDescriptionCulture>
            {
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[0],
                    ProductDescriptionID = 1,
                    ProductModel = productModels[0],
                    ProductModelID = 1
                },
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[1],
                    ProductDescriptionID = 2,
                    ProductModel = productModels[1],
                    ProductModelID = 2
                },
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[2],
                    ProductDescriptionID = 3,
                    ProductModel = productModels[2],
                    ProductModelID = 3
                }
            };
            _mock.SetupGet(c => c.ProductModelProductDescriptionCultures).Returns(() => productModelProductDescriptionCultures);
            _mock.Setup(c => c.SaveChanges());
            _mock.Setup(c => c.Dispose());
            _unitOfWork = new UnitOfWork(_mock.Object);
        }

        [TestMethod]
        public void ProductsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductDescriptionsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductDescriptionsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductInventoriesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductInventoriesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductListPriceHistoriesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductListPriceHistoriesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductModelProductDescriptionCulturesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductModelProductDescriptionCulturesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductModelsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductModelsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void SaveChangesIsCorrect()
        {
            //Act
            _unitOfWork.Commit();
            //Asset
            _mock.Verify(c => c.SaveChanges());
        }

        [TestMethod]
        public void DisposeMethodIsCorrect()
        {
            //Act
            _unitOfWork.Dispose();
            //Asset
            _mock.Verify(c => c.Dispose());
        }
    }
}
