using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ProductViewer.Domain.Abstract;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:60750", headers: "*", methods: "*")]
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
                            p.ProductEntityId,
                            p.ProductEntityName,
                            p.ProductDescriptionEntityDescription,
                            p.ProductListPriceHistoryEntityListPrice,
                            p.ProductInventoryEntityQuantity,
                            p.PriceForAll
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
            _unitOfWork.ProductInventoriesRepository.Delete(builder.ProductInventoryEntity);
            _unitOfWork.ProductListPriceHistoriesRepository.Delete(builder.ProductListPriceHistoryEntity);
            _unitOfWork.ProductsRepository.Delete(builder.ProductEntity);
            _unitOfWork.ProductModelProductDescriptionCulturesRepository.Delete(builder.ProductModelProductDescriptionCultureEntity);
            _unitOfWork.ProductModelsRepository.Delete(builder.ProductModelEntity);
            _unitOfWork.ProductDescriptionsRepository.Delete(builder.ProductDescriptionEntity);
            _unitOfWork.Commit();
        }

        private void InitialList()
        {
            _list = (from p in _unitOfWork.ProductsRepository.Get()
                     join pm in _unitOfWork.ProductModelsRepository.Get() on p.ProductModelID equals pm.ProductModelID
                     join pmpdc in _unitOfWork.ProductModelProductDescriptionCulturesRepository.Get() on pm.ProductModelID equals pmpdc.ProductModelID
                     join pd in _unitOfWork.ProductDescriptionsRepository.Get() on pmpdc.ProductDescriptionID equals pd.ProductDescriptionID
                     join pi in _unitOfWork.ProductInventoriesRepository.Get() on p.ProductID equals pi.ProductID
                     join plph in _unitOfWork.ProductListPriceHistoriesRepository.Get() on p.ProductID equals plph.ProductID
                     select new ProductViewModelBuilder(p, pd, pi, plph, pmpdc, pm).ProductViewModel);
        }
    }
}
