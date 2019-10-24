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
        private IRepository<SightType> typeRepo;
        public SightRepo(DataDbContext _context, IRepository<SightType> _typeRepo) : base(_context)
        {
            typeRepo = _typeRepo;
        }

        //public override IEnumerable<Sight> GetList(Expression<Func<Sight, bool>> filter, int offset = 0, int size = int.MaxValue)
        //{
        //    var sights = base.GetList(filter, offset, size);
        //    foreach(var s in sights)
        //    {
        //        s.Type = typeRepo.GetList(t => t.Id == s.SightTypeId).FirstOrDefault();
        //    }

        //    return sights;
        //}

        public override IEnumerable<Sight> GetList(Expression<Func<Sight, bool>> filter, int offset = 0, int size = int.MaxValue)
        {
            var sights = base.GetList(filter, offset, size);

            foreach (var s in sights)
            {
                Expression<Func<SightType, bool>> sightTypeIdExp = t => t.Id == s.SightTypeId;
                s.Type = typeRepo.GetList(sightTypeIdExp).FirstOrDefault();
            }

            return sights;
        }
    }
}
