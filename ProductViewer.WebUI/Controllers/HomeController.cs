using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ProductViewer.Domain.Abstract;
using ProductViewer.WebUI.Models;

namespace ProductViewer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private int _pageSize = 5;
        private IEnumerable<ProductViewModel> _list;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult GetProducts([DataSourceRequest]DataSourceRequest request)
        {
            if (_list == null)
            {
                InitialList();
            }

            return Json(_list.ToDataSourceResult(request, p=>new
            {
                ProductEntityId = p.ProductEntityId,
                ProductEntityName = p.ProductEntityName,
                ProductDescriptionEntityDescription = p.ProductDescriptionEntityDescription,
                ProductListPriceHistoryEntityListPrice = p.ProductListPriceHistoryEntityListPrice,
                ProductInventoryEntityQuantity = p.ProductInventoryEntityQuantity,
                PriceForAll = p.PriceForAll
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult RemoveItem(int id)
        {
            if (_list == null)
            {
                InitialList();
            }
            var product = _list.First(p => p.ProductEntityId == id);
            var builder = product.GetBuilder();
            RemoveExistingProduct(builder);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddOrEditProduct(bool isEditing = false, int id = -1)
        {
            if (_list == null)
            {
                InitialList();
            }
            if (isEditing)
            {
                ViewBag.Title = "Editing an existing product";
                var product = _list.First(p => p.ProductEntityId == id);
                return PartialView("Partials/AddOrEditProduct", product);
            }
            else
            {
                ViewBag.Title = "Adding new product";
                return PartialView("Partials/AddOrEditProduct");
            }
        }

        [HttpPost]
        public object AddOrEditProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                ProductViewModelBuilder builder = null;
                try
                {
                    builder = product.GetBuilder();
                    if (product.ProductEntityId != 0)
                    {
                        UpdateExistingProduct(builder);
                        return true;
                    }
                    else
                    {
                        CreateNewProduct(builder);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = string.Format("{0} has not been saved! Error message: {1}", builder?.ProductEntity.Name, e.Message);
                    return PartialView("Partials/AddOrEditProduct", product);
                }
            }
            else
            {
                // there is something wrong with the data values
                return PartialView("Partials/AddOrEditProduct", product);
            }
        }

        public ViewResult ProductDetails(int id)
        {
            if (_list == null)
            {
                InitialList();
            }
            var product = _list.First(p => p.ProductEntityId == id);
            return View(product);
        }

        private void InitialList()
        {
            _list = (from p in _unitOfWork.ProductsRepository.GetProductList()
                join pm in _unitOfWork.ProductModelsRepository.GetProductModelList() on p.ProductModelID equals pm.ProductModelID
                join pmpdc in _unitOfWork.ProductModelProductDescriptionCulturesRepository.GetProductModelProductDescriptionCultureList() on pm.ProductModelID equals pmpdc.ProductModelID
                join pd in _unitOfWork.ProductDescriptionsRepository.GetProductDescriptionList() on pmpdc.ProductDescriptionID equals pd.ProductDescriptionID
                join pi in _unitOfWork.ProductInventoriesRepository.GetProductInventoryList() on p.ProductId equals pi.ProductID
                join plph in _unitOfWork.ProductListPriceHistoriesRepository.GetProductListPriceHistoryList() on p.ProductId equals plph.ProductID
                select new ProductViewModelBuilder(p, pd, pi, plph, pmpdc, pm).ProductViewModel);
        }

        private void CreateNewProduct(ProductViewModelBuilder builder)
        {
            _unitOfWork.ProductModelsRepository.Create(builder.ProductModelEntity);
            _unitOfWork.ProductsRepository.Create(builder.ProductEntity);
            _unitOfWork.ProductInventoriesRepository.Create(builder.ProductInventoryEntity);
            _unitOfWork.ProductListPriceHistoriesRepository.Create(builder.ProductListPriceHistoryEntity);
            _unitOfWork.ProductDescriptionsRepository.Create(builder.ProductDescriptionEntity);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Create(builder.ProductModelProductDescriptionCultureEntity);
            _unitOfWork.Commit();
        }

        private void UpdateExistingProduct(ProductViewModelBuilder builder)
        {
            _unitOfWork.ProductModelsRepository.Update(builder.ProductModelEntity);
            _unitOfWork.ProductsRepository.Update(builder.ProductEntity);
            _unitOfWork.ProductInventoriesRepository.Update(builder.ProductInventoryEntity);
            _unitOfWork.ProductListPriceHistoriesRepository.Update(builder.ProductListPriceHistoryEntity);
            _unitOfWork.ProductDescriptionsRepository.Update(builder.ProductDescriptionEntity);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Update(builder.ProductModelProductDescriptionCultureEntity);
            _unitOfWork.Commit();
        }

        private void RemoveExistingProduct(ProductViewModelBuilder builder)
        {
            _unitOfWork.ProductInventoriesRepository.Delete(builder.ProductInventoryEntity.LocationID, builder.ProductInventoryEntity.ProductID);
            _unitOfWork.ProductListPriceHistoriesRepository.Delete(builder.ProductListPriceHistoryEntity.ProductID, builder.ProductListPriceHistoryEntity.StartDate);
            _unitOfWork.ProductsRepository.Delete(builder.ProductEntity.ProductId);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Delete(builder.ProductModelProductDescriptionCultureEntity.ProductModelID, builder.ProductModelProductDescriptionCultureEntity.ProductDescriptionID);
            _unitOfWork.ProductModelsRepository.Delete(builder.ProductModelEntity.ProductModelID);
            _unitOfWork.ProductDescriptionsRepository.Delete(builder.ProductDescriptionEntity.ProductDescriptionID);
            _unitOfWork.Commit();
        }
    }
}