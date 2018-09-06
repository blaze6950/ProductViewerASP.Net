using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductDescriptionsRepository : IDisposable
    {
        IEnumerable<ProductDescription> GetProductDescriptionList(); // получение всех объектов

        ProductDescription GetProductDescription(int id); // получение одного объекта по id

        void Create(ProductDescription item); // создание объекта

        void Update(ProductDescription item); // обновление объекта

        void Delete(int id); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}