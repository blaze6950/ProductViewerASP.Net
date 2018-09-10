using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using ProductViewer.WebUI.Models;

namespace ProductViewer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private int _pageSize = 5;

        public HomeController()
        {
            var productInfoContext = new AdoNetContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _unitOfWork = new UnitOfWork(productInfoContext);
        }

        public ViewResult Index(int page = 1)
        {
            var list = (from p in _unitOfWork.ProductsRepository.GetProductList()
                join pd in _unitOfWork.ProductDescriptionsRepository.GetProductDescriptionList() on p.ProductId equals
                    pd.ProductDescriptionID
                join pi in _unitOfWork.ProductInventoriesRepository.GetProductInventoryList() on p.ProductId equals pi
                    .ProductID
                join plph in _unitOfWork.ProductListPriceHistoriesRepository.GetProductListPriceHistoryList() on
                    p.ProductId equals plph.ProductID
                select new ProductViewModel(p, pd, pi, plph));
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = list
                .OrderBy(p => p.ProductName)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = list.Count()
                }
            };
            return View(model);
        }
    }
}