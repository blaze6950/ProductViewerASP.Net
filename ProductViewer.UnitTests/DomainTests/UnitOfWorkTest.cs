using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void UnitOfWorkIsSuccesfullInit()
        {
            //Arrange
            var context =
                new AdoNetContext(
                    "Data Source=WIN-GQOKRARBRRP;Initial Catalog=AdventureWorks2014;Integrated Security=True");
            //Act
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            //Asset
            Assert.IsNotNull(unitOfWork.ProductDescriptionsRepository);
            Assert.IsNotNull(unitOfWork.ProductInventoriesRepository);
            Assert.IsNotNull(unitOfWork.ProductListPriceHistoriesRepository);
            Assert.IsNotNull(unitOfWork.ProductModelProductDescriptionCulturesRepository);
            Assert.IsNotNull(unitOfWork.ProductModelsRepository);
            Assert.IsNotNull(unitOfWork.ProductsRepository);
        }
    }
}
