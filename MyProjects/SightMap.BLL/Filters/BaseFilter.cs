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

    public class AlbumFilter : BaseFilter<Album>
    {
        public int ItemId { get; set; }
        public bool IsMain { get; set; }

        public AlbumFilter(AlbumFilterDTO filterDto) : base(filterDto)
        {
            ItemId = filterDto.ItemId;
            IsMain = filterDto.IsMain;
        }

        public override IQueryable<Album> ApplyFilter(IQueryable<Album> set)
        {
            if (Id != 0)
            {
                set = set.Where(a => a.Id == Id);
                return set;
            }

            if(ItemId != 0)
            {
                set = set.Where(a => a.ItemId == ItemId);
            }

            if(IsMain)
            {
                set = set.Where(a => a.IsMain == true);
            }

            return set;
        }
    }
}
