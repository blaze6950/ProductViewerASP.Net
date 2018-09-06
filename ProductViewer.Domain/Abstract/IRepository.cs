using System.Collections.Generic;

namespace ProductViewer.Domain.Abstract
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetItemList(); // получение всех объектов

        void Create(T item); // создание объекта

        void Update(T item); // обновление объекта

        void Save();  // сохранение изменений
    }
}