using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList(Func<T, bool> filter, int offset = 0, int size = int.MaxValue);
        T Add(T item);
        T Update(T item);
        bool Delete(int id);
    }
}
