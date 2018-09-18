using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductDescriptionRepositoryTest
    {
        private IProductDescriptionsRepository _productsRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productsRepository = new ProductDescriptionRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
        }

        [TestMethod]
        public void ProductListIsNotNull()
        {
            //Act
            var list = _productsRepository.GetProductDescriptionList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductIsNotNull()
        {
            //Act
            var product = _productsRepository.GetProductDescription(744);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
