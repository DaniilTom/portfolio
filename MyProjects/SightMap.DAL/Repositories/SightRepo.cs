using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Linq;

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
}
