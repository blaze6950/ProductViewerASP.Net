using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductInventoriesRepository : IDisposable
    {
        IEnumerable<ProductInventory> GetProductList(); // получение всех объектов

        ProductInventory GetProduct(int id); // получение одного объекта по id

        void Create(ProductInventory item); // создание объекта

        void Update(ProductInventory item); // обновление объекта

        void Delete(int id); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}