using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductModelRepositoryTest
    {
        private IProductModelsRepository _productModelsRepository;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _productModelsRepository = new ProductModelRepository(new AdoNetContext("Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True"));
        }

        [TestMethod]
        public void ProductModelListIsNotNull()
        {
            //Act
            var list = _productModelsRepository.GetProductModelList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductModelIsNotNull()
        {
            //Act
            var product = _productModelsRepository.GetProductModel(59);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
