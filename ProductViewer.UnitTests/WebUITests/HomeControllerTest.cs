using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;
using ProductViewer.WebUI.Controllers;
using ProductViewer.WebUI.Models;

namespace ProductViewer.UnitTests.WebUITests
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _homeController;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IProductsRepository> _mockProductRepository;
        private Mock<IProductDescriptionsRepository> _mockProductDescriptionRepository;
        private Mock<IProductListPriceHistoriesRepository> _mockProductListPriceHistoryRepository;
        private Mock<IProductInventoriesRepository> _mockProductInventoryRepository;
        private Mock<IProductModelProductDescriptionCulturesRepository> _mockProductModelProductDescriptionCultureRepository;
        private Mock<IProductModelsRepository> _mockProductModelRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            #region _mockProductRepository
            List<Product> productList = new List<Product>();
            productList.AddRange(new Product[]
            {
                new Product()
                {
                    DaysToManufacture = 10,
                    ProductId = 1,
                    ProductModelID = 1,
                    Name = "Test1",
                    SafetyStockLevel = 20,
                    ListPrice = 11.11m,
                    ProductNumber = "TestNumber1",
                    ReorderPoint = 30,
                    SellStartDate = DateTime.Today,
                    StandardCost = 22.22m
                },
                new Product()
                {
                    DaysToManufacture = 11,
                    ProductId = 2,
                    ProductModelID = 2,
                    Name = "Test2",
                    SafetyStockLevel = 21,
                    ListPrice = 22.22m,
                    ProductNumber = "TestNumber2",
                    ReorderPoint = 31,
                    SellStartDate = DateTime.Today,
                    StandardCost = 33.33m
                },
                new Product()
                {
                    DaysToManufacture = 12,
                    ProductId = 3,
                    ProductModelID = 3,
                    Name = "Test3",
                    SafetyStockLevel = 22,
                    ListPrice = 33.33m,
                    ProductNumber = "TestNumber3",
                    ReorderPoint = 32,
                    SellStartDate = DateTime.Today,
                    StandardCost = 44.44m
                },
                new Product()
                {
                    DaysToManufacture = 13,
                    ProductId = 4,
                    ProductModelID = 4,
                    Name = "Test4",
                    SafetyStockLevel = 23,
                    ListPrice = 44.44m,
                    ProductNumber = "TestNumber4",
                    ReorderPoint = 33,
                    SellStartDate = DateTime.Today,
                    StandardCost = 55.55m
                }
            });
            _mockProductRepository = new Mock<IProductsRepository>();
            _mockProductRepository.Setup(c => c.Create(It.IsNotNull<Product>())).Callback((Product newProduct) => productList.Add(newProduct));
            _mockProductRepository.Setup(c => c.Delete(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Callback((int id) => productList.Remove(_mockProductRepository.Object.GetProduct(id)));
            _mockProductRepository.Setup(c => c.GetProduct(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Returns((int id) =>
            {
                return productList.Find(c => c.ProductId == id);
            });
            _mockProductRepository.Setup(c => c.GetProductList()).Returns(productList);
            _mockProductRepository.Setup(c => c.Update(It.IsNotNull<Product>())).Callback((Product newProduct) => { _mockProductRepository.Object.Delete(newProduct.ProductId); _mockProductRepository.Object.Create(newProduct); });
            #endregion

            #region _mockProductDescriptionRepository
            List<ProductDescription> productDescriptionList = new List<ProductDescription>();
            productDescriptionList.AddRange(new ProductDescription[]
            {
                new ProductDescription(){Description = "Description1", ProductDescriptionID = 1},
                new ProductDescription(){Description = "Description2", ProductDescriptionID = 2},
                new ProductDescription(){Description = "Description3", ProductDescriptionID = 3},
                new ProductDescription(){Description = "Description4", ProductDescriptionID = 4}
            });
            _mockProductDescriptionRepository = new Mock<IProductDescriptionsRepository>();
            _mockProductDescriptionRepository.Setup(c => c.Create(It.IsNotNull<ProductDescription>())).Callback((ProductDescription newProductDescription) => productDescriptionList.Add(newProductDescription));
            _mockProductDescriptionRepository.Setup(c => c.Delete(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Callback((int id) => productDescriptionList.Remove(_mockProductDescriptionRepository.Object.GetProductDescription(id)));
            _mockProductDescriptionRepository.Setup(c => c.GetProductDescription(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Returns((int id) =>
            {
                return productDescriptionList.Find(c => c.ProductDescriptionID == id);
            });
            _mockProductDescriptionRepository.Setup(c => c.GetProductDescriptionList()).Returns(productDescriptionList);
            _mockProductDescriptionRepository.Setup(c => c.Update(It.IsNotNull<ProductDescription>())).Callback((ProductDescription newProductDescription) => { _mockProductDescriptionRepository.Object.Delete(newProductDescription.ProductDescriptionID); _mockProductDescriptionRepository.Object.Create(newProductDescription); });
            #endregion

            #region _mockProductInventoryRepository
            List<ProductInventory> productInventoryList = new List<ProductInventory>();
            productInventoryList.AddRange(new ProductInventory[]
            {
                new ProductInventory()
                {
                    ProductID = 1,
                    Bin = 1,
                    LocationID = 1,
                    Quantity = 10,
                    Shelf = "A"
                },
                new ProductInventory()
                {
                    ProductID = 2,
                    Bin = 2,
                    LocationID = 1,
                    Quantity = 11,
                    Shelf = "B"
                },
                new ProductInventory()
                {
                    ProductID = 3,
                    Bin = 3,
                    LocationID = 1,
                    Quantity = 12,
                    Shelf = "C"
                },
                new ProductInventory()
                {
                    ProductID = 4,
                    Bin = 4,
                    LocationID = 1,
                    Quantity = 13,
                    Shelf = "D"
                }
            });
            _mockProductInventoryRepository = new Mock<IProductInventoriesRepository>();
            _mockProductInventoryRepository.Setup(c => c.Create(It.IsNotNull<ProductInventory>())).Callback((ProductInventory newProduct) => productInventoryList.Add(newProduct));
            _mockProductInventoryRepository.Setup(c => c.Delete(It.IsInRange((short)0, short.MaxValue, Range.Exclusive), It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Callback((short idLocation, int idProduct) => productInventoryList.Remove(_mockProductInventoryRepository.Object.GetProductInventory(locationId: idLocation, productId: idProduct)));
            _mockProductInventoryRepository.Setup(c => c.GetProductInventory(It.IsInRange((short)0, short.MaxValue, Range.Exclusive), It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Returns((short idLocation, int idProduct) =>
            {
                return productInventoryList.Find(c => c.LocationID == idLocation && c.ProductID == idProduct);
            });
            _mockProductInventoryRepository.Setup(c => c.GetProductInventoryList()).Returns(productInventoryList);
            _mockProductInventoryRepository.Setup(c => c.Update(It.IsNotNull<ProductInventory>())).Callback((ProductInventory newProduct) => { _mockProductInventoryRepository.Object.Delete(newProduct.LocationID, newProduct.ProductID); _mockProductInventoryRepository.Object.Create(newProduct); });
            #endregion

            #region _mockProductListPriceHistoryRepository
            List<ProductListPriceHistory> productListPriceHistoryList = new List<ProductListPriceHistory>();
            productListPriceHistoryList.AddRange(new ProductListPriceHistory[]
            {
                new ProductListPriceHistory(){ListPrice = 11.11m, ProductID = 1, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = 22.22m, ProductID = 2, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = 33.33m, ProductID = 3, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = 44.44m, ProductID = 4, StartDate = DateTime.Today}
            });
            _mockProductListPriceHistoryRepository = new Mock<IProductListPriceHistoriesRepository>();
            _mockProductListPriceHistoryRepository.Setup(c => c.Create(It.IsNotNull<ProductListPriceHistory>())).Callback((ProductListPriceHistory newProductListPriceHistory) => productListPriceHistoryList.Add(newProductListPriceHistory));
            _mockProductListPriceHistoryRepository.Setup(c => c.Delete(It.IsInRange(0, Int32.MaxValue, Range.Exclusive), It.IsNotNull<DateTime>())).Callback((int idProduct, DateTime startDate) => productListPriceHistoryList.Remove(_mockProductListPriceHistoryRepository.Object.GetProductListPriceHistory(idProduct, startDate)));
            _mockProductListPriceHistoryRepository.Setup(c => c.GetProductListPriceHistory(It.IsInRange(0, Int32.MaxValue, Range.Exclusive), It.IsNotNull<DateTime>())).Returns((int idProduct, DateTime startDate) =>
            {
                return productListPriceHistoryList.Find(c => c.ProductID == idProduct && c.StartDate == startDate);
            });
            _mockProductListPriceHistoryRepository.Setup(c => c.GetProductListPriceHistoryList()).Returns(productListPriceHistoryList);
            _mockProductListPriceHistoryRepository.Setup(c => c.Update(It.IsNotNull<ProductListPriceHistory>())).Callback((ProductListPriceHistory newProductListPriceHistory) => { _mockProductListPriceHistoryRepository.Object.Delete(newProductListPriceHistory.ProductID, newProductListPriceHistory.StartDate); _mockProductListPriceHistoryRepository.Object.Create(newProductListPriceHistory); });
            #endregion

            #region _mockProductModelProductDescriptionCultureRepository
            List<ProductModelProductDescriptionCulture> productModelProductDescriptionCultureList = new List<ProductModelProductDescriptionCulture>();
            productModelProductDescriptionCultureList.AddRange(new ProductModelProductDescriptionCulture[]
            {
                new ProductModelProductDescriptionCulture(){CultureID = "en", ProductDescriptionID = 1, ProductModelID = 1},
                new ProductModelProductDescriptionCulture(){CultureID = "en", ProductDescriptionID = 2, ProductModelID = 2},
                new ProductModelProductDescriptionCulture(){CultureID = "en", ProductDescriptionID = 3, ProductModelID = 3},
                new ProductModelProductDescriptionCulture(){CultureID = "en", ProductDescriptionID = 4, ProductModelID = 4}
            });
            _mockProductModelProductDescriptionCultureRepository = new Mock<IProductModelProductDescriptionCulturesRepository>();
            _mockProductModelProductDescriptionCultureRepository.Setup(c => c.Create(It.IsNotNull<ProductModelProductDescriptionCulture>())).Callback((ProductModelProductDescriptionCulture newProductModelProductDescriptionCulture) => productModelProductDescriptionCultureList.Add(newProductModelProductDescriptionCulture));
            _mockProductModelProductDescriptionCultureRepository.Setup(c => c.Delete(It.IsInRange(0, Int32.MaxValue, Range.Exclusive), It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Callback((int idProductModel, int idProductDescription) => productModelProductDescriptionCultureList.Remove(_mockProductModelProductDescriptionCultureRepository.Object.GetProductModelProductDescriptionCulture(idProductModel, idProductDescription)));
            _mockProductModelProductDescriptionCultureRepository.Setup(c => c.GetProductModelProductDescriptionCulture(It.IsInRange(0, Int32.MaxValue, Range.Exclusive), It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Returns((int idProductModel, int idProductDescription) =>
            {
                return productModelProductDescriptionCultureList.Find(c => c.ProductDescriptionID == idProductDescription && c.ProductModelID == idProductModel);
            });
            _mockProductModelProductDescriptionCultureRepository.Setup(c => c.GetProductModelProductDescriptionCultureList()).Returns(productModelProductDescriptionCultureList);
            _mockProductModelProductDescriptionCultureRepository.Setup(c => c.Update(It.IsNotNull<ProductModelProductDescriptionCulture>())).Callback((ProductModelProductDescriptionCulture newProductModelProductDescriptionCulture) => { _mockProductModelProductDescriptionCultureRepository.Object.Delete(newProductModelProductDescriptionCulture.ProductModelID, newProductModelProductDescriptionCulture.ProductDescriptionID); _mockProductModelProductDescriptionCultureRepository.Object.Create(newProductModelProductDescriptionCulture); });
            #endregion

            #region _mockProductModelRepository
            List<ProductModel> productModelList = new List<ProductModel>();
            productModelList.AddRange(new ProductModel[]
            {
                new ProductModel(){Name = "Test1", ProductModelID = 1},
                new ProductModel(){Name = "Test2", ProductModelID = 2},
                new ProductModel(){Name = "Test3", ProductModelID = 3},
                new ProductModel(){Name = "Test4", ProductModelID = 4}
            });
            _mockProductModelRepository = new Mock<IProductModelsRepository>();
            _mockProductModelRepository.Setup(c => c.Create(It.IsNotNull<ProductModel>())).Callback((ProductModel newProductModel) => productModelList.Add(newProductModel));
            _mockProductModelRepository.Setup(c => c.Delete(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Callback((int id) => productModelList.Remove(_mockProductModelRepository.Object.GetProductModel(id)));
            _mockProductModelRepository.Setup(c => c.GetProductModel(It.IsInRange(0, Int32.MaxValue, Range.Exclusive))).Returns((int id) =>
            {
                return productModelList.Find(c => c.ProductModelID == id);
            });
            _mockProductModelRepository.Setup(c => c.GetProductModelList()).Returns(productModelList);
            _mockProductModelRepository.Setup(c => c.Update(It.IsNotNull<ProductModel>())).Callback((ProductModel newProductModel) => { _mockProductModelRepository.Object.Delete(newProductModel.ProductModelID); _mockProductModelRepository.Object.Create(newProductModel); });
            #endregion

            #region _mockUnitOfWork
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(c => c.ProductsRepository).Returns(_mockProductRepository.Object);
            _mockUnitOfWork.Setup(c => c.ProductDescriptionsRepository).Returns(_mockProductDescriptionRepository.Object);
            _mockUnitOfWork.Setup(c => c.ProductInventoriesRepository).Returns(_mockProductInventoryRepository.Object);
            _mockUnitOfWork.Setup(c => c.ProductListPriceHistoriesRepository).Returns(_mockProductListPriceHistoryRepository.Object);
            _mockUnitOfWork.Setup(c => c.ProductModelProductDescriptionCulturesRepository).Returns(_mockProductModelProductDescriptionCultureRepository.Object);
            _mockUnitOfWork.Setup(c => c.ProductModelsRepository).Returns(_mockProductModelRepository.Object);
            _mockUnitOfWork.Setup(c => c.Commit());
            #endregion

            _homeController = new HomeController(_mockUnitOfWork.Object);
        }

        [TestMethod]
        public void IndexHttpGetIsCorrect()
        {
            //Act
            var result = _homeController.Index("");
            //Assert
            _mockProductRepository.Verify(c=>c.GetProductList());
            _mockProductModelRepository.Verify(c => c.GetProductModelList());
            _mockProductDescriptionRepository.Verify(c => c.GetProductDescriptionList());
            _mockProductInventoryRepository.Verify(c => c.GetProductInventoryList());
            _mockProductListPriceHistoryRepository.Verify(c => c.GetProductListPriceHistoryList());
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.GetProductModelProductDescriptionCultureList());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void RemoveItemIsCorrect()
        {
            int expectedCount = _mockProductRepository.Object.GetProductList().Count() - 1;
            //Act
            var result = _homeController.RemoveItem(1);
            int actualCount = _mockProductRepository.Object.GetProductList().Count();
            //Assert
            Assert.AreEqual(expectedCount, actualCount);
            _mockProductRepository.Verify(c => c.GetProductList());
            _mockProductModelRepository.Verify(c => c.GetProductModelList());
            _mockProductDescriptionRepository.Verify(c => c.GetProductDescriptionList());
            _mockProductInventoryRepository.Verify(c => c.GetProductInventoryList());
            _mockProductListPriceHistoryRepository.Verify(c => c.GetProductListPriceHistoryList());
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.GetProductModelProductDescriptionCultureList());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void AddOrEditProductGetIsCorrect_Adding()
        {
            //Act
            var result = _homeController.AddOrEditProduct() as PartialViewResult;
            //Assert
            _mockProductRepository.Verify(c => c.GetProductList());
            _mockProductModelRepository.Verify(c => c.GetProductModelList());
            _mockProductDescriptionRepository.Verify(c => c.GetProductDescriptionList());
            _mockProductInventoryRepository.Verify(c => c.GetProductInventoryList());
            _mockProductListPriceHistoryRepository.Verify(c => c.GetProductListPriceHistoryList());
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.GetProductModelProductDescriptionCultureList());
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewBag.Title as string, "Adding new product");
            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void AddOrEditProductGetIsCorrect_Editing()
        {
            //Act
            var result = _homeController.AddOrEditProduct(true, 1) as PartialViewResult;
            //Assert
            _mockProductRepository.Verify(c => c.GetProductList());
            _mockProductModelRepository.Verify(c => c.GetProductModelList());
            _mockProductDescriptionRepository.Verify(c => c.GetProductDescriptionList());
            _mockProductInventoryRepository.Verify(c => c.GetProductInventoryList());
            _mockProductListPriceHistoryRepository.Verify(c => c.GetProductListPriceHistoryList());
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.GetProductModelProductDescriptionCultureList());
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewBag.Title as string, "Editing an existing product");
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void AddOrEditProductPostIsCorrect_Adding()
        {
            //Arrange
            int expectedCount = _mockProductRepository.Object.GetProductList().Count() + 1;
            var newProduct = new ProductViewModel()
            {
                ProductEntityId = 0,
                ProductDescriptionEntityDescription = "Description5",
                ProductEntitySellStartDate = DateTime.Today,
                ProductEntityName = "Test5",
                ProductInventoryEntityQuantity = 12,
                ProductListPriceHistoryEntityListPrice = 77.77m,
                ProductDescriptionEntityProductDescriptionID = 0,
                ProductEntityDaysToManufacture = 66,
                ProductListPriceHistoryEntityStartDate = DateTime.Today,
                ProductInventoryEntityBin = 6,
                ProductEntityListPrice = 77.77m,
                ProductInventoryEntityShelf = "Z",
                ProductEntityReorderPoint = 8,
                ProductEntityStandardCost = 77.77m,
                ProductEntitySafetyStockLevel = 23,
                ProductEntityNumber = "TestNumber5",
                ProductModelEntityProductModelID = 0
            };
            var builder = newProduct.GetBuilder();
            //Act
            _homeController.ModelState.Clear();
            var result = _homeController.AddOrEditProduct(newProduct);
            //Assert
            _mockProductRepository.Verify(c => c.Create(builder.ProductEntity));
            _mockProductModelRepository.Verify(c => c.Create(builder.ProductModelEntity));
            _mockProductDescriptionRepository.Verify(c => c.Create(builder.ProductDescriptionEntity));
            _mockProductInventoryRepository.Verify(c => c.Create(builder.ProductInventoryEntity));
            _mockProductListPriceHistoryRepository.Verify(c => c.Create(builder.ProductListPriceHistoryEntity));
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.Create(builder.ProductModelProductDescriptionCultureEntity));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddOrEditProductPostIsCorrect_Editing()
        {
            //Arrange
            int expectedCount = _mockProductRepository.Object.GetProductList().Count() + 1;
            var newProduct = new ProductViewModel()
            {
                ProductEntityId = 1,
                ProductDescriptionEntityDescription = "Description5",
                ProductEntitySellStartDate = DateTime.Today,
                ProductEntityName = "Test5",
                ProductInventoryEntityQuantity = 12,
                ProductListPriceHistoryEntityListPrice = 77.77m,
                ProductDescriptionEntityProductDescriptionID = 1,
                ProductEntityDaysToManufacture = 66,
                ProductListPriceHistoryEntityStartDate = DateTime.Today,
                ProductInventoryEntityBin = 6,
                ProductEntityListPrice = 77.77m,
                ProductInventoryEntityShelf = "Z",
                ProductEntityReorderPoint = 8,
                ProductEntityStandardCost = 77.77m,
                ProductEntitySafetyStockLevel = 23,
                ProductEntityNumber = "TestNumber5",
                ProductModelEntityProductModelID = 1
            };
            var builder = newProduct.GetBuilder();
            //Act
            _homeController.ModelState.Clear();
            var result = _homeController.AddOrEditProduct(newProduct);
            //Assert
            _mockProductRepository.Verify(c => c.Update(builder.ProductEntity));
            _mockProductModelRepository.Verify(c => c.Update(builder.ProductModelEntity));
            _mockProductDescriptionRepository.Verify(c => c.Update(builder.ProductDescriptionEntity));
            _mockProductInventoryRepository.Verify(c => c.Update(builder.ProductInventoryEntity));
            _mockProductListPriceHistoryRepository.Verify(c => c.Update(builder.ProductListPriceHistoryEntity));
            _mockProductModelProductDescriptionCultureRepository.Verify(c => c.Update(builder.ProductModelProductDescriptionCultureEntity));
            Assert.IsNotNull(result);
        }
    }
}
