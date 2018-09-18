using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductInventoriesRepository
    {
        IEnumerable<ProductInventory> GetProductInventoryList(); // получение всех объектов

        ProductInventory GetProductInventory(Int16 locationId, int productId); // получение одного объекта по id

        void Create(ProductInventory item); // создание объекта

        void Update(ProductInventory item); // обновление объекта

        void Delete(Int16 locationId, int productId); // удаление объекта по id
    }
}