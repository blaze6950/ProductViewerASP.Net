using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private IProductsRepository _productsRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ProductID", typeof(int)){AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1},
                    new DataColumn("DaysToManufacture", typeof(int)),
                    new DataColumn("ListPrice", typeof(decimal)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("ProductNumber", typeof(string)),
                    new DataColumn("ReorderPoint", typeof(Int16)),
                    new DataColumn("SafetyStockLevel", typeof(Int16)),
                    new DataColumn("SellStartDate", typeof(DateTime)),
                    new DataColumn("StandardCost", typeof(decimal)),
                    new DataColumn("ProductModelID", typeof(int))
                }
            };
            _productDataTable.Rows.Add(1, 2, 33.33m, "Kayak", "TT8-088", 23, 12, DateTime.Now, 44.44m, 0);
            _productDataTable.Rows.Add(2, 3, 44.44m, "Lodka", "TT9-099", 24, 13, DateTime.Now, 55.55m, 0);
            _productDataTable.Rows.Add(3, 4, 55.55m, "Katamaran", "TT6-06", 25, 14, DateTime.Now, 66.66m, 0);
            _mock.Setup(c => c.GetProducts()).Returns(_productDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productsRepository = new ProductRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productDataTable.Rows.Count + 1;
            var newProduct = new Product(){DaysToManufacture = 23, ProductId = 4, ProductModelID = 0, Name = "Test", SafetyStockLevel = 67, ListPrice = 77.77m, ProductNumber = "TestNumber", ReorderPoint = 34, SellStartDate = DateTime.Now, StandardCost = 33.33m};
            //Act
            _productsRepository.Create(newProduct);
            //Assert
            _mock.Verify(c=>c.GetProducts());
            _mock.Verify(c=>c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productDataTable.Rows.Count - 1;
            //Act
            _productsRepository.Delete(1);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productDataTable.Rows.Count);
            _mock.Verify(c=>c.CommitChanges());
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
            _mock.Verify(c=>c.GetProducts());
            _mock.Verify(c=>c.CommitChanges());
            Assert.AreNotEqual(oldName, _productsRepository.GetProduct(1).Name);
        }

        [TestMethod]
        public void ProductListIsNotNull()
        {
            //Act
            var list = _productsRepository.GetProductList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductIsNotNull()
        {
            //Act
            var product = _productsRepository.GetProduct(1);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
