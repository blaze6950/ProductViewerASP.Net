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
    public class ProductInventoryRepositoryTest
    {
        private IProductInventoriesRepository _productInventoriesRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productInventoryDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productInventoryDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("Bin", typeof(byte)),
                    new DataColumn("LocationID", typeof(Int16)),
                    new DataColumn("ProductID", typeof(int)),
                    new DataColumn("Quantity", typeof(Int16)),
                    new DataColumn("Shelf", typeof(string))
                }
            };
            _productInventoryDataTable.Rows.Add(1, 2, 5, 23, "A");
            _productInventoryDataTable.Rows.Add(2, 3, 6, 34, "B");
            _productInventoryDataTable.Rows.Add(3, 4, 7, 45, "C");
            _mock.Setup(c => c.GetProductInventories()).Returns(_productInventoryDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productInventoriesRepository = new ProductInventoryRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductInventoryClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productInventoryDataTable.Rows.Count + 1;
            var newProductInventory = new ProductInventory() { Quantity = 56, ProductID = 8, LocationID = 5, Shelf = "D", Bin = 4};
            //Act
            _productInventoriesRepository.Create(newProductInventory);
            //Assert
            _mock.Verify(c => c.GetProductInventories());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productInventoryDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductInventoryClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productInventoryDataTable.Rows.Count - 1;
            //Act
            _productInventoriesRepository.Delete(2, 5);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productInventoryDataTable.Rows.Count);
            _mock.Verify(c => c.CommitChanges());
        }

        [TestMethod]
        public void Update_ProductInventoryClassObjectPassed()
        {
            //Arrange
            var productInventory = _productInventoriesRepository.GetProductInventory(2, 5);
            var oldShelf = productInventory.Shelf;
            var newShelf = "D";
            productInventory.Shelf = newShelf;
            //Act
            _productInventoriesRepository.Update(productInventory);
            //Assert
            _mock.Verify(c => c.GetProductInventories());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreNotEqual(oldShelf, _productInventoriesRepository.GetProductInventory(2, 5).Shelf);
        }

        [TestMethod]
        public void ProductInventoryListIsNotNull()
        {
            //Act
            var list = _productInventoriesRepository.GetProductInventoryList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductInventoryIsNotNull()
        {
            //Act
            var productInventory = _productInventoriesRepository.GetProductInventory(2, 5);
            //Asset
            Assert.IsNotNull(productInventory);
        }
    }
}
