using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Base, new()
    {
        protected DbSet<T> set;
        private DataDbContext context;

        public BaseRepository(DataDbContext _context)
        {
            context = _context;
            set = _context.Set<T>();
        }

        public T Add(T item)
        {
            T temp = null;
            var entityEntry = set.Add(item);
            if (context.SaveChanges() > 0)
                temp = entityEntry.Entity;
            return temp;
        }

        public T Update(T item)
        {
            T temp = null;
            var entityEntry = set.Update(item);
            if (context.SaveChanges() > 0)
                temp = entityEntry.Entity;
            return temp;
        }

        public bool Delete(int id)
        {
            T item = new T { Id = id };
            set.Attach(item);
            set.Remove(item);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }

        public T GetById(int id)
        {
            var item = EagerLoadItemById(id);
            context.Entry(item).State = EntityState.Detached;
            return item;
        }

        public IEnumerable<T> GetList(Func<T, bool> filter)
        {
            var collection = EagerLoadCollection(filter);
            return collection.AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Переопределяется для жадной загрузки.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract T EagerLoadItemById(int id);

        protected abstract IQueryable<T> EagerLoadCollection(Func<T, bool> filter);

    }
}
