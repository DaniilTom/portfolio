using SightMap.DAL.Models;
using System;
using System.Linq;

namespace SightMap.DAL.Repositories
{
    public class SightTypeRepo : BaseRepository<SightType>
    {
        public SightTypeRepo(DataDbContext _context) : base(_context) { }
    }
}
