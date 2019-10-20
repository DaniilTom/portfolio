using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetList(Func<T, bool> filter);
        T Add(T item);
        T Update(T item);
        bool Delete(int id);
    }
}
