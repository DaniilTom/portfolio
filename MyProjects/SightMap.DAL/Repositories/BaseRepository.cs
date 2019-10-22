using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private DbSet<T> dBSet;
        private DataDbContext context;

        public BaseRepository(DataDbContext _context)
        {
            context = _context;
            dBSet = _context.Set<T>();
        }

        public virtual T Add(T item)
        {
            //return dBSet.Add(item)?.Entity;

            T temp = null;
            var entityEntry = dBSet.Add(item);
            if (context.SaveChanges() > 0)
                temp = entityEntry.Entity;
            return temp;
        }

        public virtual T Update(T item)
        {
            T temp = null;
            
            if (!(item.Id > 0))
                return temp;

            var entityEntry = dBSet.Update(item);
            if (context.SaveChanges() > 0)
                temp = entityEntry.Entity;
            
            return temp;
        }

        public virtual bool Delete(int id)
        {
            T item = new T { Id = id };
            dBSet.Attach(item);
            dBSet.Remove(item);
            if (context.SaveChanges() > 0)
                return true;
            return false;
        }

        public virtual IEnumerable<T> GetList(Func<T, bool> filter, int offset = 0, int size = int.MaxValue)
        {
            return dBSet.AsNoTracking().Where(filter).Skip(offset).Take(size).ToList();
        }
    }
}
