using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductInventoriesRepository : IDisposable
    {
        IEnumerable<ProductInventory> GetProductInventoryList(); // получение всех объектов

        ProductInventory GetProductInventory(int locationId, int productId); // получение одного объекта по id

        void Create(ProductInventory item); // создание объекта

        void Update(ProductInventory item); // обновление объекта

        void Delete(int locationId, int productId); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}