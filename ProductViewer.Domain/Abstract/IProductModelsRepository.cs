using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductModelsRepository
    {
        IEnumerable<ProductModel> GetProductModelList(); // получение всех объектов

        ProductModel GetProductModel(int id); // получение одного объекта по id

        void Create(ProductModel item); // создание объекта

        void Update(ProductModel item); // обновление объекта

        void Delete(int id); // удаление объекта по id
    }
}