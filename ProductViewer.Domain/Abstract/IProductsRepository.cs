using System.Collections.Generic;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Abstract
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProductList(); // получение всех объектов

        Product GetProduct(int id); // получение одного объекта по id

        void Create(Product item); // создание объекта

        void Update(Product item); // обновление объекта

        void Delete(int id); // удаление объекта по id
    }
}