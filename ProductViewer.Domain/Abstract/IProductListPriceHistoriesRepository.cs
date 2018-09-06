﻿using System;
using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductListPriceHistoriesRepository : IDisposable
    {
        IEnumerable<ProductListPriceHistory> GetProductList(); // получение всех объектов

        ProductListPriceHistory GetProduct(int id); // получение одного объекта по id

        void Create(ProductListPriceHistory item); // создание объекта

        void Update(ProductListPriceHistory item); // обновление объекта

        void Delete(int id); // удаление объекта по id

        void Save();  // сохранение изменений
    }
}