using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SightMap.BLL.Filters
{
    public abstract class BaseFilter<T> : IFilter<T> where T : BaseEntity
    {
        public int Id { get; }
        public int Offset { get; }
        public int Size { get; }

        protected BaseFilter(BaseFilterDTO filterDto)
        {
            Id = filterDto.Id;
            Offset = filterDto.Offset;
            Size = filterDto.Size;
        }

        public abstract IQueryable<T> ApplyFilter(IQueryable<T> set);
    }
}
