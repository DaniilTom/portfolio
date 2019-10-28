using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SightMap.BLL.Filters
{
    public interface IFilter<T> where T : BaseEntity
    {
        int Offset { get; }
        int Size { get; }
        //Expression<Func<T, bool>> GetExpression();
        IQueryable<T> ApplyFilter(IQueryable<T> set);
    }
}
