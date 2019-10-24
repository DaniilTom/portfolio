using System;
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

        public override Expression<Func<Review, bool>> GetExpression()
        {
            Expression<Func<Review, bool>> resultExp = base.GetExpression();

            if (Id != 0)
            {
                Expression<Func<Review, bool>> idExp = r => r.Id == Id;
                return idExp;
            }

            if (ParentId != 0)
            {
                // проверка имени
                Expression<Func<Review, bool>> parentIdExp = r => r.ParentId == ParentId;
                resultExp = AndExp(resultExp, parentIdExp);
            }

            if (ItemId != 0)
            {
                // проверка имени
                Expression<Func<Review, bool>> itemIdExp = r => r.ItemId == ItemId;
                resultExp = AndExp(resultExp, itemIdExp);
            }

            return resultExp;
        }
    }
}
