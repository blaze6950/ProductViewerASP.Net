using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductModelProductDescriptionCultureRepositoryTest
    {
        private IProductModelProductDescriptionCulturesRepository _productDescriptionCulturesRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
        }

        [TestMethod]
        public void ProductModelProductDescriptionCultureListIsNotNull()
        {
            //Act
            var list = _productDescriptionCulturesRepository.GetProductModelProductDescriptionCultureList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductModelProductDescriptionCultureIsNotNull()
        {
            //Act
            var product = _productDescriptionCulturesRepository.GetProductModelProductDescriptionCulture(59, 744);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
