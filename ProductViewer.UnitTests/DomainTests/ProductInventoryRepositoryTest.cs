using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductInventoryRepositoryTest
    {
        private IProductInventoriesRepository _productInventoriesRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productInventoriesRepository = new ProductInventoryRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
        }

        [TestMethod]
        public void ProductListIsNotNull()
        {
            //Act
            var list = _productInventoriesRepository.GetProductInventoryList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductIsNotNull()
        {
            //Act
            var productInventory = _productInventoriesRepository.GetProductInventory(0, 952);
            //Asset
            Assert.IsNotNull(productInventory);
        }
    }
}
