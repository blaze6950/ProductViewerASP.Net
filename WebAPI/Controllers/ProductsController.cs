using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Concrete;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private IEnumerable<ProductViewModel> _list;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            if (_list == null)
            {
                InitialList();
            }
            return Json(from p in _list
                        select new
                        {
                            ProductEntityId = p.ProductEntityId,
                            ProductEntityName = p.ProductEntityName,
                            ProductDescriptionEntityDescription = p.ProductDescriptionEntityDescription,
                            ProductListPriceHistoryEntityListPrice = p.ProductListPriceHistoryEntityListPrice,
                            ProductInventoryEntityQuantity = p.ProductInventoryEntityQuantity,
                            PriceForAll = p.PriceForAll
                        });
        }

        // GET: api/Products/5
        [HttpGet]
        public ProductViewModel GetProduct(int id)
        {
            if (_list == null)
            {
                InitialList();
            }
            return _list.FirstOrDefault(p => p.ProductEntityId == id);
        }

        // POST: api/Products
        [HttpPost]
        public void Create([FromBody]ProductViewModel value)
        {
            var builder = value.GetBuilder();
            _unitOfWork.ProductModelsRepository.Create(builder.ProductModelEntity);
            _unitOfWork.ProductsRepository.Create(builder.ProductEntity);
            _unitOfWork.ProductInventoriesRepository.Create(builder.ProductInventoryEntity);
            _unitOfWork.ProductListPriceHistoriesRepository.Create(builder.ProductListPriceHistoryEntity);
            _unitOfWork.ProductDescriptionsRepository.Create(builder.ProductDescriptionEntity);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Create(builder.ProductModelProductDescriptionCultureEntity);
            _unitOfWork.Commit();
        }

        // PUT: api/Products/5
        [HttpPut]
        public void Update([FromBody]ProductViewModel value)
        {
            var builder = value.GetBuilder();
            _unitOfWork.ProductModelsRepository.Update(builder.ProductModelEntity);
            _unitOfWork.ProductsRepository.Update(builder.ProductEntity);
            _unitOfWork.ProductInventoriesRepository.Update(builder.ProductInventoryEntity);
            _unitOfWork.ProductListPriceHistoriesRepository.Update(builder.ProductListPriceHistoryEntity);
            _unitOfWork.ProductDescriptionsRepository.Update(builder.ProductDescriptionEntity);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Update(builder.ProductModelProductDescriptionCultureEntity);
            _unitOfWork.Commit();
        }

        // DELETE: api/Products/5
        [HttpDelete]
        public void Delete(int id)
        {
            var builder = GetProduct(id).GetBuilder();
            _unitOfWork.ProductInventoriesRepository.Delete(builder.ProductInventoryEntity.LocationID, builder.ProductInventoryEntity.ProductID);
            _unitOfWork.ProductListPriceHistoriesRepository.Delete(builder.ProductListPriceHistoryEntity.ProductID, builder.ProductListPriceHistoryEntity.StartDate);
            _unitOfWork.ProductsRepository.Delete(builder.ProductEntity.ProductId);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Delete(builder.ProductModelProductDescriptionCultureEntity.ProductModelID, builder.ProductModelProductDescriptionCultureEntity.ProductDescriptionID);
            _unitOfWork.ProductModelsRepository.Delete(builder.ProductModelEntity.ProductModelID);
            _unitOfWork.ProductDescriptionsRepository.Delete(builder.ProductDescriptionEntity.ProductDescriptionID);
            _unitOfWork.Commit();
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
    }
}
