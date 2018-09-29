using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductViewer.WebUI.Models;

namespace ProductViewer.UnitTests.WebUITests
{
    [TestClass]
    public class PagingInfoTest
    {
        [TestMethod]
        public void TotalPagesValueIsCorrect()
        {
            //Arrange
            int itemsPerPage = 5, totalItems = 20;
            int expected = (int)(totalItems / itemsPerPage);
            var pagingInfo = new PagingInfo() {CurrentPage = 1, ItemsPerPage = itemsPerPage, TotalItems = totalItems };
            //Act
            var actual = pagingInfo.TotalPages;
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
