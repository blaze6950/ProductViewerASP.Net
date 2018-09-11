using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
        private IEnumerable<ProductViewModel> _list;

        public HomeController()
        {
            var productInfoContext = new AdoNetContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _unitOfWork = new UnitOfWork(productInfoContext);
        }

        public ViewResult Index(int page = 1)
        {
            _list = (from p in _unitOfWork.ProductsRepository.GetProductList()
                join pm in _unitOfWork.ProductModelsRepository.GetProductModelList() on p.ProductModelID equals pm.ProductModelID
                join pmpdc in _unitOfWork.ProductModelProductDescriptionCulturesRepository.GetProductModelProductDescriptionCultureList() on pm.ProductModelID equals pmpdc.ProductModelID
                join pd in _unitOfWork.ProductDescriptionsRepository.GetProductDescriptionList() on pmpdc.ProductDescriptionID equals pd.ProductDescriptionID
                join pi in _unitOfWork.ProductInventoriesRepository.GetProductInventoryList() on p.ProductId equals pi.ProductID
                join plph in _unitOfWork.ProductListPriceHistoriesRepository.GetProductListPriceHistoryList() on p.ProductId equals plph.ProductID
                     select new ProductViewModel(p, pd, pi, plph));
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = _list
                .OrderBy(p => p.ProductEntity.Name)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _list.Count()
                }
            };
            return View(model);
        }

        [HttpGet]
        public ViewResult AddOrEditProduct(bool isEditing = false, int id = -1)
        {
            if (isEditing)
            {
                ViewBag.Title = "Editing an existing product";
                return View(_list.First(p => p.ProductEntityId == id));
            }
            else
            {
                ViewBag.Title = "Adding new product";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductEntityId != 0)
                {
                    _unitOfWork.ProductsRepository.Update(product.ProductEntity);
                    _unitOfWork.ProductDescriptionsRepository.Update(product.ProductDescriptionEntity);
                    _unitOfWork.ProductInventoriesRepository.Update(product.ProductInventoryEntity);
                    _unitOfWork.ProductListPriceHistoriesRepository.Update(product.ProductListPriceHistoryEntity);
                    _unitOfWork.Commit();
                    TempData["message"] = string.Format("{0} has been saved", product.ProductEntity.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.ProductsRepository.Create(product.ProductEntity);
                    _unitOfWork.ProductDescriptionsRepository.Create(product.ProductDescriptionEntity);
                    _unitOfWork.ProductInventoriesRepository.Create(product.ProductInventoryEntity);
                    _unitOfWork.ProductListPriceHistoriesRepository.Create(product.ProductListPriceHistoryEntity);
                    _unitOfWork.Commit();
                    TempData["message"] = string.Format("{0} has been saved", product.ProductEntity.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
    }
}