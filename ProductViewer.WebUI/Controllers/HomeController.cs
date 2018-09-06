using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;

namespace ProductViewer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductsRepository _products;
        private IProductDescriptionsRepository _productDescriptions;
        private IProductInventoriesRepository _productInventories;
        private IProductListPriceHistoriesRepository _productListPriceHistories;

        public HomeController()
        {
            var productInfoContext = new ProductInfoContext();
            _products = new ProductRepository(productInfoContext);
            _productDescriptions = new ProductDescriptionRepository(productInfoContext);
            _productInventories = new ProductInventoryRepository(productInfoContext);
            _productListPriceHistories = new ProductListPriceHistoryRepository(productInfoContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}