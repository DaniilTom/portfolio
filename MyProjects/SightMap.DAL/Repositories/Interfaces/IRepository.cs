using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SightMap.DAL.Repositories
{
    public interface IRepository<T>
    {
        //IEnumerable<T> GetList(Expression<Func<T, bool>> filter, int offset = 0, int size = int.MaxValue);
        IEnumerable<T> GetList(Func<IQueryable<T>, IQueryable<T>> filter, int offset = 0, int size = int.MaxValue);

        T Add(T item);
        T Update(T item);
        bool Delete(int id);
    }
}
