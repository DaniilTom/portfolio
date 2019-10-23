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

        public override bool IsStatisfy(Review item)
        {
            if (Id != 0)
            {
                if (Id == item.Id)
                    return true;
                else
                    return false;
            }
            
            if (ItemId != 0)
            {
                // проверка имени
                if (ItemId != item.ItemId)
                    return false;
            }
            
            if (ParentId != 0)
            {
                // проверка имени
                if (ParentId != item.ParentId)
                    return false;
            }

            return true;
        }
    }
}
