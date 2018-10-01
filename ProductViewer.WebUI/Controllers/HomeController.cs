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

        public ActionResult GetProducts([System.Web.Http.ModelBinding.ModelBinder(typeof(WebApiDataSourceRequestModelBinder))]DataSourceRequest request)
        {
            if (_list == null)
            {
                InitialList();
            }
            return Json(_list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult Index(string searchValue, int page = 1, Column sortCurrentCol = Column.Name, bool sortCurrentDir = false, Column sortColumn = Column.Name)
        {
            if (_list == null)
            {
                InitialList();
            }
            var sortConfig = new SortConfig(){CurrentColumn = sortCurrentCol, IsAsc = sortCurrentDir};
            switch (sortColumn)
            {
                case Column.Name:
                    if (sortConfig.CurrentColumn != sortColumn)
                    {
                        sortConfig.CurrentColumn = sortColumn;
                        sortConfig.IsAsc = true;
                        _list = _list.OrderBy(p => p.ProductEntityName);
                    }
                    else
                    {
                        sortConfig.IsAsc = !sortConfig.IsAsc;
                        if (sortConfig.IsAsc)
                        {
                            _list = _list.OrderBy(p => p.ProductEntityName);
                            break;
                        }
                        _list = _list.OrderByDescending(p => p.ProductEntityName);
                    }
                    break;
                case Column.UnitPrice:
                    if (sortConfig.CurrentColumn != sortColumn)
                    {
                        sortConfig.CurrentColumn = sortColumn;
                        sortConfig.IsAsc = true;
                        _list = _list.OrderBy(p => p.ProductListPriceHistoryEntityListPrice);
                    }
                    else
                    {
                        sortConfig.IsAsc = !sortConfig.IsAsc;
                        if (sortConfig.IsAsc)
                        {
                            _list = _list.OrderBy(p => p.ProductListPriceHistoryEntityListPrice);

                            break;
                        }
                        _list = _list.OrderByDescending(p => p.ProductListPriceHistoryEntityListPrice);
                    }
                    break;
                case Column.Quantity:
                    if (sortConfig.CurrentColumn != sortColumn)
                    {
                        sortConfig.CurrentColumn = sortColumn;
                        sortConfig.IsAsc = true;
                        _list = _list.OrderBy(p => p.ProductInventoryEntityQuantity);
                    }
                    else
                    {
                        sortConfig.IsAsc = !sortConfig.IsAsc;
                        if (sortConfig.IsAsc)
                        {
                            _list = _list.OrderBy(p => p.ProductInventoryEntityQuantity);
                            break;
                        }
                        _list = _list.OrderByDescending(p => p.ProductInventoryEntityQuantity);
                    }
                    break;
                case Column.PriceForAll:
                    if (sortConfig.CurrentColumn != sortColumn)
                    {
                        sortConfig.CurrentColumn = sortColumn;
                        sortConfig.IsAsc = true;
                        _list = _list.OrderBy(p => p.PriceForAll);
                    }
                    else
                    {
                        sortConfig.IsAsc = !sortConfig.IsAsc;
                        if (sortConfig.IsAsc)
                        {
                            _list = _list.OrderBy(p => p.PriceForAll);
                            break;
                        }
                        _list = _list.OrderByDescending(p => p.PriceForAll);
                    }
                    break;
                default:
                    _list = _list.OrderBy(p => p.ProductEntityName);
                    break;
            }
            
            ProductListViewModel model = null;
            if (string.IsNullOrEmpty(searchValue) || string.IsNullOrWhiteSpace(searchValue))
            {
                model = new ProductListViewModel()
                {
                    Products = _list
                        .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = _pageSize,
                        TotalItems = _list.Count()
                    },
                    SortConfig = sortConfig
                };
            }
            else
            {
                var buffer = _list
                    .Where(p => p.ProductEntityName.ToLower().Contains(searchValue.ToLower()));
                model = new ProductListViewModel()
                {
                    Products = buffer
                        .Skip((page - 1) * _pageSize)
                        .Take(_pageSize),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = _pageSize,
                        TotalItems = buffer.Count()
                    },
                    SearchValue = searchValue,
                    SortConfig = sortConfig
                };
            }
            return View(model);
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