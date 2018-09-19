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
    public class ProductListPriceHistoryRepositoryTest
    {
        private IProductListPriceHistoriesRepository _productListPriceHistoriesRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productListPriceHistoryDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productListPriceHistoryDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ProductID", typeof(int)),
                    new DataColumn("StartDate", typeof(DateTime)),
                    new DataColumn("ListPrice", typeof(decimal))
                }
            };
            _productListPriceHistoryDataTable.Rows.Add(1, DateTime.Today, 33.33m);
            _productListPriceHistoryDataTable.Rows.Add(2, DateTime.Today, 44.44m);
            _productListPriceHistoryDataTable.Rows.Add(3, DateTime.Today, 55.55m);
            _mock.Setup(c => c.GetProductListPriceHistories()).Returns(_productListPriceHistoryDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productListPriceHistoriesRepository = new ProductListPriceHistoryRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductListPriceHistoryClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productListPriceHistoryDataTable.Rows.Count + 1;
            var newProductListPriceHistory = new ProductListPriceHistory() { ProductID = 4, ListPrice = 66.66m, StartDate = DateTime.Now};
            //Act
            _productListPriceHistoriesRepository.Create(newProductListPriceHistory);
            //Assert
            _mock.Verify(c => c.GetProductListPriceHistories());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productListPriceHistoryDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductListPriceHistoryClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productListPriceHistoryDataTable.Rows.Count - 1;
            //Act
            _productListPriceHistoriesRepository.Delete(1, DateTime.Today);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productListPriceHistoryDataTable.Rows.Count);
            _mock.Verify(c => c.CommitChanges());
        }

        [TestMethod]
        public void Update_ProductListPriceHistoryClassObjectPassed()
        {
            //Arrange
            var productListPriceHistory = _productListPriceHistoriesRepository.GetProductListPriceHistory(1, DateTime.Today);
            var oldListPrice = productListPriceHistory.ListPrice;
            var newListPrice = 99.99m;
            productListPriceHistory.ListPrice = newListPrice;
            //Act
            _productListPriceHistoriesRepository.Update(productListPriceHistory);
            //Assert
            _mock.Verify(c => c.GetProductListPriceHistories());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreNotEqual(oldListPrice, _productListPriceHistoriesRepository.GetProductListPriceHistory(1, DateTime.Today).ListPrice);
        }

        [TestMethod]
        public void ProductListPriceHistoryListIsNotNull()
        {
            //Act
            var list = _productListPriceHistoriesRepository.GetProductListPriceHistoryList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductListPriceHistoryIsNotNull()
        {
            //Act
            var product = _productListPriceHistoriesRepository.GetProductListPriceHistory(1, DateTime.Today);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
