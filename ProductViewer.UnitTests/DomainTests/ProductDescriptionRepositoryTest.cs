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
    public class ProductDescriptionRepositoryTest
    {
        private IProductDescriptionsRepository _productDescriptionsRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productDescriptionDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productDescriptionDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ProductDescriptionID", typeof(int)){AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1},
                    new DataColumn("Description", typeof(string))
                }
            };
            _productDescriptionDataTable.Rows.Add(1, "Test1");
            _productDescriptionDataTable.Rows.Add(2, "Test2");
            _productDescriptionDataTable.Rows.Add(3, "Test3");
            _mock.Setup(c => c.GetProductDescriptions()).Returns(_productDescriptionDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productDescriptionsRepository = new ProductDescriptionRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductDescriptionClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productDescriptionDataTable.Rows.Count + 1;
            var newProduct = new ProductDescription() {ProductDescriptionID = 4, Description = "Test4"};
            //Act
            _productDescriptionsRepository.Create(newProduct);
            //Assert
            _mock.Verify(c => c.GetProductDescriptions());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productDescriptionDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductDescriptionClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productDescriptionDataTable.Rows.Count - 1;
            //Act
            _productDescriptionsRepository.Delete(1);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productDescriptionDataTable.Rows.Count);
            _mock.Verify(c => c.CommitChanges());
        }

        [TestMethod]
        public void Update_ProductDescriptionClassObjectPassed()
        {
            //Arrange
            var product = _productDescriptionsRepository.GetProductDescription(1);
            var oldDescription = product.Description;
            var newDescription = "Test0";
            product.Description = newDescription;
            //Act
            _productDescriptionsRepository.Update(product);
            //Assert
            _mock.Verify(c => c.GetProductDescriptions());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreNotEqual(oldDescription, _productDescriptionsRepository.GetProductDescription(1).Description);
        }

        [TestMethod]
        public void ProductDescriptionListIsNotNull()
        {
            //Act
            var list = _productDescriptionsRepository.GetProductDescriptionList();
            //Asset
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ProductDescriptionIsNotNull()
        {
            //Act
            var product = _productDescriptionsRepository.GetProductDescription(1);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
