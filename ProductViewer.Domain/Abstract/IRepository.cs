﻿using System;
using System.Collections.Generic;

namespace ProductViewer.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        T FindById(Func<T, bool> predicate);

        IEnumerable<T> Get(); // получение всех объектов

        IEnumerable<T> Get(Func<T, bool> predicate);

        void Create(T item); // создание объекта

        void Update(T item); // обновление объекта

        void Delete(T item); // удаление объекта
    }
}