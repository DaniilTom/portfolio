using SightMap.DAL.Models;
using System;
using System.Linq;

namespace SightMap.DAL.Repositories
{
    public class SightTypeRepo : BaseRepository<SightType>
    {
        public SightTypeRepo(DataDbContext _context) : base(_context) { }

        protected override SightType EagerLoadItemById(int id)
        {
            return set.Find(id);
        }

        protected override IQueryable<SightType> EagerLoadCollection(Func<SightType, bool> filter)
        {
            return set.Where(filter).AsQueryable();
        }
    }
}
