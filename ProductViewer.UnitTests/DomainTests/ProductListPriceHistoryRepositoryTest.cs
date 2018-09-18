using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductListPriceHistoryRepositoryTest
    {
        private IProductListPriceHistoriesRepository _productListPriceHistoriesRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productListPriceHistoriesRepository = new ProductListPriceHistoryRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
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
            var product = _productListPriceHistoriesRepository.GetProductListPriceHistory(952, DateTime.Today);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
