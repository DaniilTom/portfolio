using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private object lockObj;

        private DbSet<T> _dBSet;
        private DbSet<T> dBSet
        {
            get
            {
                //if (context.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                //    context.Database.GetDbConnection().Open();

                //if (!context.Database.CanConnect())
                //{
                //    context = new DataDbContext();
                //    _dBSet = context.Set<T>();
                //}

                context = new DataDbContext();
                _dBSet = context.Set<T>();

                return _dBSet;
            }

            set
            {
                _dBSet = value;
            }
        }
        private DataDbContext context;

        public BaseRepository()//DataDbContext _context)
        {
            //DataDbContext(DbContextOptions < DataDbContext > options) : base(options) { }

            //DbContextOptionsBuilder<DataDbContext> options = new DbContextOptionsBuilder<DataDbContext>();
            //options.UseSqlServer(DALConstants.ConnectionString);

            context = new DataDbContext();
            //context = _context;
            dBSet = context.Set<T>();
            lockObj = new object();
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

        public virtual IEnumerable<T> GetList(Func<IQueryable<T>, IQueryable<T>> filter, int offset = 0, int size = int.MaxValue)
        {
            //int Id = new Random().Next(1, 5);
            //Expression<Func<T, bool>> exp1 = t => t.Id == Id;

            //return dBSet.Where(filter).Skip(offset).Take(size).ToList();
            //return dBSet.Where(t => t.Id == 1).Skip(offset).Take(size).ToList();
            //return dBSet.AsEnumerable().Where(t => t.Id == 1).AsQueryable().Skip(offset).Take(size).ToList();

            lock (lockObj)
            {
                return filter(dBSet)
                    .Skip(offset)
                    .Take(size)
                    .ToArray();
            }
        }
    }
}
