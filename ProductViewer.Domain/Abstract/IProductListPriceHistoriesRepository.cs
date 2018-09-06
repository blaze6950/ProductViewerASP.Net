using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductListPriceHistoriesRepository : IDisposable
    {
        IEnumerable<ProductListPriceHistory> GetProductListPriceHistoryList(); // получение всех объектов

        ProductListPriceHistory GetProductListPriceHistory(int productId, DateTime startDate); // получение одного объекта по id

        void Create(ProductListPriceHistory item); // создание объекта

        void Update(ProductListPriceHistory item); // обновление объекта

        void Delete(int productId, DateTime startDate); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}