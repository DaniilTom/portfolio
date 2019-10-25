using Microsoft.EntityFrameworkCore;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SightMap.DAL.Repositories
{
    public class SightRepo : BaseRepository<Sight>
    {
        public SightRepo(DataDbContext _context) : base(_context) { }
    }
}
