using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class AdoNetContextTest
    {
        private AdoNetContext _context;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _context = new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True");
        }

        [TestMethod]
        public void ProductDataTableIsNotNull()
        {
            //Act
            var products = _context.GetProducts();
            //Assert
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void ProductDescriptionDataTableIsNotNull()
        {
            //Act
            var productDescriptions = _context.GetProductDescriptions();
            //Assert
            Assert.IsNotNull(productDescriptions);
        }

        [TestMethod]
        public void ProductInventoryDataTableIsNotNull()
        {
            //Act
            var productInventories = _context.GetProductInventories();
            //Assert
            Assert.IsNotNull(productInventories);
        }

        [TestMethod]
        public void ProductListPriceHistoryDataTableIsNotNull()
        {
            //Act
            var productListPriceHistories = _context.GetProductListPriceHistories();
            //Assert
            Assert.IsNotNull(productListPriceHistories);
        }

        [TestMethod]
        public void ProductModelDataTableIsNotNull()
        {
            //Act
            var productModels = _context.GetProductModels();
            //Assert
            Assert.IsNotNull(productModels);
        }

        [TestMethod]
        public void CommitChangesMethodSuccesExecution()
        {
            //Act
            _context.CommitChanges();
            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RefreshDataMethodSuccesExecution()
        {
            //Act
            _context.RefreshData();
            //Assert
            Assert.IsTrue(true);
        }
    }
}
