using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductModelProductDescriptionCulturesRepository
    {
        IEnumerable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultureList(); // получение всех объектов

        ProductModelProductDescriptionCulture GetProductModelProductDescriptionCulture(int productModelId, int productDescriptionId); // получение одного объекта по id

        ProductModelProductDescriptionCulture Create(ProductModelProductDescriptionCulture item); // создание объекта

        void Update(ProductModelProductDescriptionCulture item); // обновление объекта

        void Delete(int productModelId, int productDescriptionId); // удаление объекта по id
    }
}