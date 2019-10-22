using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SightMap.DAL.Repositories
{
    public class ReviewRepo : BaseRepository<Review>
    {
        public ReviewRepo(DataDbContext _context) : base(_context) { }
    }
}
