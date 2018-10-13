using System;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;
using ProductViewer.UnitTests.Fakes;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class RepositoryTest
    {
        private IRepository<Product> _repository;
        private Mock<IProductViewerContext> _mockContext;
        private Mock<FakeDbSet<Product>> _mockDbSet;
        private FakeDbSet<Product> _products;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _products = new FakeDbSet<Product>
            {
                new Product() {DaysToManufacture = 1, ListPrice = new decimal(11.11), Name = "Test1", ProductID = 1, ProductModelID = 1, ProductNumber = "TT-T001", ReorderPoint = 1, SafetyStockLevel = 1, SellStartDate = DateTime.Today, StandardCost = new decimal(11.11)},
                new Product() {DaysToManufacture = 2, ListPrice = new decimal(22.22), Name = "Test2", ProductID = 2, ProductModelID = 2, ProductNumber = "TT-T002", ReorderPoint = 2, SafetyStockLevel = 2, SellStartDate = DateTime.Today, StandardCost = new decimal(22.22)},
                new Product() {DaysToManufacture = 3, ListPrice = new decimal(33.33), Name = "Test3", ProductID = 3, ProductModelID = 3, ProductNumber = "TT-T003", ReorderPoint = 3, SafetyStockLevel = 3, SellStartDate = DateTime.Today, StandardCost = new decimal(33.33)}
            };
            _mockDbSet = new Mock<FakeDbSet<Product>>();
            _mockContext = new Mock<IProductViewerContext>();
            _mockDbSet.Setup(s => s.Add(It.IsNotNull<Product>())).Callback((Product p) => _products.Add(p));
            _mockDbSet.Setup(s => s.AsNoTracking()).Returns(_products);
            _mockContext.SetupGet(c => c.Products).Returns(() => _mockDbSet.Object);
            _mockContext.Setup(c => c.Set<Product>()).Returns(() => _mockDbSet.Object);
            _mockContext.Setup(c => c.SetDeleted(It.IsNotNull<Product>()));
            _mockContext.Setup(c => c.SetModified(It.IsNotNull<Product>()));
            _repository = new Repository<Product>(_mockContext.Object);
        }

        [TestMethod]
        public void CreateIsCorrect()
        {
            //Act
            int expectedCount = _products.Length + 1;
            _repository.Create(new Product());
            //Assert
            _mockDbSet.Verify(s => s.Add(It.IsNotNull<Product>()));
            Assert.AreEqual(expectedCount, _products.Length);
        }

        [TestMethod]
        public void FindByIdIsCorrect()
        {
            //Act
            _repository.FindById(c => c.ProductID == 10);
            //Assert
            _mockDbSet.Verify(s => s.AsNoTracking());
        }

        [TestMethod]
        public void GetAllCorrect()
        {
            //Act
            _repository.Get();
            //Assert
            _mockDbSet.Verify(s => s.AsNoTracking());
        }

        [TestMethod]
        public void GetWithPredicateIsCorrect()
        {
            //Act
            _repository.Get(c => c.ListPrice > 10);
            //Assert
            _mockDbSet.Verify(s => s.AsNoTracking());
        }

        [TestMethod]
        public void DeleteIsCorrect()
        {
            //Act
            _repository.Delete(new Product());
            //Assert
            _mockContext.Verify(c => c.SetDeleted(It.IsNotNull<Product>()));
        }

        [TestMethod]
        public void UpdateIsCorrect()
        {
            //Act
            _repository.Update(new Product());
            //Assert
            _mockContext.Verify(c => c.SetModified(It.IsNotNull<Product>()));
        }
    }
}