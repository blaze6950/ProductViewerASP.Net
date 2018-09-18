using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private IProductsRepository _productsRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productsRepository = new ProductRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
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
            var product = _productsRepository.GetProduct(952);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
