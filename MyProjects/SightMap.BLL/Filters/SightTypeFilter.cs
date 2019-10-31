using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;

namespace SightMap.BLL.Filters
{
    public class SightTypeFilter : BaseFilter<SightType>
    {
        public string Name { get; set; }

        public SightTypeFilter(SightTypeFilterDTO filterDto) : base(filterDto)
        {
            Name = filterDto.Name;
        }

        public override IQueryable<SightType> ApplyFilter(IQueryable<SightType> set)
        {
            if (Id != 0)
            {
                set = set.Where(s => s.Id == Id);
                return set;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                // проверка имени
                set = set.Where(s => EF.Functions.Like(s.Name, "%" + Name + "%"));
            }

            return set;
        }
    }
}
