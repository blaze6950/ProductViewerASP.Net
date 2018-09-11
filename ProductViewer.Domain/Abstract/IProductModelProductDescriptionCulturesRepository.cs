using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductModelProductDescriptionCulturesRepository
    {
        IEnumerable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultureList(); // получение всех объектов

        ProductModelProductDescriptionCulture GetProductModelProductDescriptionCulture(int id); // получение одного объекта по id

        void Create(ProductModelProductDescriptionCulture item); // создание объекта

        void Update(ProductModelProductDescriptionCulture item); // обновление объекта

        void Delete(int id); // удаление объекта по id
    }
}