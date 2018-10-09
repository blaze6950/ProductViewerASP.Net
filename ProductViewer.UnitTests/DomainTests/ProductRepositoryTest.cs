using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;

namespace ProductViewer.UnitTests.DomainTests
{
    public class InMemoryDatabase
    {
        private readonly OrmLiteConnectionFactory dbFactory =
            new OrmLiteConnectionFactory(":memory:", SqliteOrmLiteDialectProvider.Instance);

        public IDbConnection OpenConnection() => this.dbFactory.OpenDbConnection();

        public void Insert<T>(IEnumerable<T> items)
        {
            using (var db = this.OpenConnection())
            {
                db.CreateTableIfNotExists<T>();
                foreach (var item in items)
                {
                    db.Insert(item);
                }
            }
        }
    }

    [TestClass]
    public class ProductRepositoryTest
    {
        private IProductsRepository _productsRepository;
        private Mock<IConnectionFactory> _mock;
        private List<Product> _products;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _products = new List<Product>
            {
                new Product() {DaysToManufacture = 1, ListPrice = new decimal(11.11), Name = "Test1", ProductId = 0, ProductModelID = 0, ProductNumber = "TT-T001", ReorderPoint = 1, SafetyStockLevel = 1, SellStartDate = DateTime.Today, StandardCost = new decimal(11.11)},
                new Product() {DaysToManufacture = 2, ListPrice = new decimal(22.22), Name = "Test2", ProductId = 2, ProductModelID = 2, ProductNumber = "TT-T002", ReorderPoint = 2, SafetyStockLevel = 2, SellStartDate = DateTime.Today, StandardCost = new decimal(22.22)},
                new Product() {DaysToManufacture = 3, ListPrice = new decimal(33.33), Name = "Test3", ProductId = 3, ProductModelID = 3, ProductNumber = "TT-T003", ReorderPoint = 3, SafetyStockLevel = 3, SellStartDate = DateTime.Today, StandardCost = new decimal(33.33)},
            };
            var db = new InMemoryDatabase();
            db.Insert(_products);
            _mock = new Mock<IConnectionFactory>();
            _mock.SetupGet(c => c.GetConnection).Returns(db.OpenConnection());
            _productsRepository = new ProductRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _products.Count + 1;
            var newProduct = new Product(){DaysToManufacture = 23, ProductId = 4, ProductModelID = 0, Name = "Test", SafetyStockLevel = 67, ListPrice = 77.77m, ProductNumber = "TestNumber", ReorderPoint = 34, SellStartDate = DateTime.Now, StandardCost = 33.33m};
            //Act
            _productsRepository.Create(newProduct);
            //Assert
            _mock.Verify(c=>c.GetConnection);
            Assert.AreEqual(countRowsExcepted, _products.Count);
        }

        [TestMethod]
        public void Delete_ProductClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _products.Count - 1;
            //Act
            _productsRepository.Delete(1);
            //Assert
            Assert.AreEqual(countRowsExcepted, _products.Count);
            _mock.Verify(c => c.GetConnection);
        }

        [TestMethod]
        public void Update_ProductClassObjectPassed()
        {
            //Arrange
            var product = _productsRepository.GetProduct(1);
            var oldName = product.Name;
            var newName = "NewName";
            product.Name = newName;
            //Act
            _productsRepository.Update(product);
            //Assert
            _mock.Verify(c=>c.GetConnection);
            Assert.AreNotEqual(oldName, _productsRepository.GetProduct(1).Name);
        }

        [TestMethod]
        public void ProductListIsNotNull()
        {
            //Act
            var list = _productsRepository.GetProductList();
            //Asset
            Assert.IsNotNull(list);
            _mock.Verify(c => c.GetConnection);
        }

        [TestMethod]
        public void ProductIsNotNull()
        {
            //Act
            var product = _productsRepository.GetProduct(1);
            //Asset
            Assert.IsNotNull(product);
            _mock.Verify(c => c.GetConnection);
        }
    }
}
