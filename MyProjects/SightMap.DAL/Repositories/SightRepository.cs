using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public class SightRepo : BaseRepository<Sight>
    {
        public SightRepo(DataDbContext _context) : base(_context) { }

        protected override Sight EagerLoadItemById(int id)
        {
            return set.Include(s => s.Type).FirstOrDefault(s => s.Id == id);
        }

        protected override IQueryable<Sight> EagerLoadCollection(Func<Sight, bool> filter)
        {
            return set.Include(s => s.Type).Where(filter).AsQueryable();
        }
    }

    public class SightTypeRepo : BaseRepository<SightType>
    {
        public SightTypeRepo(DataDbContext _context) : base(_context) { }
    }

    public class BaseRepository<T> : IRepository<T> where T : Base, new()
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
        protected virtual T EagerLoadItemById(int id)
        {
            return set.Find(id);
        }

        protected virtual IQueryable<T> EagerLoadCollection(Func<T, bool> filter)
        {
            return set.Where(filter).AsQueryable();
        }

    }
}
