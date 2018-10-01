using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.Domain.Entities;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class ProductModelRepositoryTest
    {
        private IProductModelsRepository _productModelsRepository;
        private Mock<IAdoNetContext> _mock;
        private DataTable _productModelDataTable;

        [TestInitialize]
        public void SetupTests()
        {
            //Arrange
            _mock = new Mock<IAdoNetContext>();
            _productModelDataTable = new DataTable()
            {
                Columns =
                {
                    new DataColumn("ProductModelID", typeof(int)){AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1},
                    new DataColumn("Name", typeof(string))
                }
            };
            _productModelDataTable.Rows.Add(1, "Kayak");
            _productModelDataTable.Rows.Add(2, "Lodka");
            _productModelDataTable.Rows.Add(3, "Sudno");
            _mock.Setup(c => c.GetProductModels()).Returns(_productModelDataTable);
            _mock.Setup(c => c.CommitChanges());
            _productModelsRepository = new ProductModelRepository(_mock.Object);
        }

        [TestMethod]
        public void Create_ProductModelClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productModelDataTable.Rows.Count + 1;
            var newProductModel = new ProductModel() {Name = "Test", ProductModelID = 4};
            //Act
            _productModelsRepository.Create(newProductModel);
            //Assert
            _mock.Verify(c => c.GetProductModels());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreEqual(countRowsExcepted, _productModelDataTable.Rows.Count);
        }

        [TestMethod]
        public void Delete_ProductModelClassObjectPassed()
        {
            //Arrange
            int countRowsExcepted = _productModelDataTable.Rows.Count - 1;
            //Act
            _productModelsRepository.Delete(1);
            //Assert
            Assert.AreEqual(countRowsExcepted, _productModelDataTable.Rows.Count);
            _mock.Verify(c => c.CommitChanges());
        }

        [TestMethod]
        public void Update_ProductModelClassObjectPassed()
        {
            //Arrange
            var product = _productModelsRepository.GetProductModel(1);
            var oldName = product.Name;
            var newName = "NewName";
            product.Name = newName;
            //Act
            _productModelsRepository.Update(product);
            //Assert
            _mock.Verify(c => c.GetProductModels());
            _mock.Verify(c => c.CommitChanges());
            Assert.AreNotEqual(oldName, _productModelsRepository.GetProductModel(1).Name);
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
            var product = _productModelsRepository.GetProductModel(1);
            //Asset
            Assert.IsNotNull(product);
        }
    }
}
