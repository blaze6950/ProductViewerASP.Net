using System;
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

        [HttpGet]
        public ViewResult Index(int page = 1)
        {
            if (_list == null)
            {
                InitialList();
            }
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

        private void InitialList()
        {
            _list = (from p in _unitOfWork.ProductsRepository.GetProductList()
                join pm in _unitOfWork.ProductModelsRepository.GetProductModelList() on p.ProductModelID equals pm.ProductModelID
                join pmpdc in _unitOfWork.ProductModelProductDescriptionCulturesRepository.GetProductModelProductDescriptionCultureList() on pm.ProductModelID equals pmpdc.ProductModelID
                join pd in _unitOfWork.ProductDescriptionsRepository.GetProductDescriptionList() on pmpdc.ProductDescriptionID equals pd.ProductDescriptionID
                join pi in _unitOfWork.ProductInventoriesRepository.GetProductInventoryList() on p.ProductId equals pi.ProductID
                join plph in _unitOfWork.ProductListPriceHistoriesRepository.GetProductListPriceHistoryList() on p.ProductId equals plph.ProductID
                select new ProductViewModel(p, pd, pi, plph, pmpdc, pm));
        }

        public RedirectToRouteResult RemoveItem(int id)
        {
            if (_list == null)
            {
                InitialList();
            }
            var product = _list.First(p => p.ProductEntityId == id);
            _unitOfWork.ProductInventoriesRepository.Delete(product.ProductInventoryEntity.LocationID, product.ProductInventoryEntity.ProductID);
            _unitOfWork.ProductListPriceHistoriesRepository.Delete(product.ProductListPriceHistoryEntity.ProductID, product.ProductListPriceHistoryEntity.StartDate);
            _unitOfWork.ProductsRepository.Delete(product.ProductEntityId);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Delete(product.ProductModelProductDescriptionCultureEntity.ProductModelID, product.ProductModelProductDescriptionCultureEntity.ProductDescriptionID);
            _unitOfWork.ProductModelsRepository.Delete(product.ProductModelEntity.ProductModelID);
            _unitOfWork.ProductDescriptionsRepository.Delete(product.ProductDescriptionEntity.ProductDescriptionID);
            _unitOfWork.Commit();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ViewResult AddOrEditProduct(bool isEditing = false, int id = -1)
        {
            if (_list == null)
            {
                InitialList();
            }
            if (isEditing)
            {
                ViewBag.Title = "Editing an existing product";
                var product = _list.First(p => p.ProductEntityId == id);
                return View(product);
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
                try
                {
                    if (product.ProductEntityId != 0)
                    {
                        _unitOfWork.ProductModelsRepository.Update(product.ProductModelEntity);
                        _unitOfWork.ProductsRepository.Update(product.ProductEntity);
                        _unitOfWork.ProductInventoriesRepository.Update(product.ProductInventoryEntity);
                        _unitOfWork.ProductListPriceHistoriesRepository.Update(product.ProductListPriceHistoryEntity);
                        _unitOfWork.ProductDescriptionsRepository.Update(product.ProductDescriptionEntity);
                        _unitOfWork.ProductModelProductDescriptionCulturesRepository.Update(product.ProductModelProductDescriptionCultureEntity);
                        _unitOfWork.Commit();
                        TempData["message"] = string.Format("{0} has been saved", product.ProductEntity.Name);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _unitOfWork.ProductModelsRepository.Create(product.ProductModelEntity);
                        _unitOfWork.ProductsRepository.Create(product.ProductEntity);
                        _unitOfWork.ProductInventoriesRepository.Create(product.ProductInventoryEntity);
                        _unitOfWork.ProductListPriceHistoriesRepository.Create(product.ProductListPriceHistoryEntity);
                        _unitOfWork.ProductDescriptionsRepository.Create(product.ProductDescriptionEntity);
                        _unitOfWork.ProductModelProductDescriptionCulturesRepository.Create(product.ProductModelProductDescriptionCultureEntity);
                        _unitOfWork.Commit();
                        TempData["message"] = string.Format("{0} has been saved", product.ProductEntity.Name);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = string.Format("{0} has not been saved! Error message: {1}", product.ProductEntity.Name, e.Message);
                    return View(product);
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