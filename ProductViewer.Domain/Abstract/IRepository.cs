using System.Collections.Generic;

namespace ProductViewer.Domain.Abstract
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetItemList(); // получение всех объектов

        T Create(T item); // создание объекта

        void Update(T item); // обновление объекта

        void Save();  // сохранение изменений
    }
}