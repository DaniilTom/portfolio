using System;
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

        public override Expression<Func<SightType, bool>> GetExpression()
        {
            Expression<Func<SightType, bool>> resultExp = base.GetExpression();

            if (Id != 0)
            {
                Expression<Func<SightType, bool>> idExp = s => s.Id == Id;
                return idExp;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                // проверка имени
                Expression<Func<SightType, bool>> nameExp = s => EF.Functions.Like(s.Name, "%" + Name + "%");
                resultExp = AndExp(resultExp, nameExp);
            }

            return resultExp;
        }
    }
}
