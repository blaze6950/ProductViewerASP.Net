using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductInventoriesRepository : IDisposable
    {
        IEnumerable<ProductInventory> GetProductInventoryList(); // получение всех объектов

        ProductInventory GetProductInventory(int id); // получение одного объекта по id

        void Create(ProductInventory item); // создание объекта

        void Update(ProductInventory item); // обновление объекта

        void Delete(int id); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}