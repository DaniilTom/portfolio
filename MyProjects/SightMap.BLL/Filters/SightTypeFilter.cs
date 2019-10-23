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

        public override bool IsStatisfy(SightType item)
        {
            if (Id != 0)
            {
                if (Id == item.Id)
                    return true;
                else
                    return false;
            }
            
            if (!string.IsNullOrEmpty(Name))
            {
                // проверка имени
                if (Name != item.Name)
                    return false;
            }

            return true;
        }
    }
}
