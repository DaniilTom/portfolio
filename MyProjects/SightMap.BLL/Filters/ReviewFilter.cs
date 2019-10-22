using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SightMap.BLL.Filters
{
    public class ReviewFilter : BaseFilter<Review>
    {
        public int ItemId { get; set; }
        public ReviewFilter(ReviewFilterDTO dto) : base(dto)
        {
            ItemId = dto.ItemId;
        }

        public override bool IsStatisfy(Review item)
        {
            return (ItemId == item.ItemId) || base.IsStatisfy(item);
        }
    }
}
