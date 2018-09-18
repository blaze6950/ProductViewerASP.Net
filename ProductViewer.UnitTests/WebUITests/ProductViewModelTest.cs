using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.WebUI.Models;

namespace ProductViewer.UnitTests.WebUITests
{
    [TestClass]
    public class ProductViewModelTest
    {
        [TestMethod]
        public void PriceForAllCorrectValue()
        {
            //Arrange
            short quantity = 10;
            int unitPrice = 34;
            int expected = unitPrice * quantity;
            var productViewModel = new ProductViewModel()
            {
                ProductInventoryEntityQuantity = quantity,
                ProductListPriceHistoryEntityListPrice = unitPrice
            };
            //Act
            var actual = productViewModel.PriceForAll;
            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetBuilderMethodReturnValueIsNotNull()
        {
            //Arrange
            var productViewModel = new ProductViewModel();
            //Act
            var actual = productViewModel.GetBuilder();
            //Assert
            Assert.IsNotNull(actual);
        }
    }
}
