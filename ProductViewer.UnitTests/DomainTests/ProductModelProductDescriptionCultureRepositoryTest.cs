using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductModelProductDescriptionCultureRepositoryTest
    {
        private IProductModelProductDescriptionCulturesRepository _productDescriptionCulturesRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productModelProductDescriptionCultureDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productModelProductDescriptionCultureDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ProductDescriptionID", typeof(int)),
                    new DataColumn("ProductModelID", typeof(int)),
                    new DataColumn("CultureID", typeof(string))
                }
            };
            _productModelProductDescriptionCultureDataTable.Rows.Add(1, 2, "en");
            _productModelProductDescriptionCultureDataTable.Rows.Add(2, 3, "en");
            _productModelProductDescriptionCultureDataTable.Rows.Add(3, 4, "en");
            _mock.Setup(c => c.GetProductModelProductDescriptionCulture()).Returns(_productModelProductDescriptionCultureDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productDescriptionCulturesRepository = new ProductModelProductDescriptionCultureRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductModelProductDescriptionCultureClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productModelProductDescriptionCultureDataTable.Rows.Count + 1;
            var newProductModelProductDescriptionCulture = new ProductModelProductDescriptionCulture() { ProductModelID = 5, ProductDescriptionID = 4, CultureID = "en"};
            //Act
            _productDescriptionCulturesRepository.Create(newProductModelProductDescriptionCulture);
            //Assert
            _mock.Verify(c => c.GetProductModelProductDescriptionCulture());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productModelProductDescriptionCultureDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductModelProductDescriptionCultureClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productModelProductDescriptionCultureDataTable.Rows.Count - 1;
            //Act
            _productDescriptionCulturesRepository.Delete(2, 1);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productModelProductDescriptionCultureDataTable.Rows.Count);
            _mock.Verify(c => c.CommitChanges());
        }

        [TestMethod]
        public void Update_ProductModelProductDescriptionCultureClassObjectPassed()
        {
            //Arrange
            var productModelProductDescriptionCulture = _productDescriptionCulturesRepository.GetProductModelProductDescriptionCulture(2, 1);
            var oldCultureID = productModelProductDescriptionCulture.CultureID;
            var newCultureID = "ru";
            productModelProductDescriptionCulture.CultureID = newCultureID;
            //Act
            _productDescriptionCulturesRepository.Update(productModelProductDescriptionCulture);
            //Assert
            _mock.Verify(c => c.GetProductModelProductDescriptionCulture());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(oldCultureID, _productDescriptionCulturesRepository.GetProductModelProductDescriptionCulture(2, 1).CultureID);
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
            var product = _productDescriptionCulturesRepository.GetProductModelProductDescriptionCulture(2, 1);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
