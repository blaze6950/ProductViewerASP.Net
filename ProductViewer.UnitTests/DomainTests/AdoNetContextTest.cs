using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.UnitTests.DomainTests
{
    [TestClass]
    public class AdoNetContextTest
    {
        private Mock<IAdoNetContext> _mock;
        private IAdoNetContext _context;

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
            _context = _mock.Object;
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
            _mock.Verify(c=>c.CommitChanges());
        }

        [TestMethod]
        public void RefreshDataMethodSuccesExecution()
        {
            //Act
            _context.RefreshData();
            //Assert
            _mock.Verify(c => c.RefreshData());
        }
    }
}
