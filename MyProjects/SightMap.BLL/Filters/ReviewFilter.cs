using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class ReviewFilter : BaseFilter<Review>
    {
        public int ParentId { get; set; }
        public int ItemId { get; set; }
        public ReviewFilter(ReviewFilterDTO filterDto) : base(filterDto)
        {
            ItemId = filterDto.ItemId;
            ParentId = filterDto.ParentId;
        }

        public override IQueryable<Review> ApplyFilter(IQueryable<Review> set)
        {
            if (Id != 0)
            {
                set.Where(r => r.Id == Id);
                return set;
            }

            if (ParentId != 0)
            {
                // проверка имени
                set.Where(r => r.ParentId == ParentId);
            }

            if (ItemId != 0)
            {
                // проверка имени
                set.Where(r => r.ItemId == ItemId);
            }

            return set;
        }
    }
}
