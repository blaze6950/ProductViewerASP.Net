using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        private IUnitOfWork _unitOfWork;
        private Mock<IAdoNetContext> _mock;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _mock.Setup(c => c.GetProducts()).Returns(new DataTable());
            _mock.Setup(c => c.RefreshData());
            _mock.Setup(c => c.GetProductModels()).Returns(new DataTable());
            _mock.Setup(c => c.GetProductDescriptions()).Returns(new DataTable());
            _mock.Setup(c => c.CommitChanges());
            _mock.Setup(c => c.GetProductInventories()).Returns(new DataTable());
            _mock.Setup(c => c.GetProductListPriceHistories()).Returns(new DataTable());
            _mock.Setup(c => c.GetProductModelProductDescriptionCulture()).Returns(new DataTable());
            _mock.Setup(c => c.Dispose());
            _unitOfWork = new UnitOfWork(_mock.Object);
        }

        [TestMethod]
        public void ProductsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductDescriptionsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductDescriptionsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductInventoriesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductInventoriesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductListPriceHistoriesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductListPriceHistoriesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductModelProductDescriptionCulturesRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductModelProductDescriptionCulturesRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ProductModelsRepository_IsSuccesfullInit()
        {
            //Act
            var actual = _unitOfWork.ProductModelsRepository;
            //Asset
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CommitMethodIsCorrect()
        {
            //Act
            _unitOfWork.Commit();
            //Asset
            _mock.Verify(c=>c.CommitChanges());
        }

        [TestMethod]
        public void DisposeMethodIsCorrect()
        {
            //Act
            _unitOfWork.Dispose();
            //Asset
            _mock.Verify(c => c.Dispose());
        }
    }
}
